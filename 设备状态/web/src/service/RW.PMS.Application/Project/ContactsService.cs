using AutoMapper;
using RW.PMS.Application.Contracts.Project;
using RW.PMS.Application.Contracts.DTO;
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
using RW.PMS.Application.Contracts.DTO.Project;

namespace RW.PMS.Application.Project
{
    public class ContactsService : CrudApplicationService<ContactsEntity, int>, IContactsService
    {
        private readonly IClientCompanyService _clientCompanyService;
        public ContactsService(IDataValidatorProvider dataValidator, IClientCompanyService clientCompanyService,
           IRepository<ContactsEntity, int> repository, IMapper mapper, Lazy<ICurrentUser> currentUser) : base(
           dataValidator, repository, mapper, currentUser)
        {
            _clientCompanyService = clientCompanyService;
        }
        /// <summary>
        /// 联系人分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PagedResult<ContactListDto> GetPagedList(ContractSearchDto input)
        {
            var list = Repository.Select.From<Client_CompanyEntity, DictionaryEntity, OrganizationEntity, DictionaryEntity, bd_SalesAreaInfoEntity>
                ((c, com, d, org, d2, sa) => c
                .LeftJoin(t => t.CompanyId == com.Id)
                .LeftJoin(t => t.ContactsCategory == d.Id)
                .LeftJoin(t => t.Department == org.Id))
                //.LeftJoin(t => t.t2.PersonInCharge == t.t1.Id)
                .LeftJoin(t => t.t2.ClientRank == t.t5.Id)
                .LeftJoin(t => t.t2.MarketArea == t.t6.Id)
                .WhereIf(input.ClientName.NotNullOrWhiteSpace(), (c, com, d, org, d2, sa) => com.ClientName.Contains(input.ClientName))
                .WhereIf(input.MarketArea != 0, (c, com, d, org, d2, sa) => com.MarketArea == input.MarketArea)
                .WhereIf(input.ClientRank != 0, (c, com, d, org, d2, sa) => com.ClientRank == input.ClientRank)
                .WhereIf(input.ContactsName.NotNullOrWhiteSpace(), (c, com, d, org, d2, sa) => c.ContactsName.Contains(input.ContactsName))
               .OrderByDescending(t => t.t1.Id)
               .Count(out var total)
               .Page(input.PageNo, input.PageSize).ToList((c, com, d, org, d2, sa) => new
               {
                   contact = c,
                   comcode = com.CompanyCode,
                   category = d.DictionaryText,
                   dep = org.OrganizationName,
                   CurCompany = com.ClientFullName,
                   //chargName = c.ContactsName,
                   clientName = com.ClientName,
                   chargeId = com.PersonInCharge,
                   marketArea = sa.AreaName,
                   clientRank = d2.DictionaryText,
               }).Select(t =>
               {
                   var info = Mapper.Map<ContactListDto>(t.contact);
                   info.ContactsCategorytext = t.category;
                   info.CurCompany = t.CurCompany;
                   info.Departmenttext = t.dep;
                   info.ComCode = t.comcode;
                   info.ClientName = t.clientName;
                   info.MarketAreaTxt = t.marketArea;
                   //info.PersonInChargeName = t.chargName;
                   info.PersonInCharge = t.chargeId;
                   info.ClientRankTxt = t.clientRank;
                   info.SexName = info.Sex == 1 ? "男" : info.Sex == 2 ? "女" : "保密";
                   return info;
               }).ToList();
            var allList = Repository.Select.ToList();
            foreach (var item in list)
            {
                var a = allList.Where(t => t.Id == item.PersonInCharge).FirstOrDefault();
                if (a != null)
                {
                    item.PersonInChargeName = a.ContactsName;
                }
            }
            return new PagedResult<ContactListDto>(list, total);
        }

        public PagedResult<ContactListDto> GetPagedListByRol(string ContactsName, int rolId, int PageNo, int PageSize,string QueryType)
        {
            var list = Repository.Select.From<Client_CompanyEntity, DictionaryEntity, DictionaryEntity>((c, com, d, d2) => c
                 .LeftJoin(t => t.CompanyId == com.Id)
                 .LeftJoin(t => t.ContactsCategory == d.Id)
                 .LeftJoin(t => t.Department == d2.Id))
                .WhereIf(ContactsName.NotNullOrWhiteSpace(), (c, com, d, d2) => c.ContactsName.Contains(ContactsName))
                .WhereIf(QueryType.NotNullOrWhiteSpace(), (c, com, d, d2) => d.DictionaryText.Equals(QueryType))
                .OrderByDescending(t => t.t1.Id)
                .Count(out var total)
                /*.Page(PageNo, PageSize)*/
                .ToList((c, com, d, d2) => new
                {
                    contact = c,
                    comcode = com.CompanyCode,
                    category = d.DictionaryText,
                    dep = d2.DictionaryText
                }).Select(t =>
                {
                    var info = Mapper.Map<ContactListDto>(t.contact);
                    info.ContactsCategorytext = t.category;
                    info.Departmenttext = t.dep;
                    info.ComCode = t.comcode;
                    info.SexName = info.Sex == 1 ? "男" : info.Sex == 2 ? "女" : "保密";
                    return info;
                }).ToList();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Id == rolId)
                {
                    ContactListDto firstRow = list[i];
                    for (int j = i; j > 0; j--)
                    {
                        list[j] = list[j - 1];
                    }
                    list[0] = firstRow;
                }
            }
            var IEnumerableList = list.Skip((PageNo - 1) * PageSize).Take(PageSize);
            var ResultList = new List<ContactListDto>(IEnumerableList);
            return new PagedResult<ContactListDto>(ResultList, total);
        }

        public List<ContactListDto> GetListByClientId(int ClientId)
        {
            var list = Repository.Select.From<Client_CompanyEntity, DictionaryEntity, OrganizationEntity, DictionaryEntity, bd_SalesAreaInfoEntity>
                ((c, com, d, org, d2, sa) => c
                .LeftJoin(t => t.CompanyId == com.Id)
                .LeftJoin(t => t.ContactsCategory == d.Id)
                .LeftJoin(t => t.Department == org.Id))
                .LeftJoin(t => t.t2.ClientRank == t.t5.Id)
                .LeftJoin(t => t.t2.MarketArea == t.t6.Id)
                .Where(t => t.t2.Id == ClientId)
                .ToList((c, com, d, org, d2, sa) => new
                {
                    contact = c,
                    comcode = com.CompanyCode,
                    category = d.DictionaryText,
                    dep = org.OrganizationName,
                    CurCompany = com.ClientFullName,
                    clientName = com.ClientName,
                    chargeId = com.PersonInCharge,
                    marketArea = sa.AreaName,
                    clientRank = d2.DictionaryText,
                }).Select(t =>
                {
                    var info = Mapper.Map<ContactListDto>(t.contact);
                    info.ContactsCategorytext = t.category;
                    info.CurCompany = t.CurCompany;
                    info.Departmenttext = t.dep;
                    info.ComCode = t.comcode;
                    info.ClientName = t.clientName;
                    info.MarketAreaTxt = t.marketArea;
                    info.PersonInCharge = t.chargeId;
                    info.ClientRankTxt = t.clientRank;
                    info.SexName = info.Sex == 1 ? "男" : info.Sex == 2 ? "女" : "保密";
                    return info;
                }).ToList();
            var allList = Repository.Select.ToList();
            foreach (var item in list)
            {
                var a = allList.Where(t => t.Id == item.PersonInCharge).FirstOrDefault();
                if (a != null)
                {
                    item.PersonInChargeName = a.ContactsName;
                }
            }
            return new List<ContactListDto>(list);
        }

        public List<ContactListDto> GetListById(int Id)
        {
            var list = Repository.Select.From<Client_CompanyEntity, DictionaryEntity, OrganizationEntity, DictionaryEntity, bd_SalesAreaInfoEntity>
                ((c, com, d, org, d2, sa) => c
                .LeftJoin(t => t.CompanyId == com.Id)
                .LeftJoin(t => t.ContactsCategory == d.Id)
                .LeftJoin(t => t.Department == org.Id))
                .LeftJoin(t => t.t2.ClientRank == t.t5.Id)
                .LeftJoin(t => t.t2.MarketArea == t.t6.Id)
                .Where(t => t.t1.Id == Id)
                .ToList((c, com, d, org, d2, sa) => new
                {
                    contact = c,
                    comcode = com.CompanyCode,
                    category = d.DictionaryText,
                    dep = org.OrganizationName,
                    CurCompany = com.ClientFullName,
                    clientName = com.ClientName,
                    chargeId = com.PersonInCharge,
                    marketArea = sa.AreaName,
                    clientRank = d2.DictionaryText,
                }).Select(t =>
                {
                    var info = Mapper.Map<ContactListDto>(t.contact);
                    info.ContactsCategorytext = t.category;
                    info.CurCompany = t.CurCompany;
                    info.Departmenttext = t.dep;
                    info.ComCode = t.comcode;
                    info.ClientName = t.clientName;
                    info.MarketAreaTxt = t.marketArea;
                    info.PersonInCharge = t.chargeId;
                    info.ClientRankTxt = t.clientRank;
                    info.SexName = info.Sex == 1 ? "男" : info.Sex == 2 ? "女" : "保密";
                    return info;
                }).ToList();
            var allList = Repository.Select.ToList();
            foreach (var item in list)
            {
                var a = allList.Where(t => t.Id == item.PersonInCharge).FirstOrDefault();
                if (a != null)
                {
                    item.PersonInChargeName = a.ContactsName;
                }
            }
            return new List<ContactListDto>(list);
        }

        /// <summary>
        /// 添加联系人
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool Insert(ContactListDto input)
        {
            var entity = Mapper.Map<ContactsEntity>(input);
            entity.Birthday = input.Birthday;
            if (input.Birthday == DateTime.MinValue) entity.Age = 0;
            else entity.Age = CalculateAge(input.Birthday);
            base.Insert(entity);
            return true;
        }
        /// <summary>
        /// 修改联系人
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool Update(ContactListDto input)
        {
            if (input.Birthday == DateTime.MinValue) input.Age = 0;
            else input.Age = CalculateAge(input.Birthday);
            base.Update(input.Id.Value, input);

            return true;
        }
        /// <summary>
        /// 根据生日自动计算年龄
        /// </summary>
        /// <param name="birthday"></param>
        /// <returns></returns>
        public int CalculateAge(DateTime birthday)
        {
            var nowtime = DateTime.Now;
            var age = nowtime.Year - birthday.Year;
            if (nowtime.Month < birthday.Month || (nowtime.Month == birthday.Month && nowtime.Day < birthday.Day))
            {
                age--;
            }
            return age;
        }
    }
}
