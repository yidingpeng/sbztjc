using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RW.PMS.Domain.ValueObject;

namespace RW.PMS.Application.Contracts.Input.Message
{
    /// <summary>
    ///     消息配置表单实体
    /// </summary>
    public class MessageConfigInput
    {
        /// <summary>
        ///     主键
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        ///     消息编码
        /// </summary>
        public string MessageCode { get; set; }

        /// <summary>
        ///     消息类型
        /// </summary>
        public MessageType MessageType { get; set; }

        /// <summary>
        ///     消息标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     消息级别
        /// </summary>
        public int MessageLevel { get; set; }

        /// <summary>
        ///     提醒间隔
        /// </summary>
        public int? RepeatInterval { get; set; }

        /// <summary>
        ///     提醒间隔单位
        /// </summary>
        public TimeType? IntervalUnit { get; set; }

        /// <summary>
        ///     提前预警时间
        /// </summary>
        public int? AdvanceTime { get; set; }

        /// <summary>
        ///     提前预警时间单位
        /// </summary>
        public TimeType? AdvanceUnit { get; set; }

        /// <summary>
        ///     重复次数
        /// </summary>
        public int? RepeatTimes { get; set; }

        /// <summary>
        ///     模板内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        ///     消息目标
        /// </summary>
        public MessageTarget MessageTarget { get; set; }

        /// <summary>
        ///		目标Id
        /// </summary>
        public string Target { get; set; }

        /// <summary>
		///     提醒方式
		/// </summary>
		public MessageRemindWay RemindWay { get; set; }

        /// <summary>
        ///     备注
        /// </summary>
        public string Remark { get; set; }
    }
}
