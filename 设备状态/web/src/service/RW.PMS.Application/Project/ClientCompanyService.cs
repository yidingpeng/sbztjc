using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.Domain.Repositories;
using AutoMapper;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.CrossCutting.Extensions;
using RW.PMS.Domain.Entities.System;
using System.Dynamic;
using RW.PMS.Domain.Entities.Project;
using RW.PMS.Application.Contracts.Project;
using RW.PMS.Application.Contracts.DTO.Project;

namespace RW.PMS.Application.Project
{
    public class ClientCompanyService : CrudApplicationService<Client_CompanyEntity, int>,IClientCompanyService
    {
        private readonly IProjectContactsService _projectContactsService;
        private readonly IProjectBasicsService _projectBasicsService;
        public ClientCompanyService(IDataValidatorProvider dataValidator,
            IProjectBasicsService projectBasicsService, IProjectContactsService projectContactsService,
           IRepository<Client_CompanyEntity, int> repository, IMapper mapper, Lazy<ICurrentUser> currentUser) : base(
           dataValidator, repository, mapper, currentUser)
        {
            _projectContactsService = projectContactsService;
            _projectBasicsService = projectBasicsService;
        }
        //分页
        public PagedResult<Client_CompanyDto> GetPagedList(Client_CompanySearchDto input)
        {
            var clienclist = Repository.Select.From<DictionaryEntity, DictionaryEntity, ContactsEntity, ContactsEntity, ContactsEntity, ContactsEntity, bd_SalesAreaInfoEntity>
                ((a, b, c, d1, d2, d3, d4, s) => a
                      .LeftJoin(ab => ab.CooperativeBusiness == b.Id)
                      .LeftJoin(ac => ac.ClientRank == c.Id)
                      .LeftJoin(ad1 => ad1.CEO == d1.Id)
                      .LeftJoin(ad2 => ad2.GM == d2.Id)
                      .LeftJoin(ad3 => ad3.DeputyGM == d3.Id)
                      .LeftJoin(ad4 => ad4.PersonInCharge == d4.Id)
                      .LeftJoin(ab5 => ab5.MarketArea == s.Id))
                 .WhereIf(input.ClientFullName.NotNullOrEmpty(), (a, b, c, d1, d2, d3, d4, s) => a.ClientFullName.Contains(input.ClientFullName))
                 .WhereIf(input.PersonInChargeName.NotNullOrEmpty(), t => t.t7.ContactsName.Contains(input.PersonInChargeName))
                 .WhereIf(input.MarketArea != 0, t => t.t1.MarketArea == input.MarketArea)
                 .WhereIf(input.ClientRank != 0, t => t.t1.ClientRank == input.ClientRank)
                 .OrderByDescending((a, b, c, d1, d2, d3, d4, s) => a.Id)
                 .Count(out var total)
                 .Page(input.PageNo, input.PageSize)
                 .ToList((a, b, c, d1, d2, d3, d4, s) => new
                 {
                     AllM = a,
                     Cooper = b.DictionaryText,
                     Client = c.DictionaryText,
                     CEON = d1.ContactsName,
                     GMN = d2.ContactsName,
                     DeputyGMN = d3.ContactsName,
                     PersonICN = d4.ContactsName,
                     marketArea = s.AreaName
                 })
                 .Select(t =>
                 {
                     var mapp = Mapper.Map<Client_CompanyDto>(t.AllM);
                     mapp.CooperativeBusinessText = t.Cooper;
                     mapp.ClientRankText = t.Client;
                     mapp.CEOName = t.CEON;
                     mapp.GMName = t.GMN;
                     mapp.DeputyGMName = t.DeputyGMN;
                     mapp.PersonInChargeName = t.PersonICN;
                     mapp.MarketAreaTxt = t.marketArea;
                     return mapp;
                 }).ToList();
            //获取客户公司对应项目id
            var ProList = _projectBasicsService.GetList();
            foreach (var item in clienclist)
            {
                foreach (var ProItem in ProList)
                {
                    if (item.Id == ProItem.ClientCompany)
                    {
                        item.ProjectId = ProItem.Id;
                        break;
                    }
                    else
                        item.ProjectId = 0;
                }
            }
            return new PagedResult<Client_CompanyDto>(Mapper.Map<List<Client_CompanyDto>>(clienclist), total);
        }

        /// <summary>
        /// 根据ID查询数据集
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public List<Client_CompanyDto> GetByIdList(int Id)
        {
            var clienclist = Repository.Select.From<DictionaryEntity, DictionaryEntity, ContactsEntity, ContactsEntity, ContactsEntity, ContactsEntity, bd_SalesAreaInfoEntity>
                 ((a, b, c, d1, d2, d3, d4, s) => a
                       .LeftJoin(ab => ab.CooperativeBusiness == b.Id)
                       .LeftJoin(ac => ac.ClientRank == c.Id)
                       .LeftJoin(ad1 => ad1.CEO == d1.Id)
                       .LeftJoin(ad2 => ad2.GM == d2.Id)
                       .LeftJoin(ad3 => ad3.DeputyGM == d3.Id)
                       .LeftJoin(ad4 => ad4.PersonInCharge == d4.Id)
                       .LeftJoin(ab5 => ab5.MarketArea == s.Id))
                  .Where(t => t.t1.Id == Id)
                  .ToList((a, b, c, d1, d2, d3, d4, s) => new
                  {
                      AllM = a,
                      Cooper = b.DictionaryText,
                      Client = c.DictionaryText,
                      CEON = d1.ContactsName,
                      GMN = d2.ContactsName,
                      DeputyGMN = d3.ContactsName,
                      PersonICN = d4.ContactsName,
                      marketArea = s.AreaName
                  })
                  .Select(t =>
                  {
                      var mapp = Mapper.Map<Client_CompanyDto>(t.AllM);
                      mapp.CooperativeBusinessText = t.Cooper;
                      mapp.ClientRankText = t.Client;
                      mapp.CEOName = t.CEON;
                      mapp.GMName = t.GMN;
                      mapp.DeputyGMName = t.DeputyGMN;
                      mapp.PersonInChargeName = t.PersonICN;
                      mapp.MarketAreaTxt = t.marketArea;
                      return mapp;
                  }).ToList();
            return new List<Client_CompanyDto>(clienclist);

        }
        //防止重复值
        public int Disf(string CompanyCode,int id)
        {
            var list = Repository.Select.Where(t => t.CompanyCode.Equals(CompanyCode)&&t.Id!=id).ToList();
            int TheCount = list.Count();
            return TheCount;
        }

        //客户公司名称列表
        public PagedResult<Client_CompanyOutput> CompanyList(Client_CompanySearchDto input)
        {
            var clienclist = Repository.Select
                 .WhereIf(input.ClientName.NotNullOrEmpty(), a => a.ClientName.Contains(input.ClientName))
                 .OrderByDescending(a => a.Id)
                 .Count(out var total)
                 .Page(input.PageNo, input.PageSize)
                 .ToList(a => new
                 {
                     AllM = a
                 })
                 .Select(t =>
                 {
                     var mapp = Mapper.Map<Client_CompanyOutput>(t.AllM);
                     return mapp;
                 }).ToList();
            return new PagedResult<Client_CompanyOutput>(Mapper.Map<List<Client_CompanyOutput>>(clienclist), total);
        }

        public Client_CompanyOutput GetModel(int? Id)
        {
            var model = Repository.Select
                .WhereIf(Id.HasValue, (e) => e.Id == Id.Value).ToList(e =>
                    new
                    {
                        Device = e,
                    })
                .Select(t =>
                {
                    var deviceListOutput = Mapper.Map<Client_CompanyOutput>(t.Device);
                    return deviceListOutput;
                }).FirstOrDefault();

            return model;
        }
  
    }
}
