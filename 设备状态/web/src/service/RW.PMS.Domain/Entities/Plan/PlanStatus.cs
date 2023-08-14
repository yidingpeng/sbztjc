using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.Plan
{
    /// <summary>
    /// 计划执行状态
    /// </summary>
    public enum PlanStatus
    {
        /// <summary>
        /// 变更中
        /// </summary>
        Changging,
        /// <summary>
        /// 已发布
        /// </summary>
        Published,
        /// <summary>
        /// 已废弃
        /// </summary>
        Destroied,
    }
}
