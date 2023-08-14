using System;
using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;

namespace RW.PMS.Domain.Entities.Message
{
    /// <summary>
    ///     消息接收者实体
    /// </summary>
    [Table(Name = "msg_Receiver")]
    public class MessageReceiverEntity : IEDRoomJiaoZhunTimeEntityntity<int>
    {
        /// <summary>
        ///     消息内容Id
        /// </summary>
        public int ContentId { get; set; }

        /// <summary>
        ///     目标用户Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        ///     提醒时间
        /// </summary>
        public DateTime RemindTime { get; set; }

        /// <summary>
        ///     是否已读
        /// </summary>
        public bool IsRead { get; set; }

        /// <summary>
        ///     阅读时间
        /// </summary>
        public DateTime? ReadTime { get; set; }
    }
}