using FreeSql.DataAnnotations;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.Plan
{
    /// <summary>
    /// 计划的行为日志，任何操作都应该被记录在此信息表中
    /// </summary>
    [Table(Name = "pro_plan_logs")]
    public class PlanLogEntity : FullEntity<int>
    {
        /// <summary>
        /// 计划ID
        /// </summary>
        public int PlanId { get; set; }
        /// <summary>
        /// 计划名称
        /// </summary>
        public string PlanName { get; set; }
        /// <summary>
        /// 任务Id
        /// </summary>
        public Guid TaskId { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName { get; set; }
        /// <summary>
        /// 计划动作类型
        /// </summary>
        public PlanActionTypes LogAction { get; set; }
        /// <summary>
        /// 计划动作对象
        /// </summary>
        public PlanActionSources LogActionSource { get; set; }

        /// <summary>
        /// 操作内容
        /// 如：
        /// 【管理员】 导入了【计划模板】的【计划】，共【5】个任务。
        /// 【管理员】将任务【任务名称】移动到索引【1】
        /// 【管理员】添加了任务【任务名称】
        /// 
        /// 后期考虑加入链接快速链接到相关信息，前期只考虑文字日志。
        /// </summary>
        public string Content { get; set; }

        public void AddTask(ICurrentUser user)
        {
            LogAction = PlanActionTypes.Add;
            LogActionSource = PlanActionSources.Task;
            Content = $"【{user.RealName}】添加了任务【{TaskName}】。";
        }

        public void ClearTask(ICurrentUser user, int num)
        {
            LogAction = PlanActionTypes.Add;
            LogActionSource = PlanActionSources.Task;
            Content = $"【{user.RealName}】清空了计划【{PlanName}】的任务，共【{num}】条。";
        }

        public void SetPlan(PlanEntity plan)
        {
            this.PlanId = plan.Id;
            this.PlanName = plan.PlanName;
        }

        public void SetTask(TaskEntity task)
        {
            this.TaskId = task.Id;
            this.TaskName = task.Name;
        }

        public void AddPlan(ICurrentUser user)
        {
            LogAction = PlanActionTypes.Add;
            LogActionSource = PlanActionSources.Plan;
            Content = $"【{user.RealName}】添加了计划【{PlanName}】。";

        }

        public void SetContent(string content)
        {
            Content = content;
        }

        public void DeletePlans(ICurrentUser user, int[] ids, string[] names)
        {
            LogAction = PlanActionTypes.Delete;
            LogActionSource = PlanActionSources.Plan;
            Content = $"【{user.RealName}】删除了指定计划，分别为【{names.Aggregate((a, b) => a + "," + b)}】。";
        }

        public void DeleteTask(ICurrentUser user)
        {
            LogAction = PlanActionTypes.Delete;
            LogActionSource = PlanActionSources.Task;
            Content = $"【{user.RealName}】删除了计划【{PlanName}】的任务【{TaskName}】,ID=【{TaskId}】。";
        }

        public void MoveTask(ICurrentUser user, TaskEntity destEntity)
        {
            LogAction = PlanActionTypes.Update;
            LogActionSource = PlanActionSources.Task;
            Content = $"【{user.RealName}】移动了计划【{PlanName}】的任务1【{TaskName}】,ID=【{TaskId}】和任务2【{destEntity.Name}】,ID=【{destEntity.Id}】。";

        }

        public void UpdateTask(ICurrentUser user, string filedName, object oldValue, object newValue)
        {
            LogAction = PlanActionTypes.Update;
            LogActionSource = PlanActionSources.Task;
            Content = $"【{user.RealName}】修改了计划【{PlanName}】任务【{TaskName}】的字段【{filedName}】，新值【{newValue}】,原始值【{oldValue}】。";
        }
    }
}
