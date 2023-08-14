using RW.PMS.Application.Contracts.DTO.Log;
using RW.PMS.Application.Contracts.Service;
using RW.PMS.Application.Contracts.System;
using RW.PMS.CrossCutting.EventBus;
using RW.PMS.CrossCutting.EventBus.Handlers;
using RW.PMS.CrossCutting.Security.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Event.EventHandlers
{
    public class LoginEventHandler : IEventHandler<LoginEventData>
    {
        IStatService statSerivce;
        IEventBus eventBus;
        public LoginEventHandler(IEventBus eventBus, IStatService statSerivce)
        {
            this.statSerivce = statSerivce;
            this.eventBus = eventBus;
        }

        public void HandleEvent(LoginEventData eventData)
        {
            if (!eventData.LastLogin.HasValue || eventData.LastLogin.Value != DateTime.Now.Date)
                statSerivce.SetStat(DateTime.Now, "User", "LoginCount", 1);
            //if (eventData.Result)
            //    logService.AddLoginLog(true, "登录成功", $"{eventData.Realname}登录了系统");
            //else
            //    logService.AddLoginLog(false, "登录失败", $"{eventData.Username}登录失败");


            if (eventData.Result)
                eventBus.Trigger(new LogEventData(LogTypeEnum.Login, eventData.Result, "登录成功", $"[{eventData.Realname}]登录了系统。"));
            else
                eventBus.Trigger(new LogEventData
                {
                    Result = eventData.Result,
                    Message = "登录失败",
                    Desc = $"账号{eventData.Username}登录失败。",
                    Type = Contracts.DTO.Log.LogTypeEnum.Login
                }); ;
        }
    }
}
