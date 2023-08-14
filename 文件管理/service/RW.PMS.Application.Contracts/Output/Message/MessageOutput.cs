using System;

namespace RW.PMS.Application.Contracts.Output.Message
{
    public class MessageOutput
    {
        public int Id { get; set; }

        /// <summary>
        ///     消息标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     提醒时间
        /// </summary>
        public DateTime RemindTime { get; set; }

        /// <summary>
        ///     是否已读
        /// </summary>
        public bool IsRead { get; set; }
    }
}