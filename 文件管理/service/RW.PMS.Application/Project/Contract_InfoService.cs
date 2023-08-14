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
using System.Dynamic;
using System.Linq;

namespace RW.PMS.Application.BaseInfo
{
    public class Contract_InfoService : CrudApplicationService<Contract_InfoEntity, int>, IContract_InfoService
    {
        private readonly IContractInfoFilesService _projectInfoFilesService;
        private readonly IProjectContactsService _projectContactsService;
        IContractDetailService _cds;
        public Contract_InfoService(IDataValidatorProvider dataValidator, IRepository<Contract_InfoEntity, int> repository,
            IContractInfoFilesService projectInfoFilesService, IProjectContactsService projectContactsService, IContractDetailService cds,
            IMapper mapper, Lazy<ICurrentUser> currentUser) : base(dataValidator, repository, mapper, currentUser)
        {
            _projectInfoFilesService = projectInfoFilesService;
            _projectContactsService = projectContactsService;
            _cds = cds;
        }

        public PagedResult<Contract_InfoDto> GetContractPagedList(Contract_InfoSearchDto input)
        {
            string a = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            string[] baseUrl = a.Split("\\bin");
            var proList = Repository.Orm.Select<ProjectBasicsEntity>().ToList();
            var list = Repository.Select.WhereIf(input.ct_code.NotNullOrWhiteSpace(), t => t.ct_code.Contains(input.ct_code) || t.contractName.Contains(input.ct_code))
                //.WhereIf(input.pt_Name.NotNullOrWhiteSpace(), t => t.t2.ProjectName.Contains(input.pt_Name))
                //.WhereIf(input.MarketArea != 0, t => t.t3.MarketArea == input.MarketArea)
                //.WhereIf(input.ClientName.NotNullOrWhiteSpace(), t => t.t3.ClientName.Contains(input.ClientName))
                .OrderByDescending(t => t.CreatedAt)
                .Count(out var total)
                .Page(input.PageNo, input.PageSize)
                 .ToList(ct => new
                 {
                     M = ct,
                 })
                 .Select(t =>
                 {
                     var output = Mapper.Map<Contract_InfoDto>(t.M);
                     output.ContractDetailList = _cds.GetByPIDList(output.Id);
                     return output;
                 });

            //获取合同扫描件和合同可编辑件的地址
            var fileList = _projectInfoFilesService.GetList()
                .Where(t => t.ContentType == "application/pdf" ||
                       t.ContentType == "application/msword" ||
                       t.ContentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
            //var pdfList = _projectInfoFilesService.GetList().Where(t => t.ContentType == "application/pdf").ToList();
            //var wordList = _projectInfoFilesService.GetList().Where(t => t.ContentType == "application/msword" ||
            //t.ContentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document").ToList();
            List < Contract_InfoDto> newList = list.ToList();
            foreach (var item in newList)
            {
                if (!string.IsNullOrWhiteSpace(item.pt_idsTxt))
                {
                    string[] arrs = item.pt_idsTxt.Split(",");
                    if (arrs.Length > 0)
                    {
                        int[] intTemp = new int[arrs.Length];
                        for (int i = 0; i < arrs.Length; i++)
                        {
                            int.TryParse(arrs[i], out intTemp[i]);//int.TryParse函数返回Bool型。不会抛出异常
                        }
                        string ptNamesTxt = string.Empty;
                        string ptCodesTxt = string.Empty;
                        var proInfoList = proList.Where(o => intTemp.Contains(o.Id)).ToList();
                        foreach (var proinfo in proInfoList)
                        {
                            int index = proInfoList.IndexOf(proinfo);//下标从0开始
                            ptNamesTxt += index + 1 == proInfoList.Count ? proinfo.ProjectName : proinfo.ProjectName + ',';
                            ptCodesTxt += index + 1 == proInfoList.Count ? proinfo.ProjectCode : proinfo.ProjectCode + ',';
                        }
                        item.pt_codesTxt = ptNamesTxt;
                        item.pt_NamesTxt = ptCodesTxt;
                    }

                }
                var pdfList = fileList.Where(o => o.PID == item.Id && o.ContentType == "application/pdf");
                var wordList = fileList.Where(t => t.PID == item.Id && (t.ContentType == "application/msword" ||
                t.ContentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document"));
                
                List<string> pdfPath = new();
                List<string> wordPath = new();
                List<string> pdfName = new();
                List<string> wordName = new();
                List<int> pdfIds = new();
                List<int> wordIds = new();
                foreach (var pdfItem in pdfList)
                {
                    pdfPath.Add(baseUrl[0] + pdfItem.RelativePath);
                    pdfName.Add(pdfItem.FileName);
                    pdfIds.Add(pdfItem.Id);
                }
                foreach (var wordItem in wordList)
                {
                    wordPath.Add(baseUrl[0] + wordItem.RelativePath);
                    wordName.Add(wordItem.FileName);
                    wordIds.Add(wordItem.Id);
                }
                item.ct_attachmentPdfUrl = pdfPath.ToArray();
                item.filePdfName = pdfName.ToArray();
                item.filePdfIds = pdfIds.ToArray();
                item.ct_attachmentWordUrl = wordPath.ToArray();
                item.fileWordName = wordName.ToArray();
                item.fileWordIds = wordIds.ToArray();
            }
            //获取客户公司信息营销经理
            //var PersonInChargeList = _projectContactsService.GetAllList();

            //foreach (var item in newList)
            //{
            //    var chargeName = "";
            //    string[] ArrPt_Ids = item.pt_idsTxt.Split(",");
            //    foreach (var pt in ArrPt_Ids)
            //    {
            //        foreach (var chargeItem in PersonInChargeList)
            //        {
            //            if (Int32.Parse(pt) == chargeItem.PID && chargeItem.FZBKName == "营销经理")
            //            {
            //                chargeName += chargeItem.ContactsName + ',';
            //                break;
            //            }
            //            //else
            //            //    item.PersonInChargeName = "";
            //        }
            //    }
            //    item.PersonInChargeName = chargeName==""?"":chargeName.Substring(0, chargeName.Length - 1);
            //}
            //if (input.PersonInChargeName.NotNullOrWhiteSpace())
            //{
            //    newList = newList.Where(a => a.PersonInChargeName.Contains(input.PersonInChargeName)).ToList();
            //}

            return new PagedResult<Contract_InfoDto>(newList, total);
        }
        /// <summary>
        /// 根据ID获取合同信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public List<Contract_InfoDto> GetByIdList(int Id)
        {
            string a = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            string[] baseUrl = a.Split("\\bin");
            List<Contract_InfoDto> QueryList = new List<Contract_InfoDto>();
            var list = Repository.Select.From<ProjectBasicsEntity, Client_CompanyEntity, bd_SalesAreaInfoEntity, ContactsEntity>(
                (ct, pb, cc, sale, con) => ct
                .LeftJoin(ct => ct.pt_id == pb.Id))
                .LeftJoin(t => t.t2.ClientCompany == t.t3.Id)
                .LeftJoin(t => t.t3.MarketArea == t.t4.Id)
                .LeftJoin(t => t.t3.PersonInCharge == t.t5.Id)
                //.WhereIf(Id>0, t => t.t1.pt_idsTxt.Contains(Convert.ToString(Id)) && t.t1.IsDeleted == false)
                .OrderByDescending(t => t.t1.CreatedAt)
                 .ToList((ct, pb, cc, sale, con) => new
                 {
                     M = ct,
                     ptCode = pb.ProjectCode,
                     ptName = pb.ProjectName,
                     companyName = cc.ClientName,
                     MArea = sale.AreaName,
                     salesman = con.ContactsName,
                 })
                 .Select(t =>
                 {
                     var output = Mapper.Map<Contract_InfoDto>(t.M);
                     output.pt_codeTxt = t.ptCode;
                     output.pt_Name = t.ptName;
                     output.ClientName = t.companyName;
                     output.MarketAreaTxt = t.MArea;
                     output.PersonInChargeName = t.salesman;
                     return output;
                 }).ToList();
            //foreach (var item in list)
            //{
            //    string[] ArrPt_Ids = item.pt_idsTxt.Split(",");
            //    foreach (var ptidItem in ArrPt_Ids)
            //    {
            //        if (Int32.Parse(ptidItem)== Id)
            //        {
            //            QueryList.Add(item);
            //        }
            //    }
            //}
            var pdfList = _projectInfoFilesService.GetList().Where(t => t.ContentType == "application/pdf").ToList();
            var wordList = _projectInfoFilesService.GetList().Where(t => t.ContentType == "application/msword" ||
            t.ContentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document").ToList();
            List<Contract_InfoDto> newList = QueryList.ToList();
            foreach (var item in newList)
            {
                List<string> pdfPath = new List<string>();
                List<string> wordPath = new List<string>();
                List<string> pdfName = new List<string>();
                List<string> wordName = new List<string>();
                foreach (var pdfItem in pdfList)
                {
                    if (item.Id == pdfItem.PID)
                    {
                        pdfPath.Add(baseUrl[0] + pdfItem.RelativePath);
                        pdfName.Add(pdfItem.FileName);
                    }
                }
                foreach (var wordItem in wordList)
                {
                    if (item.Id == wordItem.PID)
                    {
                        wordPath.Add(baseUrl[0] + wordItem.RelativePath);
                        wordName.Add(wordItem.FileName);
                    }
                }
                item.ct_attachmentPdfUrl = pdfPath.ToArray();
                item.filePdfName = pdfName.ToArray();
                item.ct_attachmentWordUrl = wordPath.ToArray();
                item.fileWordName = wordName.ToArray();
            }
            return new List<Contract_InfoDto>((newList));
        }

        public List<Contract_InfoDto> GetAllList()
        {
            string a = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            string[] baseUrl = a.Split("\\bin");
            var list = Repository.Select.From<ProjectBasicsEntity, Client_CompanyEntity, bd_SalesAreaInfoEntity, ContactsEntity>(
                (ct, pb, cc, sale, con) => ct
                .LeftJoin(ct => ct.pt_id == pb.Id))
                .LeftJoin(t => t.t2.ClientCompany == t.t3.Id)
                .LeftJoin(t => t.t3.MarketArea == t.t4.Id)
                .LeftJoin(t => t.t3.PersonInCharge == t.t5.Id)
                //.Where(t => t.t2.ClientCompany==clientId)
                .OrderByDescending(t => t.t1.CreatedAt)
                 .ToList((ct, pb, cc, sale, con) => new
                 {
                     M = ct,
                     ptCode = pb.ProjectCode,
                     ptName = pb.ProjectName,
                     companyName = cc.ClientName,
                     MArea = sale.AreaName,
                     salesman = con.ContactsName,
                 })
                 .Select(t =>
                 {
                     var output = Mapper.Map<Contract_InfoDto>(t.M);
                     output.pt_codeTxt = t.ptCode;
                     output.pt_Name = t.ptName;
                     output.ClientName = t.companyName;
                     output.MarketAreaTxt = t.MArea;
                     output.PersonInChargeName = t.salesman;
                     return output;
                 });
            var pdfList = _projectInfoFilesService.GetList().Where(t => t.ContentType == "application/pdf").ToList();
            var wordList = _projectInfoFilesService.GetList().Where(t => t.ContentType == "application/msword" ||
            t.ContentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document").ToList();
            List<Contract_InfoDto> newList = list.ToList();
            foreach (var item in newList)
            {
                List<string> pdfPath = new List<string>();
                List<string> wordPath = new List<string>();
                List<string> pdfName = new List<string>();
                List<string> wordName = new List<string>();
                foreach (var pdfItem in pdfList)
                {
                    if (item.Id == pdfItem.PID)
                    {
                        pdfPath.Add(baseUrl[0] + pdfItem.RelativePath);
                        pdfName.Add(pdfItem.FileName);
                    }
                }
                foreach (var wordItem in wordList)
                {
                    if (item.Id == wordItem.PID)
                    {
                        wordPath.Add(baseUrl[0] + wordItem.RelativePath);
                        wordName.Add(wordItem.FileName);
                    }
                }
                item.ct_attachmentPdfUrl = pdfPath.ToArray();
                item.filePdfName = pdfName.ToArray();
                item.ct_attachmentWordUrl = wordPath.ToArray();
                item.fileWordName = wordName.ToArray();
            }
            return new List<Contract_InfoDto>((newList));
        }

        public string GetTheLastCtCode()
        {
            var result = Repository.Where(t => t.IsDeleted == false).OrderByDescending(t => t.Id).First();
            if (result != null)
            {
                return result.ct_code;
            }
            else
                return "";
        }


        public bool IsExist(Contract_InfoDto input)
        {
            var exist = Repository.Where(t => t.ct_code == input.ct_code).ToOne();
            if (exist == null) return false;
            if (input.Id.HasValue)
            {
                return input.Id.Value != exist.Id;
            }
            return true;
        }

        /// <summary>
        /// 增删改合同项目信息时，同步合同总金额
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool EditHTPrice(int id)
        {
            int result = 0;
            var list = _cds.GetByPIDList(id);
            if (list.Count > 0)
            {
                decimal ZJE = 0;
                foreach (var item in list)
                {
                    ZJE += item.EquipmentTotalPrice;
                }
                result = Repository.Orm.Update<Contract_InfoEntity>(id)
                    .Set(a => a.ct_cash, ZJE)
                    .Set(a => a.LastModifiedAt, DateTime.Now)
                    .ExecuteAffrows();
            }
            return result > 0;
        }
    }
}
