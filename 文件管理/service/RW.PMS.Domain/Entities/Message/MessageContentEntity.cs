using System.ComponentModel.DataAnnotations;
using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;

namespace RW.PMS.Domain.Entities.Message
{
    /// <summary>
    ///     消息内容实体
    /// </summary>
    [Table(Name = "msg_Content")]
    public class MessageContentEntity : FullEntity
    {
        /// <summary>
        ///     消息配置Id
        /// </summary>
        public int ConfigId { get; set; }

        /// <summary>
        /// 关联的数据ID
        /// 此ID与Type关联
        /// </summary>
        public int? DataId { get; set; }

        /// <summary>
        ///     消息级别
        /// </summary>
        public int MessageLevel { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        ///     消息标题
        /// </summary>
        [MaxLength(200)]
        public string Title { get; set; }

        /// <summary>
        ///     消息内容
        /// </summary>
        [MaxLength(-1)]
        public string Content { get; set; }
    }
}