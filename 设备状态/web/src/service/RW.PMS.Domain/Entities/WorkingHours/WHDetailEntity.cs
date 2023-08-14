using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.WorkingHours
{
    /// <summary>
    /// 工时明细
    /// </summary>
    [Table(Name = "wh_detail")]
    public class WHDetailEntity: FullEntity
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 填报日期
        /// </summary>
        public DateOnly WHDate { get; set; }

        /// <summary>
        /// 项目编号
        /// </summary>
        public string ProjectCode { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 工作内容
        /// </summary>
        public string JobContent { get; set; }

        /// <summary>
        /// 任务内容
        /// </summary>
        public string TaskContent { get; set; }

        /// <summary>
        /// 工作地点
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// 厂外地点
        /// </summary>
        public string OffsiteLocation { get; set; }

        /// <summary>
        /// 工作开始时间
        /// </summary>
        public TimeOnly JobStartTime { get; set; }

        /// <summary>
        /// 工作结束时间
        /// </summary>
        public TimeOnly JobEndTime { get; set; }

        /// <summary>
        /// 完成状态 0已完成 1未完成
        /// </summary>
        public string CompleteStatus { get; set; }

        /// <summary>
        /// 工作时长
        /// </summary>
        public decimal Duration { get; set; }

        /// <summary>
        /// 加班时长
        /// </summary>
        public decimal WorkOvertimeDuration { get; set; }
    }
}
