using System;
using System.Collections.Generic;
using System.Dynamic;
using AutoMapper;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Organization;
using RW.PMS.Application.Contracts.Input.System;
using RW.PMS.Application.Contracts.Output.System;
using RW.PMS.Application.Contracts.System;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.Extensions;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.System;
using RW.PMS.Domain.Repositories;
using System.Linq;
using RW.PMS.Application.Contracts.DTO.User;

namespace RW.PMS.Application.System
{
    public class OrganizationService : CrudApplicationService<OrganizationEntity, int>, IOrganizationService
    {
        private List<OrganizationEntity> AddList = new List<OrganizationEntity>();
        public OrganizationService(IDataValidatorProvider dataValidator,
            IRepository<OrganizationEntity, int> repository, IMapper mapper, Lazy<ICurrentUser> currentUser) : base(
            dataValidator, repository, mapper, currentUser)
        {
        }

        //public OrganizationView GetView(int id)
        //{
        //    var select = Repository.Select
        //        .From<OrganizationEntity>((c, p) => c
        //            .LeftJoin(a => a.ParentId == p.Id))
        //        .Where(t => t.t1.Id == id)
        //        .ToOne(t => new
        //        {
        //            Entity = t.t1,
        //            Parent = new {t.t2.Id, t.t2.OrganizationName}
        //        });
        //    var view = Mapper.Map<OrganizationView>(select.Entity);
        //    view.Parent = new ExpandoObject();
        //    view.Parent.Id = select.Parent.Id;
        //    view.Parent.OrganizationName = select.Parent.OrganizationName;
        //    return view;
        //}

        //public PagedResult<OrganizationListDto> GetPagedList(OrganizationSearchDto input)
        //{
        //    var organizations = Repository.Select
        //        //.WhereIf(input.ParentId.HasValue, t => t.ParentId <= input.ParentId.Value)
        //        //.WhereIf(input.OrganizationType > 0, t => t.OrganizationType == input.OrganizationType)
        //        .WhereIf(input.Key.NotNullOrWhiteSpace(),
        //            t => t.OrganizationName.Contains(input.Key) || t.OrganizationCode.Contains(input.Key))
        //        .Count(out var total).ToList();


        //    var lst = GetTree(organizations, 0);
        //    return new PagedResult<OrganizationListDto>(lst, total);
        //}

        public List<OrganizationListDto> GetTreeList(OrganizationSearchDto search)
        {
            var list = Repository
                .WhereIf(!string.IsNullOrEmpty(search.Key), x => x.OrganizationName.Contains(search.Key) || x.OrganizationCode.Contains(search.Key))
                .ToList();
            //无查询结果返回一个空的list
            if (!list.Any())
            {
                return new List<OrganizationListDto>();
            }
            //如果有查询条件，则判断查询出来的list中是否存在子部门，如果存在则判断该子项的父项是否存在list，不存在则查询并添加
            if (search.Key.NotNullOrWhiteSpace())
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
            var users = Repository.Orm.Select<UserEntity>().Where(x=>x.IsDeleted==false)
                .ToList(x => new { x.Id, x.RealName })
                .ToDictionary(x => x.Id, x => x.RealName);
            var OrgType = Repository.Orm.Select<DictionaryEntity>().Where(x => x.IsDeleted == false)
               .ToList(x => new { x.Id, x.DictionaryText })
               .ToDictionary(x => x.Id, x => x.DictionaryText);
            return BuildTreeList<OrganizationListDto, OrganizationEntity>(list,0,x=>x.OrderIndex).Select(x => x.SetLeader(users)).Select(x=>x.SetOrgType(OrgType)).ToList();
        }

        public List<OrganizationListDto> GetByParentIdList(int id)
        {
            var list = Repository.Where(t=>t.IsDeleted==false).ToList();
            //无查询结果返回一个空的list
            if (!list.Any())
            {
                return new List<OrganizationListDto>();
            }
            var users = Repository.Orm.Select<UserEntity>().Where(x => x.IsDeleted == false)
                .ToList(x => new { x.Id, x.RealName })
                .ToDictionary(x => x.Id, x => x.RealName);
            var treelist = BuildTreeList<OrganizationListDto, OrganizationEntity>(list).Select(x => x.SetLeader(users)).ToList();
            List<OrganizationListDto> returnList = new List<OrganizationListDto>();
            foreach (var item in treelist)
            {
                if (item.Children!=null)
                {
                    foreach (var ChildItem in item.Children)
                    {
                        if (ChildItem.ParentId == id)
                        {
                            returnList.Add(ChildItem);
                        }
                    }
                }
            }
            return returnList;
        }

        //判断子项是否有父项，如果有则判断该父项是否已存在Alllist中，没有则添加；再循环调用本方法判断该父项是否还存在父项
        public void GetParentInfo(List<OrganizationEntity> Alllist, OrganizationEntity item)
        {
            if (item.ParentId != 0)
            {
                //获取该子项的父项信息
                var list = Repository.Where(t => t.Id == item.ParentId).ToList();
                if (list.Any())
                {
                    //判断父项信息是否已存在，不存在则添加
                    if (!Alllist.Where(t=>t.Id==list[0].Id).ToList().Any())
                    {
                        AddList.Add(list[0]);
                    }
                }
                //调用本方法判断查询出来的父项是否还存在父项
                foreach (var QueryItem in list)
                {
                    GetParentInfo(Alllist,QueryItem);
                }
            }
        }


        //public IList<OrganizationTreeOutput> GetTreeList()
        //{
        //    var list = GetList();
        //    return BuildTreeList<OrganizationTreeOutput, OrganizationEntity>(list);
        //}
        public int addNewOrgnization(string OrgName)
        {
            var a = Insert(new OrganizationEntity { ParentId = 0, OrganizationCode = "OC" + DateTime.Today.ToString("yyyy-MM-dd") + new Random().Next(100, 999), OrganizationName = OrgName, OrganizationType = 0, Sort = 0 });
            if (a == null)
            {
                return 0;
            }
            return a.Id;
        }

        public bool OrganizationCodeExist(OrganizationListDto input)
        {
            var exist = Repository.Where(t => t.OrganizationCode == input.Code || t.OrganizationName.Equals(input.Name)).ToOne();
            if (exist == null) return false;
            if (input.Id > 0)
            {
                return input.Id != exist.Id;
            }
            return true;
        }
    }
}