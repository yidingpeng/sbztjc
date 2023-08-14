using AutoMapper;
using Microsoft.Extensions.Configuration;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Purchase;
using RW.PMS.Application.Contracts.Input.Message;
using RW.PMS.Application.Contracts.Message;
using RW.PMS.Application.Contracts.Purchase;
using RW.PMS.Application.Contracts.System;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.Extensions;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.Purchase;
using RW.PMS.Domain.Entities.System;
using RW.PMS.Domain.Repositories;
using RW.PMS.Domain.Repositories.System;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RW.PMS.Application.Purchase
{
    public class PurchaseService : CrudApplicationService<Mat_OperateEntity, int>, IPurchaseService
    {
        private readonly IUserService _userService;
        private readonly IThirdPartService _thirdPartService;
        private readonly IRoleService _roleService;
        private readonly ISupplierService _supplierService;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IindentPrimaryService _indentPrimaryService;
        private readonly IConfiguration _configuration;
        private readonly IConfigService _configService;
        private readonly IESBMessageService _message;
        private readonly IQCService _qc;
        public PurchaseService(IDataValidatorProvider dataValidator, IThirdPartService thirdPartService, IUserService userService, IRoleService roleService,
            IUserRoleRepository userRoleRepository, IindentPrimaryService indentPrimaryService, IConfiguration configuration, IConfigService configService,
           ISupplierService supplierService, IESBMessageService message, IQCService qc,
           IRepository<Mat_OperateEntity, int> repository, IMapper mapper, Lazy<ICurrentUser> currentUser) : base(
           dataValidator, repository, mapper, currentUser)
        {
            _userService = userService;
            _thirdPartService = thirdPartService;
            _roleService = roleService;
            _supplierService = supplierService;
            _userRoleRepository = userRoleRepository;
            _indentPrimaryService = indentPrimaryService;
            _configuration = configuration;
            _configService = configService;
            _message = message;
            _qc = qc;
        }
        /// <summary>
        /// 订单子表分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PagedResult<MaterialOperateDto> GetPagedList(MaterialOperateSearchDto input)
        {
            var list = Repository.Select.From<DictionaryEntity, Mat_SupplierEntity,UserEntity>((a, b, c,u) => a
                 .LeftJoin(a => a.State == b.DictionaryValue)
                 .LeftJoin(a => a.Supplier == c.SupCode)
                 .LeftJoin(a => Int32.Parse(a.AssignStaff) == u.Id))
                 .WhereIf(input.MaterialCode.NotNullOrEmpty(), (a, b, c,u) => a.MaterialCode.Contains(input.MaterialCode)||a.MaterialName.Contains(input.MaterialCode))
                 .Where(a => a.t1.BomId == input.BomId)
                 .Count(out var total)
                 .Page(input.PageNo, input.PageSize)
                 .ToList((a, b, c,u) => new
                 {
                     AllM = a,
                     State = b,
                     Supper = c,
                     staffName=u.RealName,
                 })
                 .Select(t =>
                 {
                     var mapp = Mapper.Map<MaterialOperateDto>(t.AllM);
                     mapp.StateText = t.State == null ? "" : t.State.DictionaryText;
                     mapp.SupplierText = t.Supper == null ? "" : t.Supper.SupName;
                     mapp.AssignStaffTxt = t.staffName;
                     return mapp;
                 }).ToList();
            return new PagedResult<MaterialOperateDto>(list, total);
        }
        /// <summary>
        /// 根据BomId查询订单明细集合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<MaterialOperateDto> GetOperateList(MaterialOperateSearchDto input)
        {
            //var info = Repository.Orm.Select<Qr_CodeEntity>().Where(o => o.BomId == input.BomId).ToList();
            //var qc = Repository.Orm.Select<MatQCEntity>().Where(o=>o.QrCode.Contains())
            var list = Repository.Select.From<DictionaryEntity, Mat_SupplierEntity, UserEntity, Qr_CodeEntity>((a, b, c, u, q) => a
                   .LeftJoin(a => a.State == b.DictionaryValue)
                   .LeftJoin(a => a.Supplier == c.SupCode)
                   .LeftJoin(a => Int32.Parse(a.AssignStaff) == u.Id)
                   .LeftJoin(a => a.BomId == q.BomId && a.MaterialCode == q.MaterialCode))
                   .Where(a => a.t1.BomId == input.BomId)
                   .ToList((a, b, c, u, q) => new
                   {
                       AllM = a,
                       State = b,
                       Supper = c,
                       staffName = u.RealName,
                       qr = q,
                   })
                   .Select(t =>
                     {
                         var mapp = Mapper.Map<MaterialOperateDto>(t.AllM);
                         mapp.StateText = t.State == null ? "" : t.State.DictionaryText;
                         mapp.SupplierText = t.Supper == null ? "" : t.Supper.SupName;
                         mapp.AssignStaffTxt = t.staffName;
                         //var QcResult = info.Where(o => o.MaterialCode == mapp.MaterialCode).FirstOrDefault();
                         mapp.QcResult = t.qr == null ? "0" : t.qr.qualified.ToString();
                         mapp.IsQrCode = t.qr == null ? false : true;
                         //mapp.QcTime = t.qc == null ? "" : t.qc.CreatedAt.ToString();
                         return mapp;
                     }).ToList();
            return new List<MaterialOperateDto>(list);
        }
        /// <summary>
        /// 根据ID查询前五条历史价格数据
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public List<MaterialOperateDto> GetByIdList(int Id)
        {
            var toone = Repository.Where(o => o.Id == Id).ToOne();
            var list = Repository.Select.From<Mat_SupplierEntity>((a, b) => a
                 .LeftJoin(a => a.Supplier == b.SupCode))
                 .Where((a, b) => a.MaterialCode == toone.MaterialCode && a.CostPrice > 0)
                 .OrderBy((a, b) => a.CostPrice)
                 .Page(1, 5)
                 .ToList((a, b) => new
                 {
                     AllM = a,
                     Supper = b
                 })
                 .Select(t =>
                 {
                     var mapp = Mapper.Map<MaterialOperateDto>(t.AllM);
                     mapp.SupplierText = t.Supper == null ? "" : t.Supper.SupName;
                     return mapp;
                 }).ToList();
            //var list = Repository.Where(o => o.MaterialCode == toone.MaterialCode && o.CostPrice > 0).OrderBy(o => o.CostPrice).Page(1,5).ToList();
            return Mapper.Map<List<MaterialOperateDto>>(list);
        }

        /// <summary>
        /// 根据ID查询历史供应商
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public List<MaterialOperateDto> GetByIdSupplier(int Id)
        {
            var toone = Repository.Where(o => o.Id == Id).ToOne();
            var list = Repository.Select.From<Mat_SupplierEntity>((a, b) => a
                 .LeftJoin(a => a.Supplier == b.SupCode))
                 .Where((a, b) => a.MaterialCode == toone.MaterialCode)
                 .OrderByDescending((a, b) => a.CreatedAt)
                 .Page(1, 5)
                 .ToList((a, b) => new
                 {
                     AllM = a,
                     Supper = b
                 })
                 .Select(t =>
                 {
                     var mapp = Mapper.Map<MaterialOperateDto>(t.AllM);
                     mapp.SupplierText = t.Supper == null ? "" : t.Supper.SupName;
                     return mapp;
                 }).ToList();
            //var list = Repository.Where(o => o.MaterialCode == toone.MaterialCode && o.CostPrice > 0).OrderBy(o => o.CostPrice).Page(1,5).ToList();
            return Mapper.Map<List<MaterialOperateDto>>(list);
        }
        /// <summary>
        /// 根据供应商查询订单子表分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PagedResult<MaterialOperateDto> GetSupplierPagedList(MaterialOperateSearchDto input)
        {
            var list = Repository.Select.From<DictionaryEntity, Mat_SupplierEntity>((a, b, c) => a
                 .LeftJoin(a => a.State == b.DictionaryValue)
                 .LeftJoin(a => a.Supplier == c.SupCode))
                 .Where(a => a.t1.Supplier == input.Supplier)
                 .Count(out var total)
                 .Page(input.PageNo, input.PageSize)
                 .ToList((a, b, c) => new
                 {
                     AllM = a,
                     State = b,
                     Supper = c
                 })
                 .Select(t =>
                 {
                     var mapp = Mapper.Map<MaterialOperateDto>(t.AllM);
                     mapp.StateText = t.State == null ? "" : t.State.DictionaryText;
                     mapp.SupplierText = t.Supper == null ? "" : t.Supper.SupName;
                     return mapp;
                 }).ToList();
            return new PagedResult<MaterialOperateDto>(list, total);
        }

        /// <summary>
        /// 判断是否存在相同bom物料
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool IsExist(MaterialOperateDto input)
        {
            var exist = Repository.Where(t => t.MaterialCode == input.MaterialCode && t.IsDeleted == false).ToOne();
            if (exist == null) return false;
            if (input.Id.HasValue)
            {
                return input.Id.Value != exist.Id;
            }
            return true;
        }
        /// <summary>
        /// 物料处理选择供应商
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="supCode"></param>
        /// <returns></returns>
        public bool OptSupplierEdit(string ids, string supCode)
        {
            string[] strArray = ids.Split(','); //字符串转数组
            foreach (var item in strArray)
            {
                Repository.Orm.Update<Mat_OperateEntity>(item)
                .Set(a => a.Supplier, supCode)
                .ExecuteAffrows();
            }
            return true;
        }
        /// <summary>
        /// 质检人员消息通知
        /// </summary>
        /// <param name="bomcode"></param>
        /// <param name="materialcode"></param>
        /// <param name="link"></param>
        /// <param name="esburl"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public bool MessagePush(string bomcode, string materialcode, string link, string esburl,string time)
        {
            try
            {
                //根据角色名称获取角色id
                var roleid = _roleService.GetList().Where(t => t.RoleName == "质检人员").First()?.Id ?? 0;

                //获取拥有该角色的所有用户id
                var useridArr = _userRoleRepository.GetList().Where(t => t.RoleId == roleid).ToList();

                if (roleid != 0)
                {
                    ArrayList myArrayList = new ArrayList();
                    //根据角色获取用户信息
                    foreach (var item in useridArr)
                    {
                        var Telnum = _userService.GetUserTelById(item.UserId)?.Telnum ?? null;
                        if (Telnum.NotNullOrWhiteSpace())
                            myArrayList.Add(new { type = "user", id = Telnum });
                    }

                    var data = new ESBSendMessageInput
                    {
                        Title = "您有编码为：" + materialcode + "的物料（BOM编码为" + bomcode + "）即将到货待检验。",
                        Type = 2,
                        Desc = "",
                        Link = "/purchase",
                        From = "PLM",
                        TargetPlantform = "OA",
                        Targets = myArrayList.ToArray(),
                        SendMode = 1,
                        SendModeValue = time
                    };
                    _thirdPartService.PostRequestAsync(esburl, data);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }

        }


        /// <summary>
        /// 物料处理成本价格
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="supCode"></param>
        /// <returns></returns>
        public bool CostPriceEdit(string ids, decimal costPrice)
        {
            string[] strArray = ids.Split(','); //字符串转数组
            foreach (var item in strArray)
            {
                Repository.Orm.Update<Mat_OperateEntity>(item)
                                .Set(a => a.CostPrice, costPrice)
                                .ExecuteAffrows();
            }
            return true;
        }
        /// <summary>
        /// 批量下单
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public bool batchAdd(List<Mat_OperateEntity> items)
        {
            Repository.Orm.Insert(items).ExecuteAffrows();
            return true;
        }

        #region 采购库存信息
        /// <summary>
        /// 库存信息分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PagedResult<InventoryDto> InventoryPagedList(InventorySearchDto input)
        {
            var list = Repository.Orm.Select<Mat_InventoryEntity>()
                 .WhereIf(input.MaterialCode.NotNullOrEmpty(), a => a.MaterialCode.Contains(input.MaterialCode) || a.MaterialName.Contains(input.MaterialName))
                 .WhereIf(input.ProjectCode.NotNullOrEmpty(), a => a.ProjectCode.Contains(input.ProjectCode) || a.ProjectName.Contains(input.ProjectName))
                 .Count(out var total)
                 .Page(input.PageNo, input.PageSize)
                 .ToList();
            return new PagedResult<InventoryDto>(Mapper.Map<List<InventoryDto>>(list), total);
        }
        #endregion

        /// <summary>
        /// 供应商修改预计、实际完成日期及预计入库日期
        /// </summary>
        /// <returns></returns>
        public bool OrderDateTimeEdit(int Id, string type, string time,int BomId)
        {
            try
            {
                //接收
                if (type == "JS")
                {
                    Repository.Orm.Update<Mat_OperateEntity>(Id).Set(a => a.YJFinishTime, time).ExecuteAffrows();
                }
                //完成
                else if (type == "WC")
                {
                    var date = time.Split(',');
                    Repository.Orm.Update<Mat_OperateEntity>(Id).Set(a => a.SJFinishTime, date[0]).Set(a => a.YjShipTime, date[1]).ExecuteAffrows();
                }
                //发货
                else if (type == "FH")
                {
                    int i = Repository.Orm.Update<Mat_OperateEntity>(Id).Set(a => a.GYSArrivalTime, time).ExecuteAffrows();
                    if (i > 0)
                    {
                        //订单主表
                        var entity = _indentPrimaryService.ByBomId(BomId);
                        //订单子表物料信息
                        var operate = Repository.Where(o => o.Id == Id).ToOne();
                        if (entity != null && operate != null)
                        {
                            var configs = _configService.GetConfigs("ZJTX").FirstOrDefault();
                            string esburl = _configuration["ESBUrl:key"] + "/sendMessage";
                            string plmurl = _configuration["PLMUrl:key"];
                            DateTime dtDate;
                            //判断是否日期格式
                            if (DateTime.TryParse(operate.GYSArrivalTime,out dtDate))
                            {
                                //预计入库时间-1
                                string yjrkTime = Convert.ToDateTime(operate.GYSArrivalTime).AddDays(-Convert.ToInt32(configs.Value)).ToString("yyyy-MM-dd");
                                //发送消息至质检人员
                                MessagePush(entity.BomCode, operate.MaterialCode, plmurl, esburl, yjrkTime);
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        /// <summary>
        /// 采购分配人员消息通知
        /// </summary>
        /// <param name="url"></param>
        /// <param name="title"></param>
        /// <param name="users"></param>
        /// <returns></returns>
        public string SendMessagePurchase(string url, string title, params string[] users)
        {
            var result = _message.SendToUser(url, title, users);
            return result;
        }

        /// <summary>
        /// 质检后在BOM订单明细中修改质检结果和质检时间
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="supCode"></param>
        /// <returns></returns>
        //public bool BomDetailQCEdit(int bomId,string MaterialCode,int result)
        //{
        //    if (!string.IsNullOrWhiteSpace(MaterialCode) && bomId > 0)
        //    {
        //        Repository.Orm.Update<Mat_OperateEntity>()
        //           .Set(a => a.QcResult, result == 1 ? "合格" : "不合格")
        //           .Set(a => a.QcTime, DateTime.Now)
        //           .Where(o => o.BomId == bomId && o.MaterialCode == MaterialCode)
        //           .ExecuteAffrows();
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
    }
}
