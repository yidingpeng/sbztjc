using RW.PMS.Domain.ValueObject;

namespace RW.PMS.Application.Contracts.Output.Message
{
    public class MessageContentView
    {
        public int Id { get; set; }

        /// <summary>
        ///     消息类型
        /// </summary>
        public MessageType MessageType { get; set; }

        /// <summary>
        ///     提醒方式
        /// </summary>
        public MessageRemindWay RemindWay { get; set; }

        /// <summary>
        ///     消息级别
        /// </summary>
        public string DictionaryText { get; set; }

        /// <summary>
        ///     消息标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     消息内容
        /// </summary>
        public string Content { get; set; }
    }
}