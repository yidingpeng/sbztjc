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
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Project
{
    public class TaskPlanService : CrudApplicationService<TaskPlanEntity, int>, ITaskPlanService
    {
        public TaskPlanService(IDataValidatorProvider dataValidator,
          IRepository<TaskPlanEntity, int> repository, IMapper mapper, Lazy<ICurrentUser> currentUser) : base(
          dataValidator, repository, mapper, currentUser)
        {

        }
        /// <summary>
        /// 计划分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<TaskPlanView> GetPagedList(TaskPlanSearchDto input)
        {
            var list = Repository.Orm.Select<TaskPlanEntity, ProjectBasicsEntity, RoleEntity, TaskProcessInfoEntity>()
            .LeftJoin(a1 => a1.t1.projectID == a1.t2.Id)
            .LeftJoin(a2 => a2.t1.roleID == a2.t3.Id)
            .OrderByDescending(t => t.t1.CreatedAt)
            .Count(out var total).Page(input.PageNo, input.PageSize)
            .ToList((s, c, c2, c3) => new
            {
                sales = s,
                //areaGM = c.ContactsName,
                //areaCharger = c2.ContactsName,
                //areasalesman = c3.ContactsName,
            })
            .Select(t =>
            {
                var output = Mapper.Map<TaskPlanView>(t.sales);
                //output.AreaGMText = t.areaGM;
                //output.AreaChargerText = t.areaCharger;
                //output.AreaSalesmanText = t.areasalesman;
                return output;
            }).ToList();
            return new List<TaskPlanView>(list);
        }

        /// <summary>
        /// 项目模板分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PagedResult<projectTemplateView> ProTemplatePagedList(projectTemplateSearchDto input)
        {

            var list = Repository.Orm.Select<projectTemplateEntity>()
                .Where(a => a.IsDeleted == false)
                .OrderByDescending(a => a.Id)
                .Count(out var total)
                .Page(input.PageNo, input.PageSize)
                .ToList(t => new
                {
                    project = t
                }).Select(t =>
                {
                    var Output = Mapper.Map<projectTemplateView>(t.project);
                    return Output;
                }).ToList();
            return new PagedResult<projectTemplateView>(Mapper.Map<List<projectTemplateView>>(list), total);
        }

        /// <summary>
        /// WBS计划新增或修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool WBSInsertOrUpdate(WbsPlanDto input)
        {
            int maxSort = 1;
            if (input.Id == 0)
            {
                //获取最大排序号
                maxSort = Repository.Orm
                              .Select<WbsPlanEntity>()
                              .Where(x => x.TemplateId == input.TemplateId)
                              .Max(o => o.Sort);
                maxSort += 1;
            }
            var entity = Mapper.Map<WbsPlanEntity>(input);
            entity.CreatedAt = DateTime.Now;
            entity.LastModifiedAt = DateTime.Now;
            entity.Sort = maxSort > 0 ? maxSort : 1;
            var ins = Repository.Orm.InsertOrUpdate<WbsPlanEntity>()
                .SetSource(entity)
                .ExecuteAffrows();
            return ins > 0;
        }

        /// <summary>
        /// WBS计划新增或修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool WBSTabsInsertOrUpdate(WbsTabsDto input)
        {
            var entity = Mapper.Map<WbsTabsEntity>(input);
            entity.CreatedAt = DateTime.Now;
            entity.LastModifiedAt = DateTime.Now;
            var ins = Repository.Orm.InsertOrUpdate<WbsTabsEntity>()
                .SetSource(entity)
                .ExecuteAffrows();
            return ins > 0;
        }
        /// <summary>
        /// 配置WBS计划列表
        /// </summary>
        /// <param name="TempLateId"></param>
        /// <returns></returns>
        public List<WbsTabsDto> GetWBSTabsList(int TempLateId)
        {
            var list = Repository.Orm.Select<WbsTabsEntity>()
                .Where(o => o.TemplateId == TempLateId)
                .OrderBy(o => o.Id)
                .ToList(t => new
                {
                    temp = t
                }).Select(t =>
                {
                    var Output = Mapper.Map<WbsTabsDto>(t.temp);
                    var wbsp = Repository.Orm.Select<WbsPlanEntity>()
                                             .Where(x => x.TemplateId == t.temp.Id)
                                             .OrderBy(o=>o.Sort)
                                             .ToList()
                                             .Select(x => Mapper.Map<WbsPlanDto>(x)).ToList();
                    Output.WBSPlan = wbsp;
                    return Output;
                }).ToList();
            return list;
        }
        /// <summary>
        /// WBS计划信息列表
        /// </summary>
        /// <param name="TemplateId"></param>
        /// <returns></returns>
        public List<WbsPlanDto> GetWBSPlanList(int TemplateId)
        {
            var list = Repository.Orm
                       .Select<WbsPlanEntity>()
                       .Where(o => o.TemplateId == TemplateId)
                       .ToList().Select(x => Mapper.Map<WbsPlanDto>(x))
                       .ToList();
            return list;
        }

        /// <summary>
        /// WBS计划移动
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Template"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public bool WBSMove(int Id,int TemplateId,string Type)
        {
            bool result = false;
            var list = Repository.Orm.Select<WbsPlanEntity>().Where(o => o.TemplateId == TemplateId).OrderBy(o => o.Sort).ToList();
            if (list.Any())
            {
                //当前操作的数据
                var current = list.Where(o => o.Id == Id).FirstOrDefault();
                if (current != null)
                {
                    if (Type == "up")
                    {
                        //当前上一排序
                        var first = list.Where(o => o.Sort < current.Sort).LastOrDefault();
                        if (first != null)
                        {
                            int f1 = Repository.Orm.Update<WbsPlanEntity>(Id).Set(a => a.Sort, first.Sort).ExecuteAffrows();
                            int f2 = Repository.Orm.Update<WbsPlanEntity>(first.Id).Set(a => a.Sort, current.Sort).ExecuteAffrows();
                            result = f1 > 0 && f2 > 0;
                        }
                    }
                    else if (Type == "down")
                    {
                        //当前下一排序
                        var last = list.Where(o => o.Sort > current.Sort).FirstOrDefault();
                        if (last != null)
                        {
                            int l1 = Repository.Orm.Update<WbsPlanEntity>(Id).Set(a => a.Sort, last.Sort).ExecuteAffrows();
                            int l2 = Repository.Orm.Update<WbsPlanEntity>(last.Id).Set(a => a.Sort, current.Sort).ExecuteAffrows();
                            result = l1 > 0 && l2 > 0;
                        }
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 逻辑删除计划名称
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool WBSTabsDelete(int Id)
        {
            var first = Repository.Orm.Select<WbsTabsEntity>()
                .Where(o => o.Id == Id).First();
            int min = GetWBSTabsList(first.TemplateId).Min(o => o.Id.Value);
            if (Id > min)
            {
                int count = Repository.Orm.Update<WbsTabsEntity>(Id).Set(a => a.IsDeleted, true).ExecuteAffrows();
                return count > 0;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 逻辑删除计划信息列表
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool WBSPlanDelete(int Id)
        {
            int count = Repository.Orm.Update<WbsPlanEntity>(Id).Set(a => a.IsDeleted, true).ExecuteAffrows();
            return count > 0;
        }
    }
}
