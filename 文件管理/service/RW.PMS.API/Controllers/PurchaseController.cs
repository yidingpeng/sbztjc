using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Project;
using RW.PMS.Application.Contracts.DTO.Purchase;
using RW.PMS.Application.Contracts.Message;
using RW.PMS.Application.Contracts.Purchase;
using RW.PMS.Application.Contracts.System;
using RW.PMS.CrossCutting.Exceptions;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.Purchase;
using RW.PMS.Domain.Repositories.Material;
using System.Collections;

namespace RW.PMS.API.Controllers
{
    public class PurchaseController : RWBaseController
    {
        IPurchaseService _purchaseService;
        ISupplierService _supplierService;
        IConfiguration _configuration;
        IFifoService _fifoService;
        IQrCodeService _qrcodeService;
        IinventoryService _inventoryService;
        IindentPrimaryService _indentprimaryService;
        IPurchaseOrderService _purchaseOrderService;
        IPurchaseDetailService _purchaseDetailService;
        IQCService _qc;
        IPurchaseOperateService _ipo;
        IProductIssuService _issu;
        IQrCodePicService _qrCodePic;
        ICdService _cd;
        IUserService _user;
        ILogService _log;

        public PurchaseController(IPurchaseService purchaseService, ISupplierService supplierService, IConfiguration configuration,
            IFifoService fifoService, IQrCodeService qrcodeService, IinventoryService inventoryService,
            IPurchaseOrderService purchaseOrderService, IPurchaseDetailService purchaseDetailService, IQrCodePicService qrCodePic,
            IindentPrimaryService indentprimaryService, IQCService qc, IPurchaseOperateService ipo, IProductIssuService issu, ICdService cd,
            IUserService user, ILogService log)
        {
            _purchaseService = purchaseService;
            _supplierService = supplierService;
            _configuration = configuration;
            _fifoService = fifoService;
            _qrcodeService = qrcodeService;
            _inventoryService = inventoryService;
            _indentprimaryService = indentprimaryService;
            _purchaseOrderService = purchaseOrderService;
            _purchaseDetailService = purchaseDetailService;
            _qc = qc;
            _ipo = ipo;
            _issu = issu;
            _qrCodePic = qrCodePic;
            _cd = cd;
            _user = user;
            _log = log;
        }

        /// <summary>
        /// 物料处理信息分页
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("getList")]
        public IResponseDto GetPageList([FromQuery] MaterialOperateSearchDto search)
        {
            var result = _purchaseService.GetPagedList(search);
            return ResponseDto.Success(result);
        }
        /// <summary>
        /// 根据BomId获取订单明细集合
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("GetOperateList")]
        public IResponseDto GetOperateList([FromQuery] MaterialOperateSearchDto search)
        {
            var result = _purchaseService.GetOperateList(search);
            return ResponseDto.Success(result);
        }
        /// <summary>
        /// 供应商分页
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("SupplierPageList")]
        public IResponseDto SupplierPageList([FromQuery] SupplierSearchDto search)
        {
            var result = _supplierService.PagedList(search);
            return ResponseDto.Success(result);
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("doAdd")]
        public IResponseDto doAdd([FromBody] MaterialOperateDto input)
        {
            var IsExist = _purchaseService.IsExist(input);
            if (IsExist)
            {
                return ResponseDto.Error(500, "BOM中已存在相同物料");
            }
            var entity = _purchaseService.Insert(input);
            return entity.Id > 0 ? ResponseDto.Success("添加成功") : ResponseDto.Error(0, ExceptionCode.InsertFailed.ToString());
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        public IResponseDto doEdit([FromBody] MaterialOperateDto input)
        {
            if (input.Id.HasValue)
            {
                var IsExist = _purchaseService.IsExist(input);
                if (IsExist)
                {
                    return ResponseDto.Error(500, "BOM中已存在相同物料");
                }
                var result = _purchaseService.Update(input.Id.Value, input);
                return result > 0 ? ResponseDto.Success("修改成功") : ResponseDto.Error(0, ExceptionCode.UpdateFailed.ToString());
            }
            return ResponseDto.Error(0, ExceptionCode.UpdateFailed.ToString());
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpDelete]
        public IResponseDto doDelete([FromBody] DeleteRequesDto input)
        {
            var ids = input.GetIds();
            var result = _purchaseService.Delete(ids);
            return result > 0 ? ResponseDto.Success("删除成功") : ResponseDto.Error(0, ExceptionCode.DeleteFailed.ToString());
        }

        /// <summary>
        /// 批量编辑
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        [HttpPost("doBatchEdit")]
        public IResponseDto doBatchEdit([FromBody] MaterialOperateDto[] inputs)
        {
            int count = 0;
            if (inputs.Length > 0)
            {
                for (int i = 0; i < inputs.Length; i++)
                {
                    int co = _purchaseService.Update(inputs[i].Id.Value, inputs[i]);
                    count += co;
                }

            return ResponseDto.Success("成功更新" + count + "条数据!");
            }
            else
            {
                return ResponseDto.Error(0, "没有可操作的数据!");
            }
        }

        ///// <summary>
        ///// 批量编辑采购人员（分配人员）
        ///// </summary>
        ///// <param name="Ids"></param>
        ///// <returns></returns>
        //[HttpPost("doAssignStaff")]
        //public IResponseDto doAssignStaff([FromBody] MaterialOperateDto[] inputs)
        //{
        //    for (int i = 0; i < inputs.Length; i++)
        //    {
        //        _purchaseService.Update(inputs[i].Id.Value, inputs[i]);
        //    }
        //    return ResponseDto.Success("修改成功");
        //}

        /// <summary>
        /// 供应商新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("SupplierAdd")]
        public IResponseDto SupplierAdd([FromBody] SupplierDto input)
        {
            var IsExist = _supplierService.IsExist(input);
            if (IsExist)
            {
                return ResponseDto.Error(500, "存在相同供应商编码");
            }
            var entity = _supplierService.Insert(input);
            return entity.Id > 0 ? ResponseDto.Success("添加成功") : ResponseDto.Error(0, ExceptionCode.InsertFailed.ToString());
        }
        /// <summary>
        /// 供应商修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("SupplierEdit")]
        public IResponseDto SupplierEdit([FromBody] SupplierDto input)
        {

            if (input.Id.HasValue)
            {
                var IsExist = _supplierService.IsExist(input);
                if (IsExist)
                {
                    return ResponseDto.Error(500, "存在相同供应商编码");
                }
                var result = _supplierService.Update(input.Id.Value, input);
                return result > 0 ? ResponseDto.Success("修改成功") : ResponseDto.Error(0, ExceptionCode.UpdateFailed.ToString());
            }
            return ResponseDto.Error(0, ExceptionCode.UpdateFailed.ToString());
        }
        /// <summary>
        /// 供应商删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpDelete("SupplierDel")]
        public IResponseDto SupplierDelete([FromBody] DeleteRequesDto input)
        {
            var ids = input.GetIds();
            var result = _supplierService.Delete(ids);
            return result > 0 ? ResponseDto.Success("删除成功") : ResponseDto.Error(0, ExceptionCode.DeleteFailed.ToString());
        }
        /// <summary>
        /// 查询所有的供应商信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("AllSupplierList")]
        public IResponseDto AllSupplierList()
        {
            var list = _supplierService.GetList().Where(o => o.IsDeleted == false).ToList();
            return ResponseDto.Success(list);
        }

        /// <summary>
        /// 供应商修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("OptSupplierEdit")]
        public IResponseDto OptSupplierEdit(string ids, string supCode)
        {
            var result = _purchaseService.OptSupplierEdit(ids, supCode);
            if (result)
                return ResponseDto.Success("修改成功");
            else
                return ResponseDto.Success("修改失败");
        }

        /// <summary>
        /// 给质检人员发送物料检验消息
        /// </summary>
        /// <param name="bomcode"></param>
        /// <param name="materialcode"></param>
        /// <returns></returns>
        [HttpPost("assignStaffFeed")]
        public IResponseDto assignStaffFeed(string bomcode, string materialcode, string time)
        {
            string esburl = _configuration["ESBUrl:key"] + "/sendMessage";
            string plmurl = _configuration["PLMUrl:key"];
            var result = _purchaseService.MessagePush(bomcode, materialcode, plmurl, esburl, time);
            if (!result) return ResponseDto.Error(-1, "发送失败");
            return ResponseDto.Success("发送成功");
        }

        /// <summary>
        /// 成本价格
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("CostPriceEdit")]
        public IResponseDto CostPriceEdit(string ids, decimal costPrice)
        {
            var result = _purchaseService.CostPriceEdit(ids, costPrice);
            return ResponseDto.Success(result);
        }

        /// <summary>
        /// 下单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //[HttpPost("doShop")]
        //public IResponseDto DoShopping(string ids, string bomCode)
        //{
        //    bool result = false;
        //    string[] strArray = ids.Split(','); //字符串转数组
        //    var bomList = _materialService.PDM_BOMList().Where(o => o.BOMCode == bomCode).FirstOrDefault();
        //    var bomListItem = _materialService.ByBomToTreeList().Where(o => o.BOMCode == bomCode).ToList();
        //    List<Mat_OperateEntity> list = new List<Mat_OperateEntity>();
        //    if (bomList != null && bomListItem.Count > 0)
        //    {
        //        foreach (var item in strArray)
        //        {
        //            var IsExist = _purchaseService.IsExist(new MaterialOperateDto { BOMCode = bomCode, MaterialCode = item });
        //            if (!IsExist)
        //            {
        //                var son = bomListItem.Where(o => o.BOMCode == bomCode && o.MaterialCode == item.ToString()).FirstOrDefault();
        //                Mat_OperateEntity dto = new Mat_OperateEntity();
        //                dto.BOMCode = bomCode;
        //                dto.MaterialCode = item.ToString();
        //                dto.MaterialName = son == null ? "" : son.MaterialName;
        //                dto.Count = son == null ? 0 : son.Count;
        //                dto.CostPrice = 0;
        //                dto.State = "material_1";//状态为已下单
        //                dto.ProjectCode = son == null ? "" : son.ProjectCode;
        //                dto.ProjectName = son == null ? "" : son.ProjectName;
        //                list.Add(dto);
        //            }
        //        }
        //        result = _purchaseService.batchAdd(list);
        //    }
        //    else
        //    {
        //        result = false;
        //    }
        //    return result == true ? ResponseDto.Success("新增成功") : ResponseDto.Error(0, ExceptionCode.InsertFailed.ToString());
        //}

        #region 采购出入库信息

        /// <summary>
        /// 出入库信息分页
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("FifoPageList")]
        public IResponseDto FifoPageList([FromQuery] FifoSearchDto search)
        {
            var result = _fifoService.FifoPagedList(search);
            return ResponseDto.Success(result);
        }

        /// <summary>
        /// 出入库信息删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpDelete("FifoDelete")]
        public IResponseDto FifoDelete([FromBody] DeleteRequesDto input)
        {
            var ids = input.GetIds();
            var result = _fifoService.Delete(ids);
            return result > 0 ? ResponseDto.Success("删除成功") : ResponseDto.Error(0, ExceptionCode.DeleteFailed.ToString());
        }

        /// <summary>
        /// 出入库新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("FifoAdd")]
        public IResponseDto FifoAdd([FromBody] FifoDto input)
        {
            string fifotext = input.Type == 1 ? "入库" : "出库";
            int InventoryNumber = 0;//出入库后的库存数量
            input.FifoDateTime = DateTime.Now;
            var inventory = _inventoryService.GetInventoryInfo(input.MaterialCode);
            //有库存信息
            if (inventory != null)
            {
                //入库
                if (input.Type == 1)
                {
                    InventoryNumber = inventory.WarehousCount + input.Count;
                }
                else //出库
                {
                    InventoryNumber = inventory.WarehousCount - input.Count;
                    if (InventoryNumber < 0)
                    {
                        return ResponseDto.Success("出库数量超出库存!");
                    }
                }
                _inventoryService.InvCountEdit(InventoryNumber, input.MaterialCode);

            }
            else //无库存信息
            {
                InventoryNumber = input.Count;
                //如果入库没有库存信息时需要增加一条
                _inventoryService.Insert(new InventoryDto
                {
                    MaterialCode = input.MaterialCode,
                    MaterialName = input.MaterialName,
                    ProjectCode = input.ProjectCode,
                    ProjectName = input.ProjectName,
                    WarehousCount = InventoryNumber
                });

            }
            var entity = _fifoService.Insert(input);
            return entity.Id > 0 ? ResponseDto.Success(fifotext + "成功") : ResponseDto.Error(0, ExceptionCode.InsertFailed.ToString());
        }
        /// <summary>
        /// 出入库修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("FifoEdit")]
        public IResponseDto FifoEdit([FromBody] FifoDto input)
        {

            if (input.Id.HasValue)
            {
                var IsExist = _fifoService.IsExist(input);
                if (IsExist)
                {
                    return ResponseDto.Error(500, input.MaterialName + "重复入库!");
                }
                var result = _fifoService.Update(input.Id.Value, input);
                return result > 0 ? ResponseDto.Success("修改成功") : ResponseDto.Error(0, ExceptionCode.UpdateFailed.ToString());
            }
            return ResponseDto.Error(0, ExceptionCode.UpdateFailed.ToString());
        }

        #endregion

        #region 二维码信息库
        /// <summary>
        /// 二维码信息库分页
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("QrCodePageList")]
        public IResponseDto QrCodePageList([FromQuery] QrCodeSearchDto search)
        {
            var result = _qrcodeService.PagedList(search);
            return ResponseDto.Success(result);
        }

        /// <summary>
        /// 二维码信息导出
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("PrintQrCodeList")]
        public IResponseDto PrintQrCodeList([FromQuery] QrCodeSearchDto search)
        {
            var result = _qrcodeService.PrintQrCodeList(search);
            return ResponseDto.Success(result);
        }

        /// <summary>
        /// 查询所有二维码数据信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetQrAllList")]
        public IResponseDto GetQrAllList()
        {
            var list = _qrcodeService.GetList();
            List<BaseCommonOutput> result = new List<BaseCommonOutput>();
            for (int i = 0; i < list.Count; i++)
            {
                BaseCommonOutput baseCommonOutput = new()
                {
                    label = list[i].QrCode,
                    value = list[i].Id
                };
                result.Add(baseCommonOutput);
            }
            return ResponseDto.Success(result);
        }

        /// <summary>
        /// 二维码信息删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpDelete("QrCodeDelete")]
        public IResponseDto QrCodeDelete([FromBody] DeleteRequesDto input)
        {
            var ids = input.GetIds();
            var result = _qrcodeService.Delete(ids);
            return result > 0 ? ResponseDto.Success("删除成功") : ResponseDto.Error(0, ExceptionCode.DeleteFailed.ToString());
        }

        /// <summary>
        /// 二维码新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("QrCodeAdd")]
        public IResponseDto QrCodeAdd([FromBody] QrCodeDto input)
        {
            var IsExist = _qrcodeService.IsExist(input);
            if (IsExist)
            {
                return ResponseDto.Error(500, "重复操作!");
            }
            var entity = _qrcodeService.Insert(input);
            return entity.Id > 0 ? ResponseDto.Success("添加成功") : ResponseDto.Error(0, ExceptionCode.InsertFailed.ToString());
        }

        /// <summary>
        /// 二维码新增（批量）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("QrCodeAddPL")]
        public IResponseDto QrCodeAddPL([FromBody] IndentPrimaryDto input)
        {
            //查询二维码信息数据
            var entity = _qrcodeService.GetList().Where(o => o.BomId == int.Parse(input.BomId)).ToList();
            MaterialOperateSearchDto dto = new() { BomId = int.Parse(input.BomId) };
            var list = _purchaseService.GetOperateList(dto);
            //
            if (entity.Count == 0 || entity.Count < list.Count)
            {
                int num1 = list.Count,
                        num2 = 0;
                foreach (var item in list)
                {
                    var exist = entity.Where(o => o.MaterialCode == item.MaterialCode).FirstOrDefault();
                    if (exist == null)
                    {
                        var add = _qrcodeService.Insert(new QrCodeDto
                        {
                            QrCode = item.MaterialCode + item.Id,
                            Supplier = item.Supplier,
                            MaterialCode = item.MaterialCode,
                            MaterialName = item.MaterialName,
                            ProjectCode = input.ProjectCode,
                            ProjectName = input.ProjectName,
                            BomID = item.BomId,
                            BomCode = input.BomCode,
                            Count = item.Count,
                            State = 1,
                            qualified = 0,
                            Remark = ""
                        });
                        num2 = add.Count > 0 ? num2++ : num2;
                    }
                }
                return num2 > 0 ? ResponseDto.Success("成功新增" + num2 + "条数据!") : ResponseDto.Error(0, ExceptionCode.InsertFailed.ToString());
            }
            else if (entity.Count > 0 && entity.Count == list.Count)
            {
                return ResponseDto.Error(0, "请勿重复操作!");
            }
            else
            {
                return ResponseDto.Error(0, "批量新增异常!");
            }
        }

        /// <summary>
        /// 二维码修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("QrCodeEdit")]
        public IResponseDto QrCodeEdit([FromBody] QrCodeDto input)
        {

            if (input.Id.HasValue)
            {
                var IsExist = _qrcodeService.IsExist(input);
                if (IsExist)
                {
                    return ResponseDto.Error(500, "[" + input.QrCode + "]信息重复操作!");
                }
                var result = _qrcodeService.Update(input.Id.Value, input);
                return result > 0 ? ResponseDto.Success("修改成功") : ResponseDto.Error(0, ExceptionCode.UpdateFailed.ToString());
            }
            return ResponseDto.Error(0, ExceptionCode.UpdateFailed.ToString());
        }

        /// <summary>
        /// 根据二维码号查询二维码信息库信息
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("GetByIdQrCode")]
        public IResponseDto GetByIdQrCode(string QrCode, int Type)
        {
            try
            {
                var result = _qrcodeService.GetByQrCode(QrCode);
                if (result != null)
                {
                    var fifo = _fifoService.GetByQrCodeFifo(QrCode, Type);
                    if (fifo == null)
                    {
                        result.type = 1;
                    }
                    else
                    {
                        //已存在入库
                        if (fifo.Type == 1)
                        {
                            result.type = 2;
                        }
                        else //已存在出库
                        {
                            result.type = 0;
                        }
                    }
                }
                return ResponseDto.Success(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 库存信息
        /// <summary>
        /// 库存信息分页
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("InventoryPageList")]
        public IResponseDto InventoryPageList([FromQuery] InventorySearchDto search)
        {
            var result = _inventoryService.InventoryPagedList(search);
            return ResponseDto.Success(result);
        }

        /// <summary>
        /// 库存信息删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpDelete("InventoryDelete")]
        public IResponseDto InventoryDelete([FromBody] DeleteRequesDto input)
        {
            var ids = input.GetIds();
            var result = _inventoryService.Delete(ids);
            return result > 0 ? ResponseDto.Success("删除成功") : ResponseDto.Error(0, ExceptionCode.DeleteFailed.ToString());
        }

        /// <summary>
        ///库存信息新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("InventoryAdd")]
        public IResponseDto InventoryAdd([FromBody] InventoryDto input)
        {
            var info = _inventoryService.GetInventoryInfo(input.MaterialCode);
            if (info != null)
            {
                int count = input.WarehousCount + info.WarehousCount;
                bool result = _inventoryService.InvCountEdit(count, input.MaterialCode);
                return result == true ? ResponseDto.Success("更新成功") : ResponseDto.Error(0, ExceptionCode.InsertFailed.ToString());
            }
            else
            {
                var entity = _inventoryService.Insert(input);
                return entity.Id > 0 ? ResponseDto.Success("添加成功") : ResponseDto.Error(0, ExceptionCode.InsertFailed.ToString());
            }
        }
        /// <summary>
        /// 库存信息修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("InventoryEdit")]
        public IResponseDto InventoryEdit([FromBody] InventoryDto input)
        {

            if (input.Id.HasValue && input.WarehousCount > 0)
            {
                var result = _inventoryService.Update(input.Id.Value, input);
                return result > 0 ? ResponseDto.Success("修改成功") : ResponseDto.Error(0, ExceptionCode.UpdateFailed.ToString());
            }
            else
            {
                return ResponseDto.Error(500, "更新异常！");
            }
        }
        #endregion

        #region BOM订单主表信息
        /// <summary>
        /// BOM订单主表分页
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("IMPageList")]
        public IResponseDto IMPageList([FromQuery] IndentPrimarySearchDto search)
        {
            var result = _indentprimaryService.PagedList(search);
            return ResponseDto.Success(result);
        }
        /// <summary>
        /// BOM订单主表新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("IMdoAdd")]
        public IResponseDto IMdoAdd([FromBody] IndentPrimaryDto input)
        {
            try
            {
                var entity = _indentprimaryService.Insert(input);
                return entity.Id > 0 ? ResponseDto.Success("添加成功") : ResponseDto.Error(0, ExceptionCode.InsertFailed.ToString());
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// BOM订单主表修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("IMdoEdit")]
        public IResponseDto IMdoEdit([FromBody] IndentPrimaryDto input)
        {
            if (input.Id.HasValue)
            {
                try
                {
                    var result = _indentprimaryService.Update(input.Id.Value, input);
                    return result > 0 ? ResponseDto.Success("修改成功") : ResponseDto.Error(0, ExceptionCode.UpdateFailed.ToString());
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return ResponseDto.Error(0, ExceptionCode.UpdateFailed.ToString());
        }
        /// <summary>
        /// BOM订单主表删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpDelete("IMdoDelete")]
        public IResponseDto IMdoDelete([FromBody] DeleteRequesDto input)
        {
            var ids = input.GetIds();
            var result = _indentprimaryService.Delete(ids);
            return result > 0 ? ResponseDto.Success("删除成功") : ResponseDto.Error(0, ExceptionCode.DeleteFailed.ToString());
        }
        #endregion
        /// <summary>
        /// 根据ID查询成本价格历史数据（前5条）
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("GetByIdList")]
        public IResponseDto GetByIdList(int Id)
        {
            var result = _purchaseService.GetByIdList(Id);
            return ResponseDto.Success(result);
        }

        /// <summary>
        /// 根据ID查询供应商历史数据（前5条）
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("GetByIdSupplier")]
        public IResponseDto GetByIdSupplier(int Id)
        {
            var result = _purchaseService.GetByIdSupplier(Id);
            return ResponseDto.Success(result);
        }

        #region 采购订单申请

        /// <summary>
        /// 新增采购订单
        /// </summary>
        /// <param name = "input" ></ param >
        /// < returns ></ returns >
        [HttpPost("doPurOrderAdd")]
        public IResponseDto doPurOrderAdd([FromBody] Mat_PurchaseOrderDto input)
        {
            var IsExist = _purchaseOrderService.IsExist(input);
            if (IsExist)
            {
                return ResponseDto.Error(500, "已存在相同申请单号");
            }
            var entity = _purchaseOrderService.Insert(input);
            input.purDetaileData.ForEach(w => w.ApplicationNo = input.ApplicationNo);
            doPurDetailAdd(input.purDetaileData);
            return entity.Id > 0 ? ResponseDto.Success("添加成功") : ResponseDto.Error(0, ExceptionCode.InsertFailed.ToString());
        }

        /// <summary>
        /// 新增采购订单
        /// </summary>
        /// <param name = "input" ></ param >
        /// < returns ></ returns >
        [HttpPost("doPurDetailAdd")]
        public void doPurDetailAdd(List<Mat_PurchaseDetailDto> detail)
        {
            foreach (var item in detail)
            {
                _purchaseDetailService.Insert(item);
            }
        }


        #endregion

        /// <summary>
        /// 批量出库
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("BatchCK")]
        public IResponseDto BatchCK([FromBody] List<FifoDto> FifoList)
        {
            int count = 0;
            foreach (var item in FifoList)
            {
                var meet = _fifoService.GetByQrCodeFifo(item.QrCode, item.Type);
                if (meet == null)
                {
                    var entity = _fifoService.Insert(new FifoDto
                    {
                        Type = item.Type,
                        BomCode = item.BomCode,
                        BomName = item.BomName,
                        ProjectCode = item.ProjectCode,
                        ProjectName = item.ProjectName,
                        Count = item.Count,
                        MaterialCode = item.MaterialCode,
                        MaterialName = item.MaterialName,
                        QrCode = item.QrCode,
                        FifoDateTime = DateTime.Now,
                        FifoPersonnel = item.FifoPersonnel,
                        DeliverySignature = item.DeliverySignature
                    });
                    if (entity.Id > 0)
                    {
                        _inventoryService.InvCountEdit(item.Type, entity.Count, item.MaterialCode);
                        _issu.Insert(new ProductIssuDto
                        {
                            QrCode = item.QrCode,
                            ProjectCode = item.ProjectCode,
                            ProjectName = item.ProjectName,
                            Count = item.Count,
                            MaterialCode = item.MaterialCode,
                            MaterialName = item.MaterialName,
                            State = 0,
                            LLMan = item.FifoPersonnel
                        });
                        count++;
                    }
                }

            }
            return count > 0 ? ResponseDto.Success("成功出库" + count + "条数据") : ResponseDto.Error(0, ExceptionCode.InsertFailed.ToString());
        }

        /// <summary>
        /// 分量出库
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Count"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("FenLiangCK")]
        public IResponseDto FenLiangCK([FromBody] FifoDto item)
        {
            var entity = _fifoService.Insert(new FifoDto
            {
                Type = item.Type,
                BomCode = item.BomCode,
                BomName = item.BomName,
                ProjectCode = item.ProjectCode,
                ProjectName = item.ProjectName,
                Count = item.Count,
                MaterialCode = item.MaterialCode,
                MaterialName = item.MaterialName,
                QrCode = item.QrCode,
                FifoDateTime = DateTime.Now,
                FifoPersonnel = item.FifoPersonnel,
                DeliverySignature = item.DeliverySignature,
                Remark = item.Remark
            });
            if (entity.Id > 0)
            {
                _inventoryService.InvCountEdit(item.Type, item.Count, item.MaterialCode);
                _issu.Insert(new ProductIssuDto
                {
                    QrCode = item.QrCode,
                    ProjectCode = item.ProjectCode,
                    ProjectName = item.ProjectName,
                    Count = item.Count,
                    MaterialCode = item.MaterialCode,
                    MaterialName = item.MaterialName,
                    State = 0,
                    LLMan = item.FifoPersonnel
                });
            }
            return entity.Id > 0 ? ResponseDto.Success("出库成功") : ResponseDto.Error(0, ExceptionCode.InsertFailed.ToString());
        }

        /// <summary>
        /// 质检信息分页
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("QCPageList")]
        public IResponseDto QCPageList([FromQuery] QCSearchDto search)
        {
            var result = _qc.QCPagedList(search);
            return ResponseDto.Success(result);
        }

        /// <summary>
        /// 质检信息删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpDelete("QCDelete")]
        public IResponseDto QCDelete([FromBody] DeleteRequesDto input)
        {
            var ids = input.GetIds();
            var result = _qc.Delete(ids);
            return result > 0 ? ResponseDto.Success("删除成功") : ResponseDto.Error(0, ExceptionCode.DeleteFailed.ToString());
        }

        /// <summary>
        /// 质检信息修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("QCEdit")]
        public IResponseDto QCEdit([FromBody] QCDto input)
        {

            if (input.Id.HasValue && input.QCount > 0)
            {
                var result = _qc.Update(input.Id.Value, input);
                return result > 0 ? ResponseDto.Success("修改成功") : ResponseDto.Error(0, ExceptionCode.UpdateFailed.ToString());
            }
            else
            {
                return ResponseDto.Error(500, "更新异常");
            }
        }

        /// <summary>
        /// 采购申请列表分页
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("SQDPageList")]
        public IResponseDto SQDPageList([FromQuery] PurchaseOrderSearchDto search)
        {
            var result = _purchaseOrderService.GetPagedList(search);
            return ResponseDto.Success(result);
        }

        /// <summary>
        /// 采购申请删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpDelete("SQDDelete")]
        public IResponseDto SQDDelete([FromBody] DeleteRequesDto input)
        {
            var ids = input.GetIds();
            var result = _purchaseOrderService.Delete(ids);
            return result > 0 ? ResponseDto.Success("删除成功") : ResponseDto.Error(0, ExceptionCode.DeleteFailed.ToString());
        }

        /// <summary>
        /// 采购订单审核
        /// </summary>
        /// <param name = "input" ></ param >
        /// < returns ></ returns >
        [HttpPost("OrderSH")]
        public IResponseDto OrderSH([FromBody] Mat_PurchaseOrderDto input)
        {
            var IsExist = _purchaseOrderService.IsExist(input);
            if (IsExist)
            {
                return ResponseDto.Error(500, "已存在相同申请单号");
            }
            var entity = _purchaseOrderService.Insert(input);
            if (entity.Id > 0)
            {
                var list = _purchaseDetailService.GetList().Where(o => o.ApplicationNo == entity.ApplicationNo);
                if (list.Any())
                {
                    foreach (var item in list)
                    {
                        _ipo.Insert(new
                        {
                            BomId = 0,
                            MaterialCode = item.Code,
                            MaterialName = item.Name,
                            Count = item.Count,
                            DrawingCode = string.Empty,
                            BOMCode = string.Empty,
                            BomName = string.Empty,
                            ProjectCode = string.Empty,
                            ProjectName = string.Empty,
                            TraceabilityCode = string.Empty
                        });
                    }
                }
            }
            return entity.Id > 0 ? ResponseDto.Success("添加成功") : ResponseDto.Error(0, ExceptionCode.InsertFailed.ToString());
        }

        /// <summary>
        /// 申请列表明细集合
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("PurchaseDetailList")]
        public IResponseDto PurchaseDetailList(string ApplicationNo)
        {
            var list = _purchaseDetailService.GetList().Where(o => o.ApplicationNo == ApplicationNo);
            return ResponseDto.Success(list);
        }

        /// <summary>
        /// 待确认领料信息列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("IssulList")]
        public IResponseDto IssulList(string LLMan)
        {
            var list = _issu.GetList().Where(o => o.LLMan == LLMan && o.State == 0);
            return ResponseDto.Success(list);
        }

        /// <summary>
        /// 新增质检信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("DoQCAdd")]
        public IResponseDto DoQCAdd([FromBody] QCDto input)
        {
            try
            {
                if (input.QCount == 0 && input.Qualified == 1)
                {
                    return ResponseDto.Error(500, "合格数量不能为0!");
                }
                var qrcode = _qrcodeService.GetByQrCode(input.QrCode);
                if (qrcode != null)
                {
                    var bo = _qc.QcInfo(input.QrCode);
                    if (bo != null) //存在质检信息做修改
                    {
                        if ((input.QCount + bo.QCount) <= input.Count)
                        {
                            bo.QCount += input.QCount;
                            bo.Qualified = input.Qualified;
                            int yxh = _qc.Update(bo.Id.Value, bo);
                            if (yxh > 0)
                            {
                                if (qrcode.qualified != input.Qualified)
                                {
                                    _qrcodeService.UpdateMQualified(input.QrCode, input.Qualified);
                                }
                                return ResponseDto.Success("更新成功");
                            }
                        }
                        else
                        {
                            return ResponseDto.Error(500, "已超出质检数量!");
                        }
                    }
                    else //否则新增质检信息
                    {
                        var entity = _qc.Insert(input);
                        if (entity.Id > 0)
                        {
                            _qrcodeService.UpdateMQualified(input.QrCode, input.Qualified);
                            _qrCodePic.UpdateImgPid(input.ImgFilesId, entity.Id);
                        }
                        else
                        {
                            ResponseDto.Error(0, ExceptionCode.InsertFailed.ToString());
                        }
                    }
                    return ResponseDto.Success("添加成功");
                }
                else
                {
                    return ResponseDto.Error(500, "操作异常");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 根据二维码号查询质检信息
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("GetByQrCodeQC")]
        public IResponseDto GetByQrCodeQC(string QrCode)
        {
            try
            {
                var result = _qc.QcInfo(QrCode);
                return ResponseDto.Success(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 根据二维码号查询报检信息
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("GetByQrCodeCD")]
        public IResponseDto GetByQrCodeCD(string QrCode)
        {
            try
            {
                var result = _cd.CDInfo(QrCode);
                return ResponseDto.Success(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 新增报检信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("DoCDAdd")]
        public IResponseDto DoCDAdd([FromBody] CDDto input)
        {
            var info = _cd.CDInfo(input.QrCode);
            if (info == null)
            {
                var entity = _cd.Insert(input);
                return entity.Id > 0 ? ResponseDto.Success("报检成功") : ResponseDto.Error(0, ExceptionCode.InsertFailed.ToString());
            }
            else
            {
                return ResponseDto.Error(500, "报检失败(重复操作!)");
            }
        }

        /// <summary>
        /// 批量报检
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("PLCDAdd")]
        public IResponseDto PLCDAdd([FromBody] List<CDDto> list)
        {
            int num = 0;
            if (list.Any())
            {
                foreach (var item in list)
                {
                    var info = _cd.CDInfo(item.QrCode);
                    if (info == null)
                    {
                        var entity = _cd.Insert(item);
                        if (entity.Id > 0)
                            num += 1;

                    }
                }
            }
            return num > 0 ? ResponseDto.Success("报检成功") : ResponseDto.Error(0, "报检失败");
        }

        /// <summary>
        /// 分配人员消息通知
        /// </summary>
        /// <param name="ProjectCode"></param>
        /// <param name="ProjectName"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        [HttpPost("SendMessagePurchase")]
        public IResponseDto SendMessagePurchase(string ProjectCode, string ProjectName, string UserID)
        {
            int uid = Convert.ToInt32(UserID);
            if (uid > 0)
            {
                var user = _user.GetUserInfoById(uid);
                if (user != null)
                {
                    string[] arr = { user.Account };
                    var  result = _purchaseService.SendMessagePurchase("/purchase/indent", "您有一条" + ProjectName + "(" + ProjectCode + ")项目上的采购分配任务!", arr);
                    _log.AddOperationLog(true, "向" + UserID + "发送了一条消息", result);
                }
                return ResponseDto.Success("消息发送成功!");
            }
            else
            {
                return ResponseDto.Error(0, "消息发送失败!");
            }
        }
    }
}

