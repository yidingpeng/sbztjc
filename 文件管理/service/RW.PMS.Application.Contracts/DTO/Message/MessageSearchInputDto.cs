using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Domain.ValueObject;
using System;

namespace RW.PMS.Application.Contracts.DTO.Message;

/// <summary>
///     消息内容搜索实体
/// </summary>
public class MessageSearchInputDto : PagedQuery
{
    public string MessageType { get; set; }

    public bool? Read { get; set; }

    public string Title { get; set; }

    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }
}
