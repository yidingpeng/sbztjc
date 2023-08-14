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
    public class ProjectInvoicingService : CrudApplicationService<ProjectInvoicingEntity, int>, IProjectInvoicingService
    {
        private readonly IContract_InfoService _contract_InfoService;

        public ProjectInvoicingService(IDataValidatorProvider dataValidator, IRepository<ProjectInvoicingEntity, int> repository,IContract_InfoService contract_InfoService,
            IMapper mapper, Lazy<ICurrentUser> currentUser) : base(dataValidator, repository, mapper, currentUser)
        {
            _contract_InfoService = contract_InfoService;
    }

        public PagedResult<ProjectInvoicingDto> GetPagedList(ProjectInvoicingSearchDto input)
        {
            var List = Repository.Select.From<ProjectBasicsEntity, Contract_InfoEntity,UserEntity>((In, ba, con,ta) => In
               .LeftJoin(t => t.ProjectId == ba.Id)
               .LeftJoin(t => t.ContactId == con.Id)
               .LeftJoin(t=>t.ApplyMan==ta.Id))
                .WhereIf(input.ProjectCode.NotNullOrWhiteSpace(), t => t.t2.ProjectCode.Contains(input.ProjectCode))
                .WhereIf(input.ct_code.NotNullOrWhiteSpace(), t => t.t3.ct_code.Contains(input.ct_code))
                .OrderByDescending(t => t.t1.Id)
                .Count(out var total)
                .Page(input.PageNo, input.PageSize)
                .ToList((In, ba, con,ta) => new
                {
                    Mainentity = In,
                    proCode = ba.ProjectCode,
                    proName = ba.ProjectName,
                    ProReceiveDate = ba.ProjectReceiveDate,
                    ctCode = con.ct_code,
                    ctCash=con.ct_cash,
                    applyname=ta.RealName,
                })
                .Select(t =>
                {
                    var info = Mapper.Map<ProjectInvoicingDto>(t.Mainentity);
                    info.ProjectCode = t.proCode;
                    info.ProjectName = t.proName;
                    info.ProjectReceiveDate = t.ProReceiveDate;
                    info.ct_code = t.ctCode;
                    info.ct_cash = t.ctCash;
                    info.ApplyManTxt = t.applyname;
                    return info;
                }).ToList();
            //计算合同未开票金额 和 合同应开票总金额
            foreach (var item in List)
            {
                //获取该项目的合同数据
                var CtCashList = _contract_InfoService.GetList().Where(t => t.pt_id == item.ProjectId && t.IsDeleted==false).ToList();
                //获取该项目的开票数据
                var InCashList = Repository.Select.Where(t => t.ProjectId == item.ProjectId && t.IsDeleted == false).ToList();
                //获取该项目的合同金额之和
                var CtCashSum = CtCashList.Sum(t => t.ct_cash);
                //获取该项目的开票金额之和
                var InCashSum = InCashList.Sum(t => t.InvoiceAmount);
                item.ct_UninvoicedAmount = CtCashSum - InCashSum;
                decimal invoicedAmount = 0;
                //计算合同应开票总金额
                foreach (var ctchash in CtCashList)
                {
                    invoicedAmount += ctchash.ct_cash / Convert.ToDecimal(1.13);
                }
                item.ct_InvoicedAmount = invoicedAmount;
            }

            return new PagedResult<ProjectInvoicingDto>(Mapper.Map<List<ProjectInvoicingDto>>(List), total);
        }

        public List<ProjectInvoicingDto> GetByIdList(int Id)
        {
            var List = Repository.Select.From<ProjectBasicsEntity, Contract_InfoEntity, UserEntity>((In, ba, con, ta) => In
                .LeftJoin(t => t.ProjectId == ba.Id)
                .LeftJoin(t => t.ContactId == con.Id)
                .LeftJoin(t => t.ApplyMan == ta.Id))
                .Where(t=>t.t1.ProjectId==Id)
                .ToList((In, ba, con, ta) => new
                {
                    Mainentity = In,
                    proCode = ba.ProjectCode,
                    proName = ba.ProjectName,
                    ctCode = con.ct_code,
                    ctCash = con.ct_cash,
                    applyname = ta.RealName,
                })
                .Select(t =>
                {
                    var info = Mapper.Map<ProjectInvoicingDto>(t.Mainentity);
                    info.ProjectCode = t.proCode;
                    info.ProjectName = t.proName;
                    info.ct_code = t.ctCode;
                    info.ct_cash = t.ctCash;
                    info.ApplyManTxt = t.applyname;
                    return info;
                }).ToList();
            //计算合同未开票金额 和 合同应开票总金额
            foreach (var item in List)
            {
                //获取该项目的合同数据
                var CtCashList = _contract_InfoService.GetList().Where(t => t.pt_id == item.ProjectId && t.IsDeleted == false).ToList();
                //获取该项目的开票数据
                var InCashList = Repository.Select.Where(t => t.ProjectId == item.ProjectId && t.IsDeleted == false).ToList();
                //获取该项目的合同金额之和
                var CtCashSum = CtCashList.Sum(t => t.ct_cash);
                //获取该项目的开票金额之和
                var InCashSum = InCashList.Sum(t => t.InvoiceAmount);
                item.ct_UninvoicedAmount = CtCashSum - InCashSum;
                decimal invoicedAmount = 0;
                //计算合同应开票总金额
                foreach (var ctchash in CtCashList)
                {
                    invoicedAmount += ctchash.ct_cash / Convert.ToDecimal(1.13);
                }
                item.ct_InvoicedAmount = invoicedAmount;
            }

            return new List<ProjectInvoicingDto>(List);
        }

        public List<ProjectInvoicingDto> GetListByClientId(int clientId)
        {
            var List = Repository.Select.From<ProjectBasicsEntity, Contract_InfoEntity, UserEntity>((In, ba, con, ta) => In
                .LeftJoin(t => t.ProjectId == ba.Id)
                .LeftJoin(t => t.ContactId == con.Id)
                .LeftJoin(t => t.ApplyMan == ta.Id))
                .Where(t => t.t2.ClientCompany==clientId)
                .ToList((In, ba, con, ta) => new
                {
                    Mainentity = In,
                    proCode = ba.ProjectCode,
                    proName = ba.ProjectName,
                    ctCode = con.ct_code,
                    ctCash = con.ct_cash,
                    applyname = ta.RealName,
                })
                .Select(t =>
                {
                    var info = Mapper.Map<ProjectInvoicingDto>(t.Mainentity);
                    info.ProjectCode = t.proCode;
                    info.ProjectName = t.proName;
                    info.ct_code = t.ctCode;
                    info.ct_cash = t.ctCash;
                    info.ApplyManTxt = t.applyname;
                    return info;
                }).ToList();
            //计算合同未开票金额 和 合同应开票总金额
            foreach (var item in List)
            {
                //获取该项目的合同数据
                var CtCashList = _contract_InfoService.GetList().Where(t => t.pt_id == item.ProjectId && t.IsDeleted == false).ToList();
                //获取该项目的开票数据
                var InCashList = Repository.Select.Where(t => t.ProjectId == item.ProjectId && t.IsDeleted == false).ToList();
                //获取该项目的合同金额之和
                var CtCashSum = CtCashList.Sum(t => t.ct_cash);
                //获取该项目的开票金额之和
                var InCashSum = InCashList.Sum(t => t.InvoiceAmount);
                item.ct_UninvoicedAmount = CtCashSum - InCashSum;
                decimal invoicedAmount = 0;
                //计算合同应开票总金额
                foreach (var ctchash in CtCashList)
                {
                    invoicedAmount += ctchash.ct_cash / Convert.ToDecimal(1.13);
                }
                item.ct_InvoicedAmount = invoicedAmount;
            }

            return new List<ProjectInvoicingDto>(List);
        }

        public bool IsExist(ProjectInvoicingDto input)
        {
            var exist = Repository.Where(t => t.InvoiceNo == input.InvoiceNo).ToOne();
            if (exist == null) return false;
            if (input.Id.HasValue)
            {
                return input.Id.Value != exist.Id;
            }
            return true;
        }

    }
}
