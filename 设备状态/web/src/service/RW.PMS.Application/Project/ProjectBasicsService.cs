using AutoMapper;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Project;
using RW.PMS.Application.Contracts.Input.Message;
using RW.PMS.Application.Contracts.Message;
using RW.PMS.Application.Contracts.Project;
using RW.PMS.Application.Contracts.System;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.Exceptions;
using RW.PMS.CrossCutting.Extensions;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.Probelem;
using RW.PMS.Domain.Entities.Project;
using RW.PMS.Domain.Entities.System;
using RW.PMS.Domain.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RW.PMS.Application.Project
{
    public class ProjectBasicsService : CrudApplicationService<ProjectBasicsEntity, int>, IProjectBasicsService
    {
        private readonly IContract_InfoService _contract_InfoService;
        private readonly IProjectInvoicingService _projectInvoicingService;
        private readonly IProjectDeviceDetailsService _projectDeviceDetailsService;
        private readonly IProjectRetMoneyService _projectRetMoneyService;
        private readonly IPayment_CollectionService _payment_CollectionService;
        private readonly IProjectContactsService _projectContactsService;
        private readonly IThirdPartService _thirdPartService;
        private readonly ILogService _logService;
        private List<ProjectBasicsEntity> AddList = new List<ProjectBasicsEntity>();

        public ProjectBasicsService(
            IDataValidatorProvider dataValidator,
            IRepository<ProjectBasicsEntity, int> repository,
            IPayment_CollectionService payment_CollectionService, IProjectRetMoneyService projectRetMoneyService, IProjectContactsService projectContactsService,
            IMapper mapper, IProjectDeviceDetailsService projectDeviceDetailsService,
             IContract_InfoService contract_InfoService, IProjectInvoicingService projectInvoicingService,
            IThirdPartService thirdPartService,
            ILogService logService,
            Lazy<ICurrentUser> currentUser) : base(dataValidator, repository, mapper, currentUser)
        {
            _contract_InfoService = contract_InfoService;
            _projectInvoicingService = projectInvoicingService;
            _projectDeviceDetailsService = projectDeviceDetailsService;
            _payment_CollectionService = payment_CollectionService;
            _projectRetMoneyService = projectRetMoneyService;
            _projectContactsService = projectContactsService;
            _thirdPartService = thirdPartService;
            _logService = logService;
        }
        /// <summary>
        /// 项目基础信息分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PagedResult<ProjectBasicsView> PagedList(ProjectBasicsSearchDto input)
        {
            string[] DateArray = input.ProjectReceiveDate.NotNullOrWhiteSpace() ? input.ProjectReceiveDate.Split("~") : null;
            var PersonInChargeList = _projectContactsService.GetAllList();
            var user = PersonInChargeList.Where(o => o.ContactsName == input.account).ToList();
            var list = Repository.Orm.Select<ProjectBasicsEntity, DictionaryEntity, DictionaryEntity, Client_CompanyEntity, DictionaryEntity,
                DictionaryEntity, bd_SalesAreaInfoEntity, DictionaryEntity>()
                .LeftJoin(a1 => a1.t1.ProjectClass == a1.t2.Id)
                .LeftJoin(a2 => a2.t1.BusinessType == a2.t3.Id)
                .LeftJoin(a3 => a3.t1.ClientCompany == a3.t4.Id)
                .LeftJoin(a4 => a4.t1.ProManagementStyle == a4.t5.Id)
                .LeftJoin(a5 => a5.t1.ProState == a5.t6.Id)
                .LeftJoin(t => t.t4.MarketArea == t.t7.Id)
                .LeftJoin(t => t.t1.ProjectStatus == t.t8.Id)
                .WhereIf(input.ProjectName.NotNullOrWhiteSpace(), a => a.t1.ProjectName.Contains(input.ProjectName))
                .WhereIf(input.ProjectCode.NotNullOrWhiteSpace(), a => a.t1.ProjectCode.Contains(input.ProjectCode))
                .WhereIf(input.ProjectClass == 30, a => a.t1.ProjectClass == 30)
                .WhereIf(input.ProjectClass == 31, a => a.t1.ProjectClass != 30)
                .WhereIf(input.BusinessType > 0, a => a.t1.BusinessType == input.BusinessType)
                .WhereIf(input.UrgentGrade > 0 && input.UrgentGrade == 1, a => a.t1.UrgentGrade == 1 || a.t1.UrgentGrade == 2)
                .WhereIf(input.UrgentGrade > 0 && input.UrgentGrade == 2, a => a.t1.UrgentGrade == 3 || a.t1.UrgentGrade == 4)
                .WhereIf(input.UrgentGrade > 0 && input.UrgentGrade == 3, a => a.t1.UrgentGrade == 5)
                .WhereIf(input.ClientCompany > 0, a => a.t1.ClientCompany == input.ClientCompany)
                .WhereIf(input.ProManagementStyle > 0, a => a.t1.ProManagementStyle == input.ProManagementStyle)
                .WhereIf(input.ProState > 0, a => a.t1.ProState == input.ProState)
                .WhereIf(input.marketArea.NotNullOrWhiteSpace(), a => a.t7.Id == Convert.ToInt32(input.marketArea))
                .WhereIf(input.ProjectReceiveDate.NotNullOrWhiteSpace(), t =>
                  t.t1.ProjectReceiveDate >= Convert.ToDateTime(DateArray[0].ToString()) && t.t1.ProjectReceiveDate < Convert.ToDateTime(DateArray[1].ToString()).AddDays(1))
                .Where(a => a.t1.IsDeleted == false && a.t1.ParentId == 0)
                .OrderByDescending(a => a.t1.Id)
                //.Count(out var total)
                //.Page(input.PageNo, input.PageSize)
                .ToList(t => new
                {
                    project = t.t1,
                    Dictionary1 = t.t2,
                    Dictionary2 = t.t3,
                    Dictionary3 = t.t4,
                    Dictionary4 = t.t5,
                    Dictionary5 = t.t6,
                    Dictionary7 = t.t7,
                    Dictionary8 = t.t8,
                    proFile = Repository.Orm.Select<pro_basics_filesEntity>().Where(b => b.PID == t.t1.Id).Count(),
                    Sum1 = Repository.Orm.Select<ProjectDeviceDetailsEntity>().Where(b => b.ProjectID == t.t1.Id).Sum(b => b.DevicePrice * b.Qty),
                    Sum2 = Repository.Orm.Select<ProjectInvoicingEntity>().Where(b => b.ProjectId == t.t1.Id).Sum(b => b.InvoiceAmount),
                    Sum3 = Repository.Orm.Select<Contract_InfoEntity>().Where(b => b.pt_id == t.t1.Id).Sum(b => b.ct_cash),
                    Sum4 = Repository.Orm.Select<Payment_CollectionEntity>().Where(b => b.Pt_Id == t.t1.Id).Sum(b => b.Payment_Cash),
                    Sum5 = Repository.Orm.Select<ProjectRetMoneyEntity>().Where(b => b.ProjectID == t.t1.Id).Sum(b => b.RetMoneyAmount),
                    Sum6 = Repository.Orm.Select<ProjectRetMoneyEntity>().Where(b => b.ProjectID == t.t1.Id).Sum(b => b.AlrExpirationMoney),
                    Sum7 = Repository.Orm.Select<ProjectRetMoneyEntity>().Where(b => b.ProjectID == t.t1.Id).Sum(b => b.RetMoneyProportion),
                    Count7 = Repository.Orm.Select<ProjectRetMoneyEntity>().Where(b => b.ProjectID == t.t1.Id).Count(),
                    count1 = Repository.Where(s => s.ParentId == t.t1.Id).Count()
                }).Select(t =>
                {
                    var Output = Mapper.Map<ProjectBasicsView>(t.project);
                    Output.ProjectBasicFileId = t.proFile > 0;
                    Output.ProjectClassName = (t.Dictionary1 == null) ? "" : t.Dictionary1.DictionaryText;
                    Output.BusinessTypeName = (t.Dictionary2 == null) ? "" : t.Dictionary2.DictionaryText;
                    Output.ClientCompanyName = (t.Dictionary3 == null) ? "" : t.Dictionary3.ClientName;
                    Output.MarketAreaTxt = (t.Dictionary7 == null) ? "" : t.Dictionary7.AreaName;
                    Output.ProManagementStyleName = (t.Dictionary4 == null) ? "" : t.Dictionary4.DictionaryText;
                    Output.ProStateName = (t.Dictionary5 == null) ? "" : t.Dictionary5.DictionaryText;
                    Output.SalesPrice = Math.Round(t.Sum1, 2);
                    Output.ProSignedCtAmount = Math.Round(Output.SalesPrice - t.Sum3, 2);
                    Output.CtInvoicedAmount = Math.Round(Output.SalesPrice / Convert.ToDecimal(1.13), 2);
                    Output.ctInvoiceAmount = Math.Round(t.Sum2, 2);
                    Output.CtUnInvoicedAmount = Math.Round(Output.CtInvoicedAmount - Output.ctInvoiceAmount, 2);
                    Output.AmountPaymentCollection = Math.Round(t.Sum4, 2);
                    Output.Payment_Precent = Output.SalesPrice > 0 ? Math.Round(Output.AmountPaymentCollection / Output.SalesPrice, 2) : 0;
                    Output.RetentionAmount = Math.Round(t.Sum5, 2);
                    Output.PeriodMoney = Math.Round(t.Sum6, 2);
                    Output.RetentionRatio = t.Count7 > 0 ? Math.Round(t.Sum7 / t.Count7, 2) : 0;
                    int count = (int)t.count1;
                    if (count > 0)
                    {
                        Output.HasChildren = true;
                    }
                    else
                    {
                        Output.HasChildren = false;
                    }
                    Output.ProjectStatusName = (t.Dictionary8 == null) ? "" : t.Dictionary8.DictionaryText;

                    //获取营销经理
                    var ProContract = PersonInChargeList.Where(o => o.PID == t.project.Id);
                    var PersonInChargeName = ProContract.Where(o => o.FZBKName == "营销经理").FirstOrDefault();
                    Output.PersonInChargeName = PersonInChargeName == null ? "" : PersonInChargeName.ContactsName;
                    return Output;
                }).ToList();

            if (input.IsPeriodMoneyZero.NotNullOrWhiteSpace())
            {
                if (Convert.ToInt32(input.IsPeriodMoneyZero) == 1)
                {
                    list = list.Where(a => a.PeriodMoney > 0).ToList();
                }
            }
            if (input.PersonInCharge.NotNullOrWhiteSpace())
            {
                list = list.Where(a => a.PersonInChargeName.Contains(input.PersonInCharge)).ToList();
            }
            List<ProjectBasicsView> list2 = new List<ProjectBasicsView>();
            //判断角色显示可以查看的项目
            if (input.role.Contains("admin") || (input.role.Contains("在产项目管理角色") && input.role.Contains("潜项管理角色")))
            {
                list2 = list;
            }
            else if (!input.role.Contains("在产项目管理角色") && input.role.Contains("潜项管理角色"))
            {
                list2 = list.Where(o => o.ProjectClass == 31).ToList();
                if (user != null)
                {
                    foreach (var item in user)
                    {
                        var listson = list.Where(o => o.Id == item.PID && o.ProjectClass == 30).FirstOrDefault();
                        if (listson != null)
                        {
                            list2.Add(listson);
                        }
                    }
                }
            }
            else if (input.role.Contains("在产项目管理角色") && !input.role.Contains("潜项管理角色"))
            {
                list2 = list.Where(o => o.ProjectClass == 30).ToList();
                if (user != null)
                {
                    foreach (var item in user)
                    {
                        var listson = list.Where(o => o.Id == item.PID && o.ProjectClass == 31).FirstOrDefault();
                        if (listson != null)
                        {
                            list2.Add(listson);
                        }
                    }
                }
            }
            else
            {
                if (user != null)
                {
                    foreach (var item in user)
                    {
                        var listson = list.Where(o => o.Id == item.PID).FirstOrDefault();
                        if (listson != null)
                        {
                            list2.Add(listson);
                        }
                    }
                }
            }
            int total = list2.Count();
            list2 = list2.Skip((input.PageNo - 1) * input.PageSize).Take(input.PageSize).ToList();
            return new PagedResult<ProjectBasicsView>(Mapper.Map<List<ProjectBasicsView>>(list2), total);
        }

        /// <summary>
        /// 项目基础信息导出数据源
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<ProjectBasicsView> ProjectListDerive(ProjectBasicsSearchDto input)
        {
            var PersonInChargeList = _projectContactsService.GetAllList();
            var list = Repository.Orm.Select<ProjectBasicsEntity, DictionaryEntity, DictionaryEntity, Client_CompanyEntity, DictionaryEntity,
                DictionaryEntity, bd_SalesAreaInfoEntity, DictionaryEntity>()
                .LeftJoin(a1 => a1.t1.ProjectClass == a1.t2.Id)
                .LeftJoin(a2 => a2.t1.BusinessType == a2.t3.Id)
                .LeftJoin(a3 => a3.t1.ClientCompany == a3.t4.Id)
                .LeftJoin(a4 => a4.t1.ProManagementStyle == a4.t5.Id)
                .LeftJoin(a5 => a5.t1.ProState == a5.t6.Id)
                .LeftJoin(t => t.t4.MarketArea == t.t7.Id)
                .LeftJoin(t => t.t1.ProjectStatus == t.t8.Id)
                .WhereIf(input.ProjectName.NotNullOrWhiteSpace(), a => a.t1.ProjectName.Contains(input.ProjectName))
                .WhereIf(input.ProjectCode.NotNullOrWhiteSpace(), a => a.t1.ProjectCode.Contains(input.ProjectCode))
                .WhereIf(input.ProjectClass == 30, a => a.t1.ProjectClass == 30)
                .WhereIf(input.ProjectClass == 31, a => a.t1.ProjectClass != 30)
                .WhereIf(input.BusinessType > 0, a => a.t1.BusinessType == input.BusinessType)
                .WhereIf(input.UrgentGrade > 0 && input.UrgentGrade == 1, a => a.t1.UrgentGrade == 1 || a.t1.UrgentGrade == 2)
                .WhereIf(input.UrgentGrade > 0 && input.UrgentGrade == 2, a => a.t1.UrgentGrade == 3 || a.t1.UrgentGrade == 4)
                .WhereIf(input.UrgentGrade > 0 && input.UrgentGrade == 3, a => a.t1.UrgentGrade == 5)
                .WhereIf(input.ClientCompany > 0, a => a.t1.ClientCompany == input.ClientCompany)
                .WhereIf(input.ProManagementStyle > 0, a => a.t1.ProManagementStyle == input.ProManagementStyle)
                .WhereIf(input.ProState > 0, a => a.t1.ProState == input.ProState)
                .WhereIf(input.marketArea.NotNullOrWhiteSpace(), a => a.t7.Id == Convert.ToInt32(input.marketArea))
                .OrderByDescending(a => a.t1.Id)
                .ToList(t => new
                {
                    project = t.t1,
                    Dictionary1 = t.t2,
                    Dictionary2 = t.t3,
                    Dictionary3 = t.t4,
                    Dictionary4 = t.t5,
                    Dictionary5 = t.t6,
                    Dictionary7 = t.t7,
                    Dictionary8 = t.t8
                }).Select(t =>
                {
                    var Output = Mapper.Map<ProjectBasicsView>(t.project);
                    Output.ProjectClassName = (t.Dictionary1 == null) ? "" : t.Dictionary1.DictionaryText;
                    Output.BusinessTypeName = (t.Dictionary2 == null) ? "" : t.Dictionary2.DictionaryText;
                    Output.ClientCompanyName = (t.Dictionary3 == null) ? "" : t.Dictionary3.ClientName;
                    Output.MarketAreaTxt = (t.Dictionary7 == null) ? "" : t.Dictionary7.AreaName;
                    Output.ProManagementStyleName = (t.Dictionary4 == null) ? "" : t.Dictionary4.DictionaryText;
                    Output.ProStateName = (t.Dictionary5 == null) ? "" : t.Dictionary5.DictionaryText;
                    Output.ProjectStatusName = (t.Dictionary8 == null) ? "" : t.Dictionary8.DictionaryText;

                    //获取营销经理
                    var ProContract = PersonInChargeList.Where(o => o.PID == t.project.Id);
                    var PersonInChargeName = ProContract.Where(o => o.FZBKName == "营销经理").FirstOrDefault();
                    Output.PersonInChargeName = PersonInChargeName == null ? "" : PersonInChargeName.ContactsName;
                    return Output;
                }).ToList();
            return new List<ProjectBasicsView>(list);
        }

        public PagedResult<ProjectBasicsView> GetAllProject(string key, int PageNo, int PageSize)
        {
            var list = Repository.Orm.Select<ProjectBasicsEntity>()
                .WhereIf(key.NotNullOrWhiteSpace(), t => t.ProjectName.Contains(key) || t.ProjectCode.Contains(key))
                .OrderByDescending(t => t.Id)
                .Count(out var total)
                .Page(PageNo, PageSize)
                .ToList(t => new
                {
                    proEntity = t
                }).Select(t =>
                {
                    var output = Mapper.Map<ProjectBasicsView>(t.proEntity);
                    return output;
                }).ToList();
            return new PagedResult<ProjectBasicsView>(list, total);
        }


        /// <summary>
        /// 计算各种需要显示的数额
        /// </summary>
        /// <param name="item"></param>
        public void Compute(ProjectBasicsView item)
        {
            //获取该项目下所有质保金
            var remoneyList = _projectRetMoneyService.GetList().Where(t => t.ProjectID == item.Id).ToList();
            if (remoneyList != null)
            {
                if (remoneyList.Sum(t => t.RetMoneyProportion) != 0 && remoneyList.Count() != 0)
                {
                    //计算该项目质保金比例
                    item.RetentionRatio = remoneyList.Sum(t => t.RetMoneyProportion) / remoneyList.Count();
                }
                //计算该项目质保金金额
                item.RetentionAmount = remoneyList.Sum(t => t.RetMoneyAmount);
                //计算该项目已到期质保金金额
                item.PeriodMoney = remoneyList.Sum(t => t.AlrExpirationMoney);
            }

            //计算项目总金额
            decimal salePrice = 0;
            var salePirceList = _projectDeviceDetailsService.GetList().Where(t => t.ProjectID == item.Id).ToList();
            foreach (var salePitem in salePirceList)
            {
                //累加设备金额
                salePrice += salePitem.Qty * salePitem.DevicePrice;
            }
            item.SalesPrice = salePrice;

            //计算项目未签合同金额
            //计算当前项目编号下合同金额之和
            var ProContractAmount = _contract_InfoService.GetList().Where(t => t.pt_id == item.Id).Sum(t => t.ct_cash);
            item.ProSignedCtAmount = salePrice - ProContractAmount;

            //计算合同应开票总金额
            item.CtInvoicedAmount = salePrice / Convert.ToDecimal(1.13);

            //计算当前项目下合同开票金额(合同已开票金额)
            var ctInvoiceAmount = _projectInvoicingService.GetList().Where(t => t.ProjectId == item.Id).Sum(t => t.InvoiceAmount);
            item.ctInvoiceAmount = ctInvoiceAmount;

            //计算合同未开票金额
            item.CtUnInvoicedAmount = item.CtInvoicedAmount - ctInvoiceAmount;

            //计算已回款总金额
            item.AmountPaymentCollection = _payment_CollectionService.GetList().Where(t => t.Pt_Id == item.Id).Sum(t => t.Payment_Cash);

            //计算项目已回款比
            if (item.AmountPaymentCollection != 0 && item.SalesPrice != 0)
            {
                item.ProPaymentCollectionRatio = item.AmountPaymentCollection / item.SalesPrice;
            }
        }

        /// <summary>
        /// 根据ID查询父ID的项目信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<ProjectBasicsView> GetByParentIdList(int Id)
        {
            var list = Repository.Orm.Select<ProjectBasicsEntity, DictionaryEntity, DictionaryEntity, Client_CompanyEntity, DictionaryEntity,
                DictionaryEntity, bd_SalesAreaInfoEntity, DictionaryEntity>()
                .LeftJoin(a1 => a1.t1.ProjectClass == a1.t2.Id)
                .LeftJoin(a2 => a2.t1.BusinessType == a2.t3.Id)
                .LeftJoin(a3 => a3.t1.ClientCompany == a3.t4.Id)
                .LeftJoin(a4 => a4.t1.ProManagementStyle == a4.t5.Id)
                .LeftJoin(a5 => a5.t1.ProState == a5.t6.Id)
                .LeftJoin(t => t.t4.MarketArea == t.t7.Id)
                .LeftJoin(t => t.t1.ProjectStatus == t.t8.Id)
                .Where(a => a.t1.ParentId == Id)
                .OrderByDescending(a => a.t1.Id)
                .ToList(t => new
                {
                    project = t.t1,
                    Dictionary1 = t.t2,
                    Dictionary2 = t.t3,
                    Dictionary3 = t.t4,
                    Dictionary4 = t.t5,
                    Dictionary5 = t.t6,
                    Dictionary6 = t.t7,
                    Dictionary7 = t.t8,

                }).Select(t =>
                {
                    var Output = Mapper.Map<ProjectBasicsView>(t.project);
                    Output.ProjectClassName = (t.Dictionary1 == null) ? "" : t.Dictionary1.DictionaryText;
                    Output.BusinessTypeName = (t.Dictionary2 == null) ? "" : t.Dictionary2.DictionaryText;
                    Output.ClientCompanyName = (t.Dictionary3 == null) ? "" : t.Dictionary3.ClientName;
                    Output.MarketAreaTxt = (t.Dictionary6 == null) ? "" : t.Dictionary6.AreaName;
                    Output.ProManagementStyleName = (t.Dictionary4 == null) ? "" : t.Dictionary4.DictionaryText;
                    Output.ProStateName = (t.Dictionary5 == null) ? "" : t.Dictionary5.DictionaryText;
                    int count = GetList().Where(s => s.ParentId == Output.Id).ToList().Count;
                    if (count > 0)
                    {
                        Output.HasChildren = true;
                    }
                    else
                    {
                        Output.HasChildren = false;
                    }
                    //Output.OwnerUnitName = (t.Dictionary6 == null) ? "" : t.Dictionary6.ClientName;
                    Output.ProjectStatusName = (t.Dictionary7 == null) ? "" : t.Dictionary7.DictionaryText;
                    return Output;
                }).ToList();
            var PersonInChargeList = _projectContactsService.GetAllList();
            foreach (var item in list)
            {
                foreach (var chargeItem in PersonInChargeList)
                {
                    if (item.Id == chargeItem.PID && chargeItem.FZBKName == "营销经理")
                    {
                        item.PersonInChargeName = chargeItem.ContactsName;
                        break;
                    }
                    else
                        item.PersonInChargeName = "";
                }
            }
            return new List<ProjectBasicsView>(list);
        }

        public List<ProjectBasicsView> GetListById(int Id)
        {
            var list = Repository.Orm.Select<ProjectBasicsEntity, DictionaryEntity, DictionaryEntity, Client_CompanyEntity, DictionaryEntity,
                DictionaryEntity, bd_SalesAreaInfoEntity, DictionaryEntity>()
                .LeftJoin(a1 => a1.t1.ProjectClass == a1.t2.Id)
                .LeftJoin(a2 => a2.t1.BusinessType == a2.t3.Id)
                .LeftJoin(a3 => a3.t1.ClientCompany == a3.t4.Id)
                .LeftJoin(a4 => a4.t1.ProManagementStyle == a4.t5.Id)
                .LeftJoin(a5 => a5.t1.ProState == a5.t6.Id)
                //.LeftJoin(a7 => a7.t1.OwnerUnitID == a7.t7.Id)
                .LeftJoin(t => t.t4.MarketArea == t.t7.Id)
                .LeftJoin(t => t.t1.ProjectStatus == t.t8.Id)
                .Where(a => a.t1.Id == Id)
                .OrderByDescending(a => a.t1.Id)
                .ToList(t => new
                {
                    project = t.t1,
                    Dictionary1 = t.t2,
                    Dictionary2 = t.t3,
                    Dictionary3 = t.t4,
                    Dictionary4 = t.t5,
                    Dictionary5 = t.t6,
                    //Dictionary6 = t.t7,
                    Dictionary7 = t.t7,
                    Dictionary8 = t.t8,

                }).Select(t =>
                {
                    var Output = Mapper.Map<ProjectBasicsView>(t.project);
                    Output.ProjectClassName = (t.Dictionary1 == null) ? "" : t.Dictionary1.DictionaryText;
                    Output.BusinessTypeName = (t.Dictionary2 == null) ? "" : t.Dictionary2.DictionaryText;
                    Output.ClientCompanyName = (t.Dictionary3 == null) ? "" : t.Dictionary3.ClientName;
                    Output.MarketAreaTxt = (t.Dictionary7 == null) ? "" : t.Dictionary7.AreaName;
                    Output.ProManagementStyleName = (t.Dictionary4 == null) ? "" : t.Dictionary4.DictionaryText;
                    Output.ProStateName = (t.Dictionary5 == null) ? "" : t.Dictionary5.DictionaryText;
                    int count = GetList().Where(s => s.ParentId == Output.Id).ToList().Count;
                    if (count > 0)
                    {
                        Output.HasChildren = true;
                    }
                    else
                    {
                        Output.HasChildren = false;
                    }
                    //Output.OwnerUnitName = (t.Dictionary6 == null) ? "" : t.Dictionary6.ClientName;
                    Output.ProjectStatusName = (t.Dictionary8 == null) ? "" : t.Dictionary8.DictionaryText;
                    return Output;
                }).ToList();
            var PersonInChargeList = _projectContactsService.GetAllList();
            foreach (var item in list)
            {
                Compute(item);
                foreach (var chargeItem in PersonInChargeList)
                {
                    if (item.Id == chargeItem.PID && chargeItem.FZBKName == "营销经理")
                    {
                        item.PersonInChargeName = chargeItem.ContactsName;
                        break;
                    }
                    else
                        item.PersonInChargeName = "";
                }
            }
            return new List<ProjectBasicsView>(list);
        }

        /// <summary>
        /// 根据客户公司和项目分类查询数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<ProjectBasicsView> GetListByClientIdAndClass(int clientId, string proClassTxt)
        {
            var list = Repository.Orm.Select<ProjectBasicsEntity, DictionaryEntity, DictionaryEntity, Client_CompanyEntity, DictionaryEntity,
                 DictionaryEntity, DictionaryEntity>()
                 .LeftJoin(a1 => a1.t1.ProjectClass == a1.t2.Id)
                 .LeftJoin(a2 => a2.t1.BusinessType == a2.t3.Id)
                 .LeftJoin(a3 => a3.t1.ClientCompany == a3.t4.Id)
                 .LeftJoin(a4 => a4.t1.ProManagementStyle == a4.t5.Id)
                 .LeftJoin(a5 => a5.t1.ProState == a5.t6.Id)
                 //.LeftJoin(a6 => a6.t1.ContractID == a6.t7.Id)
                 //.LeftJoin(a7 => a7.t1.OwnerUnitID == a7.t7.Id)
                 .LeftJoin(a8 => a8.t1.ProjectStatus == a8.t7.Id)
                 .Where(a => a.t1.ClientCompany == clientId)
                 .Where(a => a.t2.DictionaryText == proClassTxt)
                 .ToList(t => new
                 {
                     project = t.t1,
                     Dictionary1 = t.t2,
                     Dictionary2 = t.t3,
                     Dictionary3 = t.t4,
                     Dictionary4 = t.t5,
                     Dictionary5 = t.t6,
                     //Dictionary6 = t.t7,
                     Dictionary7 = t.t7,
                 }).Select(t =>
                 {
                     var Output = Mapper.Map<ProjectBasicsView>(t.project);
                     Output.ProjectClassName = (t.Dictionary1 == null) ? "" : t.Dictionary1.DictionaryText;
                     Output.BusinessTypeName = (t.Dictionary2 == null) ? "" : t.Dictionary2.DictionaryText;
                     Output.ClientCompanyName = (t.Dictionary3 == null) ? "" : t.Dictionary3.ClientName;
                     Output.ProManagementStyleName = (t.Dictionary4 == null) ? "" : t.Dictionary4.DictionaryText;
                     Output.ProStateName = (t.Dictionary5 == null) ? "" : t.Dictionary5.DictionaryText;
                     //Output.ContractName = (t.Dictionary6 == null) ? "" : t.Dictionary6.contractName;
                     int count = GetList().Where(s => s.ParentId == Output.Id).ToList().Count;
                     if (count > 0)
                     {
                         Output.HasChildren = true;
                     }
                     else
                     {
                         Output.HasChildren = false;
                     }
                     //Output.OwnerUnitName = (t.Dictionary6 == null) ? "" : t.Dictionary6.ClientName;
                     Output.ProjectStatusName = (t.Dictionary7 == null) ? "" : t.Dictionary7.DictionaryText;
                     return Output;
                 }).ToList();
            return new List<ProjectBasicsView>(list);
        }

        /// <summary>
        /// 项目信息新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool Insert(ProjectBasicsListDto input)
        {
            var entity = Mapper.Map<ProjectBasicsEntity>(input);

            base.Insert(entity);
            return true;
        }

        /// <summary>
        /// 项目信息修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool Update(ProjectBasicsListDto input)
        {
            base.Update(input.Id, input);
            return true;
        }

        /// <summary>
        /// 获取项目的父子集合信息
        /// </summary>
        /// <returns></returns>
        public IList<ProjectBasicsView> GetTreeList(string ProjectKey)
        {
            var list = Repository.WhereIf(!string.IsNullOrEmpty(ProjectKey), x => x.ProjectName.Contains(ProjectKey) || x.ProjectCode.Contains(ProjectKey)).ToList();
            //无查询结果返回一个空的list
            if (!list.Any())
            {
                return new List<ProjectBasicsView>();
            }
            //如果有查询条件，则判断查询出来的list中是否存在子项目，如果存在则判断该子项的父项是否存在list，不存在则查询并添加
            if (ProjectKey.NotNullOrWhiteSpace())
            {
                foreach (var item in list)
                {
                    GetParentInfo(list, item);
                }
                foreach (var addItem in AddList)
                {
                    list.Add(addItem);
                }
            }
            return BuildTreeList<ProjectBasicsView, ProjectBasicsEntity>(list);
        }

        /// <summary>
        /// 获取项目的父子集合信息
        /// </summary>
        /// <returns></returns>
        public IList<ProjectBasicsView> GetTreeListTable(string ProjectKey, string pt_idsTxt, int PageNo, int PageSize)
        {
            var list = Repository.WhereIf(!string.IsNullOrEmpty(ProjectKey), x => x.ProjectName.Contains(ProjectKey) || x.ProjectCode.Contains(ProjectKey))
                .OrderByDescending(t => t.Id).ToList();
            //无查询结果返回一个空的list
            if (!list.Any())
            {
                return new List<ProjectBasicsView>();
            }
            //如果有查询条件，则判断查询出来的list中是否存在子项目，如果存在则判断该子项的父项是否存在list，不存在则查询并添加
            if (ProjectKey.NotNullOrWhiteSpace())
            {
                foreach (var item in list)
                {
                    GetParentInfo(list, item);
                }
                foreach (var addItem in AddList)
                {
                    list.Add(addItem);
                }
            }
            var treeList = BuildTreeList<ProjectBasicsView, ProjectBasicsEntity>(list);
            //分页
            if (pt_idsTxt.NotNullOrWhiteSpace())
            {
                var ptidArr = pt_idsTxt.Split(",");
                for (int i = 0; i < treeList.Count; i++)
                {
                    foreach (var chooseId in ptidArr)
                    {
                        var IsChildHase = childrenIsChoose(treeList[i], Int32.Parse(chooseId));
                        if (treeList[i].Id == Int32.Parse(chooseId) || IsChildHase)
                        {
                            ProjectBasicsView firstRow = treeList[i];
                            for (int j = i; j > 0; j--)
                            {
                                treeList[j] = treeList[j - 1];
                            }
                            treeList[0] = firstRow;
                        }
                    }
                }
            }
            return treeList;
        }

        public bool childrenIsChoose(ProjectBasicsView item, int chooseId)
        {
            if (item.Children != null)
            {
                foreach (var childItem in item.Children)
                {
                    bool result = childrenIsChoose(childItem, chooseId);
                    if (result)
                    {
                        return true;
                    }
                }
            }
            if (item.Id == chooseId)
            {
                return true;
            }
            return false;
        }

        //判断子项是否有父项，如果有则判断该父项是否已存在Alllist中，没有则添加；再循环调用本方法判断该父项是否还存在父项
        public void GetParentInfo(List<ProjectBasicsEntity> Alllist, ProjectBasicsEntity item)
        {
            if (item.ParentId != 0)
            {
                //获取该子项的父项信息
                var list = Repository.Where(t => t.Id == item.ParentId).ToList();
                if (list.Any())
                {
                    //判断父项信息是否已存在，不存在则添加
                    if (!Alllist.Where(t => t.Id == list[0].Id).ToList().Any())
                    {
                        AddList.Add(list[0]);
                    }
                }
                //调用本方法判断查询出来的父项是否还存在父项
                foreach (var QueryItem in list)
                {
                    GetParentInfo(Alllist, QueryItem);
                }
            }
        }



        /// <summary>
        /// 项目编号唯一
        /// </summary>
        /// <param name="ProjectCode"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool ProOnly(string ProjectCode, int Id)
        {
            var exist = Repository.Where(t => t.ProjectCode == ProjectCode).ToOne();
            if (exist == null) return false;
            if (Id != 0)
            {
                return Id != exist.Id;
            }
            return true;
        }

        /// <summary>
        /// 营销销售数据基础信息分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PagedResult<SellBasicsDataView> SellBasicsPagedList(SellBasicsDataSearchDto input)
        {
            var allList = _projectContactsService.GetAllList();
            var list = Repository.Orm.Select<ProjectBasicsEntity, DictionaryEntity, DictionaryEntity, Client_CompanyEntity, DictionaryEntity,
                DictionaryEntity, bd_SalesAreaInfoEntity, DictionaryEntity>()
                .LeftJoin(a1 => a1.t1.ProjectClass == a1.t2.Id)
                .LeftJoin(a2 => a2.t1.BusinessType == a2.t3.Id)
                .LeftJoin(a3 => a3.t1.ClientCompany == a3.t4.Id)
                .LeftJoin(a4 => a4.t1.ProManagementStyle == a4.t5.Id)
                .LeftJoin(a5 => a5.t1.ProState == a5.t6.Id)
                // .LeftJoin(a7 => a7.t1.OwnerUnitID == a7.t7.Id)
                .LeftJoin(a7 => a7.t1.ProjectStatus == a7.t8.Id)
                .LeftJoin(t => t.t4.MarketArea == t.t7.Id)
                .WhereIf(input.ProjectName.NotNullOrWhiteSpace(), a => a.t1.ProjectName.Contains(input.ProjectName))
                .WhereIf(input.ProjectCode.NotNullOrWhiteSpace(), a => a.t1.ProjectCode.Contains(input.ProjectCode))
                .WhereIf(input.ProjectClass > 0, a => a.t1.ProjectClass == input.ProjectClass)
                .WhereIf(input.BusinessType > 0, a => a.t1.BusinessType == input.BusinessType)
                .WhereIf(input.UrgentGrade == 1, a => a.t1.UrgentGrade == 1 || a.t1.UrgentGrade == 2)
                .WhereIf(input.UrgentGrade == 2, a => a.t1.UrgentGrade == 3 || a.t1.UrgentGrade == 4)
                .WhereIf(input.UrgentGrade == 3, a => a.t1.UrgentGrade == 5)
                .WhereIf(input.ClientCompany > 0, a => a.t1.ClientCompany == input.ClientCompany)
                .WhereIf(input.ProManagementStyle > 0, a => a.t1.ProManagementStyle == input.ProManagementStyle)
                .WhereIf(input.ProState > 0, a => a.t1.ProState == input.ProState)
                .WhereIf(input.marketArea.NotNullOrWhiteSpace(), a => a.t7.Id == Convert.ToInt32(input.marketArea))
                .Where(a => a.t1.IsDeleted == false && a.t1.ParentId == 0)
                .OrderByDescending(a => a.t1.Id)
                //.Count(out var total)
                //.Page(input.PageNo, input.PageSize)
                .ToList(a => new
                {
                    all = a.t1,
                    Dictionary1 = a.t2,
                    Dictionary2 = a.t3,
                    Dictionary3 = a.t4,
                    Dictionary4 = a.t5,
                    Dictionary5 = a.t6,
                    //Dictionary6 = a.t7,
                    Dictionary7 = a.t7,
                    Dictionary8 = a.t8,
                    Sum1 = Repository.Orm.Select<ProjectDeviceDetailsEntity>().Where(b => b.ProjectID == a.t1.Id).Sum(b => b.DevicePrice * b.Qty),
                    Sum2 = Repository.Orm.Select<ProjectInvoicingEntity>().Where(b => b.ProjectId == a.t1.Id).Sum(b => b.InvoiceAmount),
                    Sum3 = Repository.Orm.Select<Contract_InfoEntity>().Where(b => b.pt_id == a.t1.Id).Sum(b => b.ct_cash),
                    Sum4 = Repository.Orm.Select<Payment_CollectionEntity>().Where(b => b.Pt_Id == a.t1.Id).Sum(b => b.Payment_Cash),
                    Sum5 = Repository.Orm.Select<ProjectRetMoneyEntity>().Where(b => b.ProjectID == a.t1.Id).Sum(b => b.RetMoneyAmount),
                    Sum6 = Repository.Orm.Select<ProjectRetMoneyEntity>().Where(b => b.ProjectID == a.t1.Id).Sum(b => b.AlrExpirationMoney),
                    Sum7 = Repository.Orm.Select<ProjectRetMoneyEntity>().Where(b => b.ProjectID == a.t1.Id).Sum(b => b.RetMoneyProportion),
                    Count7 = Repository.Orm.Select<ProjectRetMoneyEntity>().Where(b => b.ProjectID == a.t1.Id).Count(),
                    //ContractSigningDate = Repository.Orm.Select<Contract_InfoEntity>().Where(b => b.pt_id == a.t1.Id).OrderByDescending(b => b.Id).ToOne(x=>x.ct_signingDate),
                    RecentlyBilledDate = Repository.Orm.Select<ProjectInvoicingEntity>().Where(b => b.ProjectId == a.t1.Id).OrderByDescending(b => b.Id).ToOne(x => x.InvoiceDate),
                    RecentlyPaymentDate = Repository.Orm.Select<Payment_CollectionEntity>().Where(b => b.Pt_Id == a.t1.Id).OrderByDescending(b => b.Id).ToOne(x => x.ReturnDate),
                    Count1 = Repository.Where(s => s.ParentId == a.t1.Id).Count()
                }).Select(t =>
                {
                    var Output = Mapper.Map<SellBasicsDataView>(t.all);
                    Output.ProjectClassName = (t.Dictionary1 == null) ? "" : t.Dictionary1.DictionaryText;
                    Output.BusinessTypeName = (t.Dictionary2 == null) ? "" : t.Dictionary2.DictionaryText;
                    Output.ClientCompanyName = (t.Dictionary3 == null) ? "" : t.Dictionary3.ClientName;
                    Output.MarketAreaTxt = (t.Dictionary7 == null) ? "" : t.Dictionary7.AreaName;
                    Output.ProManagementStyleName = (t.Dictionary4 == null) ? "" : t.Dictionary4.DictionaryText;
                    Output.ProStateName = (t.Dictionary5 == null) ? "" : t.Dictionary5.DictionaryText;
                    Output.ProjectStatusName = (t.Dictionary8 == null) ? "" : t.Dictionary8.DictionaryText;
                    Output.TotalProAmount = Math.Round(t.Sum1, 2);
                    Output.ProUnsignedContractAmount = Math.Round(Output.TotalProAmount - t.Sum3, 2);
                    Output.ConInvoiceableAmount = Math.Round(Output.TotalProAmount / Convert.ToDecimal(1.13), 2);
                    Output.ProInvoicedAmount = Math.Round(t.Sum2, 2);
                    Output.ConUnbilledAmount = Math.Round(Output.ConInvoiceableAmount - Output.ProInvoicedAmount, 2);
                    Output.ReturnedTotalAmount = Math.Round(t.Sum4, 2);
                    Output.ProReturnedScale = Output.TotalProAmount > 0 ? Math.Round(Output.ReturnedTotalAmount / Output.TotalProAmount, 2) : 0;
                    Output.WarrantyAmount = Math.Round(t.Sum5, 2);
                    Output.ExpirationWarrantyAmount = Math.Round(t.Sum6, 2);
                    Output.WarrantyScale = t.Count7 > 0 ? Math.Round(t.Sum7 / t.Count7, 2) : 0;
                    //Output.ContractSigningDate = t.ContractSigningDate == DateTime.MinValue ? "" : t.ContractSigningDate.ToString("yyyy-MM-dd");
                    Output.RecentlyBilledDate = t.RecentlyBilledDate == DateTime.MinValue ? "" : t.RecentlyBilledDate.ToString("yyyy-MM-dd");
                    Output.RecentlyPaymentDate = t.RecentlyPaymentDate == DateTime.MinValue ? "" : t.RecentlyPaymentDate.ToString("yyyy-MM-dd");
                    //获取营销经理、项目经理、产品经理
                    var ProContract = allList.Where(o => o.PID == t.all.Id);
                    var PersonInChargeName = ProContract.Where(o => o.FZBKName == "营销经理").FirstOrDefault();
                    Output.PersonInChargeName = PersonInChargeName == null ? "" : PersonInChargeName.ContactsName;
                    var ProjectManager = ProContract.Where(o => o.FZBKName == "项目经理").FirstOrDefault();
                    Output.ProjectManager = ProjectManager == null ? "" : ProjectManager.ContactsName;
                    var ProductManager = ProContract.Where(o => o.FZBKName == "产品经理").FirstOrDefault();
                    Output.ProductManager = ProductManager == null ? "" : ProductManager.ContactsName;

                    int count = (int)t.Count1;
                    if (count > 0)
                    {
                        Output.HasChildren = true;
                    }
                    else
                    {
                        Output.HasChildren = false;
                    }
                    return Output;
                }).ToList();
            //已到期质保金金额是否大于0
            if (input.IsPeriodMoneyZero == 1)
            {
                list = list.Where(s => s.ExpirationWarrantyAmount > 0).ToList();
            }
            else if (input.IsPeriodMoneyZero == 2)
            {
                list = list.Where(s => s.ExpirationWarrantyAmount <= 0).ToList();
            }
            //项目已回款比（0，0-30,30-90,90-99.99，100）
            if (input.ProReturnedScale == 1)
            {
                list = list.Where(s => s.ProReturnedScale == 0).ToList();
            }
            else if (input.ProReturnedScale == 2)
            {
                list = list.Where(s => s.ProReturnedScale > 0 && s.ProReturnedScale <= 30).ToList();
            }
            else if (input.ProReturnedScale == 3)
            {
                list = list.Where(s => s.ProReturnedScale > 30 && s.ProReturnedScale <= 90).ToList();
            }
            else if (input.ProReturnedScale == 4)
            {
                list = list.Where(s => s.ProReturnedScale > 90 && s.ProReturnedScale < 100).ToList();
            }
            else if (input.ProReturnedScale == 5)
            {
                list = list.Where(s => s.ProReturnedScale == 100).ToList();
            }
            //营销经理
            if (!string.IsNullOrWhiteSpace(input.PersonInChargeName))
            {
                list = list.Where(s => s.PersonInChargeName.Contains(input.PersonInChargeName)).ToList();
            }
            //项目经理
            if (!string.IsNullOrWhiteSpace(input.ProjectManager))
            {
                list = list.Where(s => s.ProjectManager.Contains(input.ProjectManager)).ToList();
            }
            //产品经理
            if (!string.IsNullOrWhiteSpace(input.ProductManager))
            {
                list = list.Where(s => s.ProductManager.Contains(input.ProductManager)).ToList();
            }
            int total = list.Count();
            list = list.Skip((input.PageNo - 1) * input.PageSize).Take(input.PageSize).ToList();
            foreach (var item in list)
            {
                var contractInfo = Repository.Orm.Select<Contract_InfoEntity>().Where(b => b.pt_idsTxt.Contains(item.Id.ToString())).OrderByDescending(b => b.Id).ToList();
                foreach (var ptitem in contractInfo)
                {
                    bool isBreak = false;
                    foreach (var ptiditem in ptitem.pt_idsTxt.Split(","))
                    {
                        if (Int32.Parse(ptiditem) == item.Id)
                        {
                            item.ContractSigningDate = ptitem.ct_signingDate == DateTime.MinValue ? "" : ptitem.ct_signingDate.ToString("yyyy-MM-dd");
                            isBreak = true;
                            break;
                        }
                    }
                    if (isBreak) break;
                }
            }
            return new PagedResult<SellBasicsDataView>(Mapper.Map<List<SellBasicsDataView>>(list), total);
        }
        /// <summary>
        /// 营销销售数据基础信息子项目信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<SellBasicsDataView> ParentSellBasicsPagedList(int Id)
        {
            var list = Repository.Orm.Select<ProjectBasicsEntity, DictionaryEntity, DictionaryEntity, Client_CompanyEntity, DictionaryEntity,
                DictionaryEntity, bd_SalesAreaInfoEntity>()
                .LeftJoin(a1 => a1.t1.ProjectClass == a1.t2.Id)
                .LeftJoin(a2 => a2.t1.BusinessType == a2.t3.Id)
                .LeftJoin(a3 => a3.t1.ClientCompany == a3.t4.Id)
                .LeftJoin(a4 => a4.t1.ProManagementStyle == a4.t5.Id)
                .LeftJoin(a5 => a5.t1.ProState == a5.t6.Id)
                //.LeftJoin(a7 => a7.t1.OwnerUnitID == a7.t7.Id)
                .LeftJoin(t => t.t4.MarketArea == t.t7.Id)
                .Where(a => a.t1.IsDeleted == false && a.t1.ParentId == Id)
                .OrderByDescending(a => a.t1.Id)
                .ToList(a => new
                {
                    all = a.t1,
                    Dictionary1 = a.t2,
                    Dictionary2 = a.t3,
                    Dictionary3 = a.t4,
                    Dictionary4 = a.t5,
                    Dictionary5 = a.t6,
                    //Dictionary6 = a.t7,
                    Dictionary7 = a.t7,
                }).Select(t =>
                {
                    var Output = Mapper.Map<SellBasicsDataView>(t.all);
                    Output.ProjectClassName = (t.Dictionary1 == null) ? "" : t.Dictionary1.DictionaryText;
                    Output.BusinessTypeName = (t.Dictionary2 == null) ? "" : t.Dictionary2.DictionaryText;
                    Output.ClientCompanyName = (t.Dictionary3 == null) ? "" : t.Dictionary3.ClientName;
                    Output.MarketAreaTxt = (t.Dictionary7 == null) ? "" : t.Dictionary7.AreaName;
                    Output.ProManagementStyleName = (t.Dictionary4 == null) ? "" : t.Dictionary4.DictionaryText;
                    Output.ProStateName = (t.Dictionary5 == null) ? "" : t.Dictionary5.DictionaryText;
                    Output.TotalProAmount = Math.Round(Repository.Orm.Select<ProjectDeviceDetailsEntity>().Where(b => b.ProjectID == t.all.Id).Sum(b => b.DevicePrice * b.Qty), 2);
                    Output.ProUnsignedContractAmount = Math.Round(Output.TotalProAmount - Repository.Orm.Select<Contract_InfoEntity>().Where(b => b.pt_id == t.all.Id).Sum(b => b.ct_cash), 2);
                    Output.ConInvoiceableAmount = Math.Round(Output.TotalProAmount / Convert.ToDecimal(1.13), 2);
                    Output.ProInvoicedAmount = Math.Round(Repository.Orm.Select<ProjectInvoicingEntity>().Where(b => b.ProjectId == t.all.Id).Sum(b => b.InvoiceAmount), 2);
                    Output.ConUnbilledAmount = Math.Round(Output.ProInvoicedAmount - Output.ProInvoicedAmount, 2);
                    Output.ReturnedTotalAmount = Math.Round(Repository.Orm.Select<Payment_CollectionEntity>().Where(b => b.Pt_Id == t.all.Id).Sum(b => b.Payment_Cash), 2);
                    Output.ProReturnedScale = Output.TotalProAmount > 0 ? Math.Round(Output.ReturnedTotalAmount / Output.TotalProAmount, 2) : 0;
                    Output.WarrantyAmount = Math.Round(Repository.Orm.Select<ProjectRetMoneyEntity>().Where(b => b.ProjectID == t.all.Id).Sum(b => b.RetMoneyAmount), 2);
                    Output.ExpirationWarrantyAmount = Math.Round(Repository.Orm.Select<ProjectRetMoneyEntity>().Where(b => b.ProjectID == t.all.Id).Sum(b => b.AlrExpirationMoney), 2);
                    decimal wsRetMoneyProportion = Repository.Orm.Select<ProjectRetMoneyEntity>().Where(b => b.ProjectID == t.all.Id).Sum(b => b.RetMoneyProportion);
                    decimal wsCount = Repository.Orm.Select<ProjectRetMoneyEntity>().Where(b => b.ProjectID == t.all.Id).Count();
                    Output.WarrantyScale = wsCount > 0 ? Math.Round(wsRetMoneyProportion / wsCount, 2) : 0;
                    var ContractSigningDate = Repository.Orm.Select<Contract_InfoEntity>().Where(b => b.pt_id == t.all.Id).OrderByDescending(b => b.Id).First();
                    Output.ContractSigningDate = ContractSigningDate == null ? "" : ContractSigningDate.ct_signingDate.ToString("yyyy-MM-dd");
                    var RecentlyBilledDate = Repository.Orm.Select<ProjectInvoicingEntity>().Where(b => b.ProjectId == t.all.Id).OrderByDescending(b => b.Id).First();
                    Output.RecentlyBilledDate = RecentlyBilledDate == null ? "" : RecentlyBilledDate.InvoiceDate.ToString("yyyy-MM-dd");
                    var RecentlyPaymentDate = Repository.Orm.Select<Payment_CollectionEntity>().Where(b => b.Pt_Id == t.all.Id).OrderByDescending(b => b.Id).First();
                    Output.RecentlyPaymentDate = RecentlyPaymentDate == null ? "" : RecentlyPaymentDate.ReturnDate.ToString("yyyy-MM-dd");

                    //获取营销经理、项目经理、产品经理
                    var ProContract = Repository.Orm.Select<Project_ContactsInfo, ContactsEntity, DictionaryEntity>()
                    .LeftJoin(w => w.t1.ContactsID == w.t2.Id)
                    .LeftJoin(w => w.t1.FZBKId == w.t3.Id)
                    .Where(w => w.t1.PID == t.all.Id).ToList(w => new { w.t1, w.t2, w.t3 });
                    var PersonInChargeName = ProContract.Where(o => o.t3.DictionaryText == "营销经理").FirstOrDefault();
                    Output.PersonInChargeName = PersonInChargeName == null ? "" : PersonInChargeName.t2?.ContactsName ?? "";
                    var ProjectManager = ProContract.Where(o => o.t3.DictionaryText == "项目经理").FirstOrDefault();
                    Output.ProjectManager = ProjectManager == null ? "" : ProjectManager.t2?.ContactsName ?? "";
                    var ProductManager = ProContract.Where(o => o.t3.DictionaryText == "产品经理").FirstOrDefault();
                    Output.ProductManager = ProductManager == null ? "" : ProductManager.t2?.ContactsName ?? "";

                    int count = GetList().Where(s => s.ParentId == Output.Id).Count();
                    if (count > 0)
                    {
                        Output.HasChildren = true;
                    }
                    else
                    {
                        Output.HasChildren = false;
                    }
                    return Output;
                }).ToList();
            return new List<SellBasicsDataView>(list);
        }

        public List<ProjectBasicsView> GetListByIds(string ProjectName, string ProjectCode)
        {
            var list = Repository.Orm.Select<ProjectBasicsEntity, DictionaryEntity, DictionaryEntity, Client_CompanyEntity, DictionaryEntity,
                DictionaryEntity, bd_SalesAreaInfoEntity>()
                .LeftJoin(a1 => a1.t1.ProjectClass == a1.t2.Id)
                .LeftJoin(a2 => a2.t1.BusinessType == a2.t3.Id)
                .LeftJoin(a3 => a3.t1.ClientCompany == a3.t4.Id)
                .LeftJoin(a4 => a4.t1.ProManagementStyle == a4.t5.Id)
                .LeftJoin(a5 => a5.t1.ProState == a5.t6.Id)
                //.LeftJoin(a7 => a7.t1.OwnerUnitID == a7.t7.Id)
                .LeftJoin(t => t.t4.MarketArea == t.t7.Id)
                .WhereIf(ProjectName.NotNullOrWhiteSpace(), a => a.t1.ProjectName.Contains(ProjectName))
                .WhereIf(ProjectCode.NotNullOrWhiteSpace(), a => a.t1.ProjectCode.Contains(ProjectCode))
                .Where(a => a.t1.IsDeleted == false && a.t1.ParentId == 0)
                .OrderByDescending(a => a.t1.Id)
                .Count(out var total)
                .ToList(t => new
                {
                    project = t.t1,
                    Dictionary1 = t.t2,
                    Dictionary2 = t.t3,
                    Dictionary3 = t.t4,
                    Dictionary4 = t.t5,
                    Dictionary5 = t.t6,
                    //Dictionary6 = t.t7,
                    Dictionary7 = t.t7,

                }).Select(t =>
                {
                    var Output = Mapper.Map<ProjectBasicsView>(t.project);
                    Output.ProjectClassName = (t.Dictionary1 == null) ? "" : t.Dictionary1.DictionaryText;
                    Output.BusinessTypeName = (t.Dictionary2 == null) ? "" : t.Dictionary2.DictionaryText;
                    Output.ClientCompanyName = (t.Dictionary3 == null) ? "" : t.Dictionary3.ClientName;
                    Output.MarketAreaTxt = (t.Dictionary7 == null) ? "" : t.Dictionary7.AreaName;
                    Output.ProManagementStyleName = (t.Dictionary4 == null) ? "" : t.Dictionary4.DictionaryText;
                    Output.ProStateName = (t.Dictionary5 == null) ? "" : t.Dictionary5.DictionaryText;
                    int count = GetList().Where(s => s.ParentId == Output.Id).ToList().Count;
                    if (count > 0)
                    {
                        Output.HasChildren = true;
                    }
                    else
                    {
                        Output.HasChildren = false;
                    }
                    //Output.OwnerUnitName = (t.Dictionary6 == null) ? "" : t.Dictionary6.ClientName;
                    return Output;
                }).ToList();
            return new List<ProjectBasicsView>(list);
        }

        public PagedResult<ProjectBasicsView> GetAllList(string key, int PageNo, int PageSize)
        {
            var list = Repository.Orm.Select<ProjectBasicsEntity, DictionaryEntity, DictionaryEntity, Client_CompanyEntity, DictionaryEntity,
                DictionaryEntity, bd_SalesAreaInfoEntity>()
                .LeftJoin(a1 => a1.t1.ProjectClass == a1.t2.Id)
                .LeftJoin(a2 => a2.t1.BusinessType == a2.t3.Id)
                .LeftJoin(a3 => a3.t1.ClientCompany == a3.t4.Id)
                .LeftJoin(a4 => a4.t1.ProManagementStyle == a4.t5.Id)
                .LeftJoin(a5 => a5.t1.ProState == a5.t6.Id)
                //.LeftJoin(a7 => a7.t1.OwnerUnitID == a7.t7.Id)
                .LeftJoin(t => t.t4.MarketArea == t.t7.Id)
                .WhereIf(key.NotNullOrWhiteSpace(), t => t.t1.ProjectCode.Contains(key) || t.t1.ProjectName.Contains(key))
                .Page(PageNo, PageSize)
                .Count(out var total)
                .OrderByDescending(a => a.t1.Id)
                .ToList(t => new
                {
                    project = t.t1,
                    Dictionary1 = t.t2,
                    Dictionary2 = t.t3,
                    Dictionary3 = t.t4,
                    Dictionary4 = t.t5,
                    Dictionary5 = t.t6,
                    //Dictionary6 = t.t7,
                    Dictionary7 = t.t7,

                }).Select(t =>
                {
                    var Output = Mapper.Map<ProjectBasicsView>(t.project);
                    Output.ProjectClassName = (t.Dictionary1 == null) ? "" : t.Dictionary1.DictionaryText;
                    Output.BusinessTypeName = (t.Dictionary2 == null) ? "" : t.Dictionary2.DictionaryText;
                    Output.ClientCompanyName = (t.Dictionary3 == null) ? "" : t.Dictionary3.ClientName;
                    Output.MarketAreaTxt = (t.Dictionary7 == null) ? "" : t.Dictionary7.AreaName;
                    Output.ProManagementStyleName = (t.Dictionary4 == null) ? "" : t.Dictionary4.DictionaryText;
                    Output.ProStateName = (t.Dictionary5 == null) ? "" : t.Dictionary5.DictionaryText;
                    int count = GetList().Where(s => s.ParentId == Output.Id).ToList().Count;
                    if (count > 0)
                    {
                        Output.HasChildren = true;
                    }
                    else
                    {
                        Output.HasChildren = false;
                    }
                    //Output.OwnerUnitName = (t.Dictionary6 == null) ? "" : t.Dictionary6.ClientName;
                    return Output;
                }).ToList();
            return new PagedResult<ProjectBasicsView>(list, total);
        }

        public List<ProjectBasicsView> GetAllList(string key)
        {
            var list = Repository.Orm.Select<ProjectBasicsEntity>()
                .WhereIf(key.NotNullOrWhiteSpace(), t => t.ProjectCode.Contains(key) || t.ProjectName.Contains(key))
                .ToList(t => new
                {
                    project = t
                }).Select(t =>
                {
                    var Output = Mapper.Map<ProjectBasicsView>(t.project);
                    return Output;
                }).ToList();
            return new List<ProjectBasicsView>(list);
        }

        /// <summary>
        /// 编辑项目信息状态类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool UpdateProState(int Id, int ProjectStatus, int proState)
        {
            Repository.Orm.Update<ProjectBasicsEntity>(Id).Set(a => a.ProjectStatus, ProjectStatus).Set(a => a.ProState == proState).ExecuteAffrows();
            return true;
        }

        /// <summary>
        /// 编辑项目备注信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool UpdateProRemark(int Id, string Remark)
        {
            Repository.Orm.Update<ProjectBasicsEntity>(Id).Set(a => a.Remark, Remark).ExecuteAffrows();
            return true;
        }
        /// <summary>
        /// 项目团队消息提醒
        /// </summary>
        /// <param name="link"></param>
        /// <param name="esburl"></param>
        /// <returns></returns>
        public bool MessagePush(string link, string esburl)
        {
            try
            {
                //var proList = Repository.Where(o => o.IsDeleted == false && o.ProState == 171 && o.ProjectStatus == 215 && o.ProjectClass == 30).ToList();
                //查询出项目状态和分类进行筛选进行消息提醒
                var list = Repository.Orm.Select<ProjectBasicsEntity, DictionaryEntity, DictionaryEntity>()
                .LeftJoin(a1 => a1.t1.ProjectClass == a1.t2.Id)
                .LeftJoin(a2 => a2.t1.ProjectStatus == a2.t3.Id)
                .Where(a => a.t1.IsDeleted == false)
                .OrderByDescending(a => a.t1.Id)
                .ToList(t => new
                {
                    project = t.t1,
                    Dictionary1 = t.t2,
                    Dictionary2 = t.t3

                }).Select(t =>
                {
                    var Output = Mapper.Map<ProjectBasicsView>(t.project);
                    Output.ProjectClassName = (t.Dictionary1 == null) ? "" : t.Dictionary1.DictionaryText;
                    Output.ProjectStatusName = (t.Dictionary2 == null) ? "" : t.Dictionary2.DictionaryText;
                    return Output;
                }).ToList();
                //查询出用户的账号和电话
                var teamList = Repository.Orm.Select<Project_ContactsInfo, UserEntity, UserExtenderEntity>()
                    .LeftJoin(t1 => t1.t1.ContactsID == t1.t2.Id)
                    .LeftJoin(t1 => t1.t1.ContactsID == t1.t3.UserId)
                    .Where(o => o.t1.IsDeleted == false)
                    .ToList(t => new
                    {
                        team = t.t1,
                        name = t.t2,
                        tel = t.t3
                    }).Select(t =>
                    {
                        var Output = Mapper.Map<projectcontactsListDto>(t.team);
                        Output.ContactsName = (t.name == null) ? "" : t.name.Account;
                        Output.Telephone1 = (t.tel == null) ? "" : t.tel.Telnum;
                        return Output;
                    }).ToList();
                //选择状态：在产和项目分类：在产项目的数据进行消息提醒
                list = list.Where(a => (a.ProjectStatusName == "在产" && a.ProjectClassName == "在产项目") || a.ProjectClassName != "在产项目").ToList();
                //list = list.Where(a => a.ProjectCode == "RWYF22002").ToList();
                if (list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        ArrayList myArrayList = new ArrayList();

                        var li = teamList.Where(o => o.PID == item.Id).ToList();
                        if (li.Count > 0)
                        {
                            foreach (var item1 in li)
                            {
                                myArrayList.Add(new { type = "user", id = item1.Telephone1 });
                            }
                            var data = new ESBSendMessageInput
                            {
                                Title = "您的“" + item.ProjectName + "(" + item.ProjectCode + ")”项目动态需要更新，请点击该条消息进入润伟项目管理系统（RWPMS）及时更新动态。",
                                Type = 2, //1为待办，2为通知，待办需要完成，通知已读即可
                                Desc = "",
                                Link = "/project/projectbasics",
                                From = "PLM",
                                TargetPlantform = "OA",
                                Targets = myArrayList.ToArray(),
                                SendMode = 0,
                                SendModeValue = ""
                            };
                            _thirdPartService.PostRequestAsync(esburl, data);
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                _logService.AddOperationLog(true, ex.Message, ex.Message);
                throw new LogicException(ExceptionCode.Failed, $"消息发送异常:" + ex.Message);
            }

        }

        /// <summary>
        /// 通过项目ID查询问题反馈信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool ProblemInfo(int[] ids)
        {
            bool bo = false;
            foreach (var item in ids)
            {
                var li = Repository.Orm.Select<ProblemfeedbackEntity>().Where(o => o.pt_ID == item).ToOne();
                if (li != null)
                {
                    bo = true;
                    break;
                }
            }
            return bo;
        }
    }
}
