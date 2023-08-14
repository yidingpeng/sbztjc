using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Project;
using RW.PMS.Domain.Entities.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.Project
{
    public interface ITaskPlanService:ICrudApplicationService<TaskPlanEntity, int>
    {
        /// <summary>
        /// 项目模板分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        PagedResult<projectTemplateView> ProTemplatePagedList(projectTemplateSearchDto input);

        /// <summary>
        /// WBS计划新增或修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        bool WBSInsertOrUpdate(WbsPlanDto input);

        /// <summary>
        /// WBSTabs计划新增或修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        bool WBSTabsInsertOrUpdate(WbsTabsDto input);

        /// <summary>
        /// 配置WBS计划列表
        /// </summary>
        /// <param name="TempLateId"></param>
        /// <returns></returns>
        List<WbsTabsDto> GetWBSTabsList(int TempLateId);

        /// <summary>
        /// WBS计划信息列表
        /// </summary>
        /// <param name="TemplateId"></param>
        /// <returns></returns>
        List<WbsPlanDto> GetWBSPlanList(int TemplateId);

        /// <summary>
        /// WBS计划移动
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Template"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        bool WBSMove(int Id, int TemplateId, string Type);

        /// <summary>
        /// 逻辑删除计划名称
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        bool WBSTabsDelete(int Id);

        /// <summary>
        /// 逻辑删除计划信息列表
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        bool WBSPlanDelete(int Id);
    }
}
