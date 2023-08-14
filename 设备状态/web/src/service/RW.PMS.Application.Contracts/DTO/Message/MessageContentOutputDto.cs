using RW.PMS.Domain.Entities.Message;
using System;

namespace RW.PMS.Application.Contracts.DTO.Message;

/// <summary>
///     消息内容输出实体
/// </summary>
public class MessageContentOutputDto
{
    /// <summary>
    ///     主键
    /// </summary>
    public int Id { get; set; }

    public string Type { get; set; }


    /// <summary>
    ///     消息标题
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    ///     Content
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// 消息时间
    /// </summary>
    public string Time { get; set; }

    public bool Read { get; set; }

    public MessageContentOutputDto SetRead(MessageReceiverEntity t3)
    {
        if (t3 != null)
            this.Read = t3.IsRead;
        return this;
    }
}
