using RW.PMS.Application.Contracts.DTO.Plan;
using RW.PMS.Domain.Entities.Plan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.Plan
{
    public interface IPlanService : ICrudApplicationService<PlanEntity, int>
    {

        ///// <summary>
        ///// 根据项目编码查询计划名称列表
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //List<TaskDto> GetByCodeList(string ProjectCode);

        /// <summary>
        /// 根据项目编码查询计划名称列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<PlanDto> GetPlans(string projectCode);
        int DeletePlan(int[] ids);
        /// <summary>
        /// 新增计划
        /// </summary>
        PlanEntity AddPlan(PlanDto input);
        /// <summary>
        /// 修改计划
        /// </summary>
        int UpdatePlan(int id, PlanDto input);
        /// <summary>
        /// 根据计划获取任务列表
        /// </summary>
        /// <param name="planId">计划ID</param>
        /// <returns></returns>
        List<TaskDto> GetTasks(int planId);

        /// <summary>
        /// 将一个任务与另一个任务的位置互换
        /// </summary>
        int MoveTask(MoveTaskDto move);
        /// <summary>
        /// 导入模板，可指定计划或任务，未指定表示全部
        /// </summary>
        /// <param name="templateId"></param>
        /// <param name="planId"></param>
        /// <param name="taskId"></param>
        /// <returns></returns>
        int ImportPlanTask(ImportDto import);
        int AddTask(AddTaskDto task);
        int DeleteTask(DeleteTaskDto taskId);
        /// <summary>
        /// 修改任务属性
        /// </summary>
        int EditTaskFiled(EditTaskFiledDto edit);
        int ClearTask(ClearTaskDto clear);
    }
}
