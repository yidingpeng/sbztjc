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
    public class ProTemplateService : CrudApplicationService<projectTemplateEntity, int>, IProTemplateService
    {
        public ProTemplateService(IDataValidatorProvider dataValidator,
          IRepository<projectTemplateEntity, int> repository, IMapper mapper, Lazy<ICurrentUser> currentUser) : base(
          dataValidator, repository, mapper, currentUser)
        {

        }
        /// <summary>
        /// 项目模板分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PagedResult<projectTemplateView> ProTemplatePagedList(projectTemplateSearchDto input)
        {
            var list = Repository.Select.From<DictionaryEntity>((a, b) => a
                .LeftJoin(a1 => a1.ProjectType == b.Id))
                .WhereIf(input.Name.NotNullOrWhiteSpace(), (a, b) => a.Name.Contains(input.Name))
                .OrderByDescending((a, b) => a.Id)
                .Where((a, b) => a.IsDeleted == false)
                .Count(out var total)
                .Page(input.PageNo, input.PageSize)
                .ToList((a, b) => new
                {
                    template = a,
                    ProjectTypeName = b.DictionaryText,
                }).Select(t =>
                {
                    var Output = Mapper.Map<projectTemplateView>(t.template);
                    Output.ProjectTypeName = t.ProjectTypeName;
                    return Output;
                }).ToList();
            return new PagedResult<projectTemplateView>(Mapper.Map<List<projectTemplateView>>(list), total);
        }
        /// <summary>
        /// 模板信息新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //public bool Insert(projectTemplateView input)
        //{
        //    var entity = Mapper.Map<projectTemplateEntity>(input);

        //    base.Insert(entity);
        //    return true;
        //}

        /// <summary>
        /// 模板信息修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //public bool Update(projectTemplateView input)
        //{
        //    base.Update(input.Id, input);
        //    return true;
        //}
        /// <summary>
        /// 模板名唯一
        /// </summary>
        /// <param name="ProjectCode"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool ProOnly(string Name, int Id)
        {
            var list = Repository.Where(t => t.Name.Equals(Name) && t.Id != Id).ToOne();
            return list != null;
        }
        /// <summary>
        /// 项目模板修改状态
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool updateState(int Id)
        {
            var ent = Repository.Where(t => t.Id == Id).ToOne();
            if (ent != null)
            {
                bool par = ent.State == true ? false : true;
                Repository.Orm.Update<projectTemplateEntity>(Id).Set(a => a.State, par).ExecuteAffrows();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
