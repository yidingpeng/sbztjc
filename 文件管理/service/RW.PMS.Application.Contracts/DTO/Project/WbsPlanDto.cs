using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Project
{
    public class WbsPlanDto
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// 模板ID
        /// </summary>
        public int TemplateId { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 任务
        /// </summary>
        public string WbsName { get; set; }

        /// <summary>
        /// 里程碑（0否 1是）
        /// </summary>
        public int Milestone { get; set; }

        /// <summary>
        /// 工期
        /// </summary>
        public decimal Duration { get; set; }

        /// <summary>
        /// 标准工时
        /// </summary>
        public decimal WorkingHours { get; set; }

        /// <summary>
        /// 关联阶段
        /// </summary>
        public string CorrelationStage { get; set; }

        /// <summary>
        /// 责任角色
        /// </summary>
        public string DutyRole { get; set; }

        /// <summary>
        /// 任务类型
        /// </summary>
        public string TaskType { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public string Auditor { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int State { get; set; }
    }
}
