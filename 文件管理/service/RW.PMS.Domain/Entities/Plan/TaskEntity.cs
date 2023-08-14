using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.Plan
{
    /// <summary>
    /// 项目计划
    /// </summary>
    [Table(Name = "pro_plan_tasks")]
    public class TaskEntity : FullEntity<Guid>, IOrderable
    {
        /// <summary>
        /// 计划ID
        /// </summary>
        public int PlanId { get; set; }
        /// <summary>
        /// WBS版本号
        /// </summary>
        public string WBSVersion { get; set; }

        /// <summary>
        /// 是否里程碑
        /// </summary>
        public bool IsMilestone { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// 责任人
        /// </summary>
        public string Owner { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public string Auditor { get; set; }

        /// <summary>
        /// 项目编码
        /// </summary>
        public string ProjectCode { get; set; }

        /// <summary>
        /// 计划名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 计划类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 是否主计划
        /// </summary>
        public string IsMainPlan { get; set; }

        private decimal duration = 0;
        /// <summary>
        /// 工期(计划工期)
        /// </summary>
        public decimal Duration
        {
            get => duration;
            set
            {
                var before = duration;
                duration = value;
                OffsetDuration?.Invoke((int)(duration - before));
                PlanEndDate = PlanStartDate.AddDays((int)duration);
            }
        }

        /// <summary>
        /// 实际工期
        /// </summary>
        public decimal ActualDuration { get; set; }

        /// <summary>
        /// 计划工时
        /// </summary>
        public int PlanManHour { get; set; }

        /// <summary>
        /// 实际工时
        /// </summary>
        public int ActualManHour { get; set; }

        /// <summary>
        /// 标准工时
        /// </summary>
        public decimal WorkingHours { get; set; }

        /// <summary>
        /// 进度
        /// </summary>

        public int Progress { get; set; }

        /// <summary>
        /// 计划开始时间
        /// </summary>
        public DateTime PlanStartDate { get; set; }

        /// <summary>
        /// 计划结束时间
        /// </summary>
        public DateTime? PlanEndDate { get; set; }

        /// <summary>
        /// 实际开始时间
        /// </summary>
        public DateTime? ActualStart { get; set; }

        /// <summary>
        /// 实际结束时间
        /// </summary>
        public DateTime? ActualFinish { get; set; }

        /// <summary>
        /// 基线开始
        /// </summary>
        public string BaselineStart { get; set; }

        /// <summary>
        /// 基线结束
        /// </summary>
        public string BaselineFinish { get; set; }

        /// <summary>
        /// 浮动时间
        /// </summary>
        public int FluctuateTime { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int PlanState { get; set; }

        /// <summary>
        /// 任务分配的用户ID
        /// </summary>
        public int AssigneeIds { get; set; }

        /// <summary>
        ///父级ID
        /// </summary>
        public Guid ParentId { get; set; }

        /// <summary>
        /// 修改属性名
        /// </summary>
        /// <param name="filedName">字段名</param>
        /// <param name="value">值 </param>
        public object UpdateColumn(string filedName, object value)
        {
            var type = this.GetType();
            BindingFlags flag = BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance;
            var props = type.GetProperty(filedName, flag);
            if (props == null)
                throw new NullReferenceException($"属性未找到。属性名：${filedName}");
            var newValue = Convert.ChangeType(value, props.PropertyType);
            var oldValue = props.GetValue(this);

            props.SetValue(this, newValue);
            return oldValue;
        }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderIndex { get; set; }

        public event Action<int> OffsetDuration;
    }
}
