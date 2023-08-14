using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.Plan
{
    /// <summary>
    /// 项目名信息
    /// </summary>
    [Table(Name = "pro_plans")]
    public class PlanEntity : FullEntity, IOrderable
    {

        /// <summary>
        /// 项目编码
        /// </summary>
        public string ProjectCode { get; set; }
        /// <summary>
        /// 计划名
        /// </summary>
        public string PlanName { get; set; }

        /// <summary>
        /// 计划的版本号
        /// </summary>
        public int VersionNum { get; set; }

        /// <summary>
        ///责任人
        /// </summary>
        public string PersonLiable { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        [Column(OldName = "Order")]
        public int OrderIndex { get; set; }

        /// <summary>
        /// 是否主计划
        /// </summary>
        public int IsMainPlan { get; set; }

        /// <summary>
        /// 计划日期
        /// </summary>
        public DateTime PlanDate { get; set; } = DateTime.Now;

        /// <summary>
        /// 日历模式
        /// </summary>
        public CalendarMode CalendarMode { get; set; }

        /// <summary>
        /// 是否自动排程
        /// </summary>
        public bool IsAutoAps { get; set; } = true;

        /// <summary>
        /// 计划状态
        /// </summary>
        public PlanStatus PlanStatus { get; set; }

        public string StatusText()
        {
            switch (PlanStatus)
            {
                case PlanStatus.Changging: return "变更中";
                case PlanStatus.Published: return "已发布";
                case PlanStatus.Destroied: return "已废弃";
                default: return "未知状态";
            }
        }

        ///// <summary>
        ///// 工作开始时间，如8:00
        ///// </summary>
        //public TimeSpan TimeStart { get; set; } = new TimeSpan(9, 0, 0);
        ///// <summary>
        ///// 工作结束时间，如17:00
        ///// </summary>
        //public TimeSpan TimeEnd { get; set; } = new TimeSpan(17, 0, 0);

        /// <summary>
        /// 下一个工作日
        /// </summary>
        /// <returns>返回日期</returns>
        public DateTime NextWorkDay(DateTime date)
        {
            DateTime next = date.AddDays(1);
            switch (CalendarMode)
            {
                case CalendarMode.Normal:
                    while (next.DayOfWeek == DayOfWeek.Saturday || next.DayOfWeek == DayOfWeek.Sunday)
                        next = next.AddDays(1);
                    break;
                case CalendarMode.Single:
                    while (next.DayOfWeek == DayOfWeek.Saturday)
                        next = next.AddDays(1);
                    break;
                case CalendarMode.OddWeek:
                    if (next.DayOfWeek == DayOfWeek.Saturday && IsOddWeek(next))
                        next = next.AddDays(2);
                    break;
                case CalendarMode.EvenWeek:
                    //如果是小周，周六不休息
                    if (next.DayOfWeek == DayOfWeek.Saturday && !IsOddWeek(next))
                        next = next.AddDays(2);
                    break;
                case CalendarMode.Custom:
                    throw new NotImplementedException("暂不支持自定义排程");
                case CalendarMode.Round:
                default:
                    //无休模式，下一个工作日则是next日期
                    break;
            }
            return next;
        }

        public DateTime WorkEnd(DateTime date, int duration)
        {
            if (duration != 0)
                return date.AddDays(duration - 1);
            else
                return date;
        }

        /// <summary>
        /// 日期是否是奇数周
        /// </summary>
        bool IsOddWeek(DateTime time)
        {
            var firstDay = new DateTime(time.Year, 1, 1);//第一天日期
            var weekend = firstDay.AddDays((int)firstDay.DayOfWeek);//第一周周末日期
            //第一周为奇数周
            if (time.DayOfYear <= weekend.DayOfYear)
                return true;
            var weekNum = (time.DayOfYear - weekend.DayOfYear) / 7 + 2;//当前周数
            return weekNum % 2 == 1;
        }
    }
}
