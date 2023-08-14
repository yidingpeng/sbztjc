using Microsoft.Extensions.Logging;
using RW.PMS.CrossCutting.EventBus.Handlers;

namespace RW.PMS.Application.Event.EventHandlers
{
    public class EmailMessageHandler : IEventHandler<MessageEventData>
    {
        private readonly ILogger<EmailMessageHandler> _logger;

        public EmailMessageHandler(ILogger<EmailMessageHandler> logger)
        {
            _logger = logger;
        }

        public void HandleEvent(MessageEventData eventData)
        {
            //_logger.LogInformation(
            //    $"Handler:EmailMessageHandler;ToUser:{eventData.ToUser};Message:{eventData.Message}");
        }
    }
}