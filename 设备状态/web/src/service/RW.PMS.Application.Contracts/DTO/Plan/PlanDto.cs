using RW.PMS.Domain.Entities.Plan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Plan
{
    /// <summary>
    /// 计划名称信息
    /// </summary>
    public class PlanDto
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 项目编码
        /// </summary>
        public string ProjectCode { get; set; }

        /// <summary>
        /// 计划名
        /// </summary>
        public string PlanName { get; set; }

        /// <summary>
        ///责任人
        /// </summary>
        public string PersonLiable { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderIndex { get; set; }

        /// <summary>
        /// 是否主计划
        /// </summary>
        public int IsMainPlan { get; set; }

        /// <summary>
        /// 计划开始日期
        /// </summary>
        public DateTime PlanDate { get; set; }

        /// <summary>
        /// 日历模式
        /// </summary>
        public CalendarMode CalendarMode { get; set; }

        /// <summary>
        /// 是否自动排程
        /// </summary>
        public bool IsAutoAps { get; set; }

        /// <summary>
        /// 计划状态：变更中、已发布
        /// </summary>
        public string Status { get; set; }


    }
}
