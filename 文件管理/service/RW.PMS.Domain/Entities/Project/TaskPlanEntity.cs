using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.Project
{
    /// <summary>
    /// 计划主表
    /// </summary>
    [Table(Name = "TaskPlan")]
    public class TaskPlanEntity : FullEntity
    {
        /// <summary>
        /// 计划单据号
        /// </summary>
        public string planReceiptNo { get; set; }
        /// <summary>
        /// 项目ID
        /// </summary>
        public int projectID { get; set; }
        /// <summary>
        /// 任务模式
        /// </summary>
        public int taskMode{get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        public int roleID { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>
        public int reviewer { get; set; }
        /// <summary>
        /// 责任人 
        /// </summary>
        public int responsible { get; set; }
        /// <summary>
        /// 计划开始
        /// </summary>
        public DateTime planStart { get; set; }
        /// <summary>
        /// 计划结束
        /// </summary>
        public DateTime planEnd { get; set; }
        /// <summary>
        /// 计划工期
        /// </summary>
        public int planDuration { get; set; }

        /// <summary>
        /// 实际开始
        /// </summary>
        public DateTime actualStart { get; set; }
        /// <summary>
        /// 实际结束
        /// </summary>
        public DateTime actualEnd { get; set; }
        /// <summary>
        /// 前置
        /// </summary>
        public int frontTask { get; set; }
        /// <summary>
        /// 后置
        /// </summary>
        public int postpositionTask { get; set; }
        /// <summary>
        /// 进度
        /// </summary>
        public int schedule { get; set; }
        /// <summary>
        /// 任务状态
        /// </summary>
        public int taskState { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(1000)]
        public string remark { get; set; }

    }
}
