using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.Plan
{
    /// <summary>
    /// 操作类型
    /// </summary>
    public enum PlanActionTypes
    {
        Unknow,
        Add,
        Delete,
        Update,
        UpdateFiled,
        Clear,
    }

    /// <summary>
    /// 操作对象
    /// </summary>
    public enum PlanActionSources
    {
        Unknow,
        Plan,
        Task,
        Document,
    }
}
