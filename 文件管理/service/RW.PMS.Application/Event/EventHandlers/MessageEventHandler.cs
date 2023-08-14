using Microsoft.Extensions.Logging;
using RW.PMS.Application.Contracts.DTO.Message;
using RW.PMS.Application.Contracts.Message;
using RW.PMS.Application.Contracts.System;
using RW.PMS.CrossCutting.EventBus;
using RW.PMS.CrossCutting.EventBus.Handlers;
using RW.PMS.CrossCutting.Extender;
using System.Linq;

namespace RW.PMS.Application.Event.EventHandlers
{
    /// <summary>
    /// 消息事件处理
    /// 可将消息发送到站内、邮件、app等等
    /// </summary>
    public class MessageEventHandler : IEventHandler<MessageEventData>
    {
        IMessageContentService msgService;
        IEventBus eventBus;
        IUserService userService;
        public MessageEventHandler(
            IMessageContentService msgService,
            IEventBus eventBus,
            IUserService userService)
        {
            this.msgService = msgService;
            this.eventBus = eventBus;
            this.userService = userService;
        }

        public void HandleEvent(MessageEventData eventData)
        {
            msgService.SendMessage(new MessageSendInputDto
            {
                Title = eventData.Title,
                Content = eventData.Content,
                DataId = eventData.DataId,
                Type = eventData.Type,
                UserIds = eventData.UserIds,
            });

            var infos = userService.GetUserListByIds(eventData.UserIds);
            var names = infos.Select(x => x.Fullname).ArrayToString();

            eventBus.Trigger(new LogEventData
            {
                Type = Contracts.DTO.Log.LogTypeEnum.Operation,
                Message = "发送消息成功",
                Result = true,
                Desc = $"发送 {eventData.Title} 消息给 [{names}]",
            });

        }
    }
}