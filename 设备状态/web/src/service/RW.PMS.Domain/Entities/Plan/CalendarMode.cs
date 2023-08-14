using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.Plan
{
    /// <summary>
    /// 日历模式；
    /// 用于休假、计划排程
    /// </summary>
    public enum CalendarMode
    {
        /// <summary>
        /// 标准日历（双休）
        /// </summary>
        Normal = 0,
        /// <summary>
        /// 无休日历
        /// </summary>
        Round = 1,
        /// <summary>
        /// 单休日历
        /// </summary>
        Single = 2,
        /// <summary>
        /// 奇数周（大小周，小周休）
        /// </summary>
        OddWeek = 3,
        /// <summary>
        /// 偶数周（大小周，大周休）
        /// </summary>
        EvenWeek = 4,
        /// <summary>
        /// 自定义排程，需要设置日历
        /// </summary>
        Custom = 99,
    }
}
