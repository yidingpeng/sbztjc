using System;
using System.ComponentModel.DataAnnotations;
using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using RW.PMS.Domain.ValueObject;

namespace RW.PMS.Domain.Entities.System
{
    /// <summary>
    ///     系统报警
    /// </summary>
    [Table(Name = "sys_alarm", OldName = "SystemAlarm")]
    public class SystemAlarmEntity : Entity<long>
    {
        /// <summary>
        ///     报警内容
        /// </summary>
        [MaxLength(500)]
        public string AlarmContent { get; set; }

        /// <summary>
        ///     报警来源
        /// </summary>
        [Column(MapType = typeof(int))]
        public AlarmSource AlarmSource { get; set; }

        /// <summary>
        ///     报警时间
        /// </summary>
        public DateTime AlarmTime { get; set; }
    }
}