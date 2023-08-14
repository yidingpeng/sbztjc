using AutoMapper;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Project;
using RW.PMS.Application.Contracts.Project;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.Project;
using RW.PMS.Domain.Entities.System;
using RW.PMS.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RW.PMS.Application.Project
{
    public class ProjectDynamicService : CrudApplicationService<ProjectDynamicEntity, int>, IProjectDynamicService
    {
        public ProjectDynamicService(IDataValidatorProvider dataValidator,
          IRepository<ProjectDynamicEntity, int> repository, IMapper mapper, Lazy<ICurrentUser> currentUser) : base(
          dataValidator, repository, mapper, currentUser)
        {

        }
        /// <summary>
        /// 项目动态分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PagedResult<ProjectDynamicView> GetPagedList(ProjectDynamicSearchDto input)
        {
            var list = Repository.Orm.Select<ProjectDynamicEntity,DictionaryEntity, ProjectBasicsEntity, UserEntity>()
            .LeftJoin(a1 => a1.t1.dyType == a1.t2.Id)
            .LeftJoin(a2 => a2.t1.projectID == a2.t3.Id)
            .LeftJoin(a3 => a3.t1.CreatedBy == a3.t4.Id)
            .OrderByDescending(t => t.t1.CreatedAt)
            .Count(out var total)
            .Page(input.PageNo, input.PageSize)
            .ToList(t => new
            {
                sales = t.t1,
                typeName = t.t2,
                ProjectName=t.t3,
                userName = t.t4
            })
            .Select(t =>
            {
                var output = Mapper.Map<ProjectDynamicView>(t.sales);
                output.dyTypeName = t.typeName == null ? "" : t.typeName.DictionaryText;
                output.projectName = t.ProjectName == null ? "" : t.ProjectName.ProjectName;
                output.CreatedByName = t.userName == null ? "" : t.userName.RealName;
                return output;
            }).ToList();
            return new PagedResult<ProjectDynamicView>(Mapper.Map<List<ProjectDynamicView>>(list), total);
        }

        /// <summary>
        /// 项目动态List
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<ProjectDynamicView> GetList(int Id)
        {
            var list = Repository.Orm.Select<ProjectDynamicEntity, DictionaryEntity, ProjectBasicsEntity,UserEntity>()
            .LeftJoin(a1 => a1.t1.dyType == a1.t2.Id)
            .LeftJoin(a2 => a2.t1.projectID == a2.t3.Id)
            .LeftJoin(a3 => a3.t1.CreatedBy == a3.t4.Id)
            .Where(o => o.t1.projectID == Id && o.t1.IsDeleted == false)
            .OrderByDescending(t => t.t1.CreatedAt)
            .ToList(t => new
            {
                sales = t.t1,
                typeName = t.t2,
                ProjectName = t.t3,
                userName=t.t4
            })
            .Select(t =>
            {
                var output = Mapper.Map<ProjectDynamicView>(t.sales);
                output.dyTypeName = t.typeName == null ? "" : t.typeName.DictionaryText;
                output.projectName = t.ProjectName == null ? "" : t.ProjectName.ProjectName;
                output.CreatedByName = t.userName == null ? "" : t.userName.RealName;
                return output;
            }).ToList();
            return new List<ProjectDynamicView>(list);
        }
        /// <summary>
        /// 项目动态新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool Insert(ProjectDynamicView input)
        {
            var entity = Mapper.Map<ProjectDynamicEntity>(input);

            base.Insert(entity);
            return true;
        }

        /// <summary>
        /// 项目动态修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool Update(ProjectDynamicView input)
        {
            base.Update(input.Id, input);
            return true;
        }
    }
}
