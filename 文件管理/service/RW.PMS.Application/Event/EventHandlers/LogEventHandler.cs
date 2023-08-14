using RW.PMS.Application.Contracts.System;
using RW.PMS.CrossCutting.EventBus.Handlers;
using RW.PMS.CrossCutting.Security.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Event.EventHandlers
{
    public class LogEventHandler : IEventHandler<LogEventData>
    {
        readonly Lazy<ICurrentUser> currentUser;
        readonly ILogService _log;
        public LogEventHandler(Lazy<ICurrentUser> currentUser, ILogService log)
        {
            this.currentUser = currentUser;
            this._log = log;
        }

        public void HandleEvent(LogEventData eventData)
        {
            _log.AddLog(new Contracts.DTO.Log.LogInputDto
            {
                Type = eventData.Type,
                Result = eventData.Result,
                Desc = eventData.Desc.Replace("#user#", currentUser.Value.RealName),
                Message = eventData.Message.Replace("#user#", currentUser.Value.RealName),
            });
        }
    }
}
