using System.ComponentModel.DataAnnotations;
using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using RW.PMS.Domain.ValueObject;

namespace RW.PMS.Domain.Entities.Message
{
    /// <summary>
    ///     消息配置
    /// </summary>
    [Table(Name = "msg_Config")]
    public class MessageConfigEntity : FullEntity
    {
        /// <summary>
        ///     消息编码
        /// </summary>
        public string MessageCode { get; set; }

        /// <summary>
        ///     消息类型
        /// </summary>
        [Column(MapType = typeof(int))]
        public MessageType MessageType { get; set; }

        /// <summary>
        ///     消息标题
        /// </summary>
        [MaxLength(200)]
        public string Title { get; set; }

        /// <summary>
        ///     消息级别（关联字典表-MessageLevel）
        /// </summary>
        public int MessageLevel { get; set; }

        /// <summary>
        ///     提醒间隔
        /// </summary>
        public int? RepeatInterval { get; set; }

        /// <summary>
        ///     提醒间隔单位
        /// </summary>
        [Column(MapType = typeof(int), IsNullable = true)]
        public TimeType? IntervalUnit { get; set; }

        /// <summary>
        ///     提前预警时间
        /// </summary>
        public int? AdvanceTime { get; set; }

        /// <summary>
        ///     提前预警时间单位
        /// </summary>
        [Column(MapType = typeof(int), IsNullable = true)]
        public TimeType? AdvanceUnit { get; set; }

        /// <summary>
        ///     重复次数
        /// </summary>
        public int? RepeatTimes { get; set; }

        /// <summary>
        ///     模板内容
        /// </summary>
        [MaxLength(-1)]
        public string Content { get; set; }

        /// <summary>
        ///     消息目标类型
        /// </summary>
        [Column(MapType = typeof(int))]
        public MessageTarget MessageTarget { get; set; }

        /// <summary>
        ///     目标Id
        /// </summary>
        [MaxLength(5000)]
        public string Target { get; set; }

        /// <summary>
        ///     提醒方式
        /// </summary>
        [Column(MapType = typeof(int))]
        public MessageRemindWay RemindWay { get; set; }

        /// <summary>
        ///     备注
        /// </summary>
        [MaxLength(500)]
        public string Remark { get; set; }

    }
}