using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Message;
using RW.PMS.Domain.Entities.Message;
using System;
using System.Collections.Generic;

namespace RW.PMS.Application.Contracts.Message;

/// <summary>
///     消息内容应用服务
/// </summary>
public interface IMessageContentService : ICrudApplicationService<MessageContentEntity, int>
{
    /// <summary>
    ///     获取消息内容分页数据
    /// </summary>
    /// <param name="input"><see cref="MessageSearchInputDto" />搜索输入项</param>
    /// <returns></returns>
    PagedResult<MessageContentOutputDto> GetPagedList(MessageSearchInputDto input);

    /// <summary>
    /// 获取最新消息
    /// </summary>
    /// <param name="top">查询数量</param>
    List<MessageContentOutputDto> GetNewMessage(int top);

    /// <summary>
    /// 发送消息
    /// </summary>
    bool SendMessage(MessageSendInputDto input);

    /// <summary>
    /// 阅读消息，将消息标记为已读
    /// </summary>
    MessageDetailOutputDto ReadMessage(int id);

    /// <summary>
    /// 删除自己的指定消息
    /// </summary>
    bool DeleteMessage(int id);

    /// <summary>
    /// 清空自己的消息
    /// </summary>
    bool ClearMessage();

    /// <summary>
    /// 清空大于某个时间段的消息记录
    /// 通常由定时任务处理，清除大于[60]天的消息记录，防止消息数过多问题
    /// </summary>
    int CleanMessageByDate(TimeSpan old);

    List<MessageTopOutput> GetUnReadMessage(int count);
}
