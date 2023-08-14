using AutoMapper;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Project;
using RW.PMS.Application.Contracts.Project;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.Extensions;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.Project;
using RW.PMS.Domain.Entities.System;
using RW.PMS.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Project
{
    public class Payment_CollectionService : CrudApplicationService<Payment_CollectionEntity, int>, IPayment_CollectionService
    {
        private readonly IContract_InfoService _contract_InfoService;
        private readonly IProjectInvoicingService _projectInvoicingService;
        private readonly IProjectDeviceDetailsService _projectDeviceDetailsService;
        private readonly IProjectContactsService _projectContactsService;
        public Payment_CollectionService(IDataValidatorProvider dataValidator,
            IProjectDeviceDetailsService projectDeviceDetailsService, IContract_InfoService contract_InfoService, IProjectInvoicingService projectInvoicingService,
            IProjectContactsService projectContactsService,
           IRepository<Payment_CollectionEntity, int> repository, IMapper mapper, Lazy<ICurrentUser> currentUser) : base(
           dataValidator, repository, mapper, currentUser)
        {
            _contract_InfoService = contract_InfoService;
            _projectInvoicingService = projectInvoicingService;
            _projectContactsService = projectContactsService;
            _projectDeviceDetailsService = projectDeviceDetailsService;
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PagedResult<Payment_CollectionOutput> GetPagedList(Payment_CollectionSearchDto input)
        {
            string[] DateArray = input.PaymentDateRangT.NotNullOrWhiteSpace() ? input.PaymentDateRangT.Split("~") : null;
            var clienclist = Repository.Select.From<ProjectBasicsEntity, DictionaryEntity, 
                DictionaryEntity, Client_CompanyEntity, bd_SalesAreaInfoEntity>((a, b,dic1,dic2, cc, sale) => a
                  .LeftJoin(ab => ab.Pt_Id == b.Id)
                  .LeftJoin(ad => ad.ReturnType == dic1.Id)
                  .LeftJoin(ad => ad.ReturnWay == dic2.Id))
                .LeftJoin(t => t.t2.ClientCompany == t.t5.Id)
                .LeftJoin(t => t.t5.MarketArea == t.t6.Id)
                 .WhereIf(input.Pt_Name.NotNullOrEmpty(), (a, b, dic1, dic2, cc, sale) => b.ProjectName.Contains(input.Pt_Name))
                 .WhereIf(input.Pt_Code.NotNullOrEmpty(), (a, b, dic1, dic2, cc, sale) =>b.ProjectCode.Contains(input.Pt_Code))
                 .WhereIf(input.MarketArea != 0, t => t.t5.MarketArea == input.MarketArea)
                 .WhereIf(input.ClientName.NotNullOrWhiteSpace(), t => t.t5.ClientName.Contains(input.ClientName))
                 .WhereIf(input.PaymentDateRangT.NotNullOrWhiteSpace(),t=>
                 t.t1.ReturnDate >= Convert.ToDateTime(DateArray[0].ToString()) && t.t1.ReturnDate < Convert.ToDateTime(DateArray[1].ToString()).AddDays(1))
                 .OrderByDescending((a, b, dic1, dic2, cc, sale) => a.Id)
                 .Count(out var total)
                 //.Page(input.PageNo, input.PageSize)
                 .ToList((a, b, dic1, dic2, cc, sale) => new
                 {
                     AllM = a,
                     ProjectName = b.ProjectName,
                     ProjectCode=b.ProjectCode,
                     ReturnTypeText=dic1.DictionaryText,
                     ReturnWayText = dic2.DictionaryText,
                     companyName = cc.ClientName,
                     MArea = sale.AreaName,
                 })
                 .Select(t =>
                 {
                     var mapp = Mapper.Map<Payment_CollectionOutput>(t.AllM);
                     mapp.Pt_Name = t.ProjectName;
                     mapp.Pt_Code = t.ProjectCode;
                     mapp.ReturnTypeText = t.ReturnTypeText;
                     mapp.ReturnWayText = t.ReturnWayText;
                     mapp.ClientName = t.companyName;
                     mapp.MarketAreaTxt = t.MArea;
                     return mapp;
                 }).ToList();
            var PersonInChargeList = _projectContactsService.GetAllList();
            foreach (var item in clienclist)
            {
                foreach (var chargeItem in PersonInChargeList)
                {
                    if (item.Pt_Id == chargeItem.PID && chargeItem.FZBKName == "营销经理")
                    {
                        item.PersonInChargeName = chargeItem.ContactsName;
                        break;
                    }
                    else
                        item.PersonInChargeName = "";
                }
            }
            if (input.PersonInChargeName.NotNullOrWhiteSpace())
            {
                clienclist = clienclist.Where(a => a.PersonInChargeName.Contains(input.PersonInChargeName)).ToList();
                total = clienclist.Count;
            }
            foreach (var item in clienclist)
            {
                //获取项目总金额
                decimal salePrice = 0;
                var salePirceList = _projectDeviceDetailsService.GetList().Where(t => t.ProjectID == item.Pt_Id).ToList();
                foreach (var salePitem in salePirceList)
                {
                    //累加设备金额
                    salePrice += salePitem.Qty * salePitem.DevicePrice;
                }
                //计算当前项目编号下合同金额之和
                var ProContractAmount = _contract_InfoService.GetList().Where(t => t.pt_id == item.Pt_Id).Sum(t => t.ct_cash);
                //得出项目已签合同金额
                item.pt_SignedCash = salePrice - ProContractAmount;
                //获取项目未签合同金额
                item.pt_UnSignedCash = salePrice-item.pt_SignedCash;
                //计算当前项目下合同开票金额(合同已开票金额)
                var ctInvoiceAmount = _projectInvoicingService.GetList().Where(t => t.ProjectId == item.Pt_Id).Sum(t => t.InvoiceAmount);
                item.ctInvoiceAmount = ctInvoiceAmount;
                //计算合同未开票金额
                item.ctUnInvoiceAmount = salePrice / Convert.ToDecimal(1.13) - ctInvoiceAmount;
                //计算回款比
                if (item.Payment_Cash!=0&&salePrice!=0)
                {
                    item.Payment_Precent = item.Payment_Cash / salePrice;
                }
            }
            var IEnumerableList = clienclist.Skip((input.PageNo - 1) * input.PageSize).Take(input.PageSize);
            //把IEnumerable类型转换成list类型
            var ResultList = new List<Payment_CollectionOutput>(IEnumerableList);
            return new PagedResult<Payment_CollectionOutput>(Mapper.Map<List<Payment_CollectionOutput>>(ResultList), total);
        }
        /// <summary>
        /// 根据ID获取回款信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<Payment_CollectionOutput> GetByIdList(int Id)
        {
            var clienclist = Repository.Select.From<ProjectBasicsEntity, DictionaryEntity,
                DictionaryEntity, Client_CompanyEntity, bd_SalesAreaInfoEntity, ContactsEntity>((a, b, dic1, dic2, cc, sale, con) => a
                  .LeftJoin(ab => ab.Pt_Id == b.Id)
                  .LeftJoin(ad => ad.ReturnType == dic1.Id)
                  .LeftJoin(ad => ad.ReturnWay == dic2.Id))
                .LeftJoin(t => t.t2.ClientCompany == t.t5.Id)
                .LeftJoin(t => t.t5.MarketArea == t.t6.Id)
                .LeftJoin(t => t.t5.PersonInCharge == t.t7.Id)
                 .Where(t => t.t1.Pt_Id == Id)
                 .ToList((a, b, dic1, dic2, cc, sale, con) => new
                 {
                     AllM = a,
                     ProjectName = b.ProjectName,
                     ProjectCode = b.ProjectCode,
                     ReturnTypeText = dic1.DictionaryText,
                     ReturnWayText = dic2.DictionaryText,
                     companyName = cc.ClientName,
                     MArea = sale.AreaName,
                     salesman = con.ContactsName,
                 })
                 .Select(t =>
                 {
                     var mapp = Mapper.Map<Payment_CollectionOutput>(t.AllM);
                     mapp.Pt_Name = t.ProjectName;
                     mapp.Pt_Code = t.ProjectCode;
                     mapp.ReturnTypeText = t.ReturnTypeText;
                     mapp.ReturnWayText = t.ReturnWayText;
                     mapp.ClientName = t.companyName;
                     mapp.MarketAreaTxt = t.MArea;
                     mapp.PersonInChargeName = t.salesman;
                     return mapp;
                 }).ToList();
            foreach (var item in clienclist)
            {
                //获取项目总金额
                decimal salePrice = 0;
                var salePirceList = _projectDeviceDetailsService.GetList().Where(t => t.ProjectID == item.Pt_Id).ToList();
                foreach (var salePitem in salePirceList)
                {
                    //累加设备金额
                    salePrice += salePitem.Qty * salePitem.DevicePrice;
                }
                //计算当前项目编号下合同金额之和
                var ProContractAmount = _contract_InfoService.GetList().Where(t => t.pt_id == item.Pt_Id).Sum(t => t.ct_cash);
                //得出项目已签合同金额
                item.pt_SignedCash = salePrice - ProContractAmount;
                //获取项目未签合同金额
                item.pt_UnSignedCash = salePrice - item.pt_SignedCash;
                //计算当前项目下合同开票金额(合同已开票金额)
                var ctInvoiceAmount = _projectInvoicingService.GetList().Where(t => t.ProjectId == item.Pt_Id).Sum(t => t.InvoiceAmount);
                item.ctInvoiceAmount = ctInvoiceAmount;
                //计算合同未开票金额
                item.ctUnInvoiceAmount = salePrice / Convert.ToDecimal(1.13) - ctInvoiceAmount;
                //计算回款比
                if (item.Payment_Cash != 0 && salePrice != 0)
                {
                    item.Payment_Precent = item.Payment_Cash / salePrice;
                }
            }
            return new List<Payment_CollectionOutput>(clienclist);
        }


        public List<Payment_CollectionOutput> GetListByClientId(int clietId)
        {
            var clienclist = Repository.Select.From<ProjectBasicsEntity, DictionaryEntity,
                DictionaryEntity, Client_CompanyEntity, bd_SalesAreaInfoEntity, ContactsEntity>((a, b, dic1, dic2, cc, sale, con) => a
                  .LeftJoin(ab => ab.Pt_Id == b.Id)
                  .LeftJoin(ad => ad.ReturnType == dic1.Id)
                  .LeftJoin(ad => ad.ReturnWay == dic2.Id))
                .LeftJoin(t => t.t2.ClientCompany == t.t5.Id)
                .LeftJoin(t => t.t5.MarketArea == t.t6.Id)
                .LeftJoin(t => t.t5.PersonInCharge == t.t7.Id)
                 .Where(t => t.t2.ClientCompany == clietId)
                 .ToList((a, b, dic1, dic2, cc, sale, con) => new
                 {
                     AllM = a,
                     ProjectName = b.ProjectName,
                     ProjectCode = b.ProjectCode,
                     ReturnTypeText = dic1.DictionaryText,
                     ReturnWayText = dic2.DictionaryText,
                     companyName = cc.ClientName,
                     MArea = sale.AreaName,
                     salesman = con.ContactsName,
                 })
                 .Select(t =>
                 {
                     var mapp = Mapper.Map<Payment_CollectionOutput>(t.AllM);
                     mapp.Pt_Name = t.ProjectName;
                     mapp.Pt_Code = t.ProjectCode;
                     mapp.ReturnTypeText = t.ReturnTypeText;
                     mapp.ReturnWayText = t.ReturnWayText;
                     mapp.ClientName = t.companyName;
                     mapp.MarketAreaTxt = t.MArea;
                     mapp.PersonInChargeName = t.salesman;
                     return mapp;
                 }).ToList();
            foreach (var item in clienclist)
            {
                //获取项目总金额
                decimal salePrice = 0;
                var salePirceList = _projectDeviceDetailsService.GetList().Where(t => t.ProjectID == item.Pt_Id).ToList();
                foreach (var salePitem in salePirceList)
                {
                    //累加设备金额
                    salePrice += salePitem.Qty * salePitem.DevicePrice;
                }
                //计算当前项目编号下合同金额之和
                var ProContractAmount = _contract_InfoService.GetList().Where(t => t.pt_id == item.Pt_Id).Sum(t => t.ct_cash);
                //得出项目已签合同金额
                item.pt_SignedCash = salePrice - ProContractAmount;
                //获取项目未签合同金额
                item.pt_UnSignedCash = salePrice - item.pt_SignedCash;
                //计算当前项目下合同开票金额(合同已开票金额)
                var ctInvoiceAmount = _projectInvoicingService.GetList().Where(t => t.ProjectId == item.Pt_Id).Sum(t => t.InvoiceAmount);
                item.ctInvoiceAmount = ctInvoiceAmount;
                //计算合同未开票金额
                item.ctUnInvoiceAmount = salePrice / Convert.ToDecimal(1.13) - ctInvoiceAmount;
                //计算回款比
                if (item.Payment_Cash != 0 && salePrice != 0)
                {
                    item.Payment_Precent = item.Payment_Cash / salePrice;
                }
            }
            return new List<Payment_CollectionOutput>(clienclist);
        }


        /// <summary>
        /// 添加回款
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool Insert(Payment_CollectionDto input)
        {
            var entity = Mapper.Map<Payment_CollectionEntity>(input);
            base.Insert(entity);
            return true;
        }
        /// <summary>
        /// 编辑回款
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool Update(Payment_CollectionDto input)
        {
            base.Update(input.Id, input);
            return true;
        }

        /// <summary>
        /// 根据ID数组获取回款信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<Payment_CollectionOutput> GetProIdsList(int[] Ids)
        {
            var clienclist = Repository.Select.From<ProjectBasicsEntity, DictionaryEntity,
                DictionaryEntity>((a, b, dic1, dic2) => a
                  .LeftJoin(ab => ab.Pt_Id == b.Id)
                  .LeftJoin(ad => ad.ReturnType == dic1.Id)
                  .LeftJoin(ad => ad.ReturnWay == dic2.Id))
                  .Where(a => Ids.Contains(a.t1.Pt_Id))
                 .ToList((a, b, dic1, dic2) => new
                 {
                     AllM = a,
                     ProjectName = b.ProjectName,
                     ProjectCode = b.ProjectCode,
                     ReturnTypeText = dic1.DictionaryText,
                     ReturnWayText = dic2.DictionaryText
                 })
                 .Select(t =>
                 {
                     var mapp = Mapper.Map<Payment_CollectionOutput>(t.AllM);
                     mapp.Pt_Name = t.ProjectName;
                     mapp.Pt_Code = t.ProjectCode;
                     mapp.ReturnTypeText = t.ReturnTypeText;
                     mapp.ReturnWayText = t.ReturnWayText;
                     return mapp;
                 }).ToList();
            return new List<Payment_CollectionOutput>(clienclist);
        }
    }
}
