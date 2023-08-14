using RW.PMS.Application.Contracts.Service;
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
    public class WorkflowEventHandler : IEventHandler<WorkflowEventData>
    {
        IEventBus eventBus;
        Lazy<ICurrentUser> currentUser;
        IStatService statService;

        public WorkflowEventHandler(Lazy<ICurrentUser> currentUser, IEventBus eventBus, IStatService statService)
        {
            this.currentUser = currentUser;
            this.eventBus = eventBus;
            this.statService = statService;
        }

        public void HandleEvent(WorkflowEventData eventData)
        {
            var action = eventData.GetActionText();

            if (eventData.Action == WorkflowActionEnums.Publish)
            {
                statService.SetStat(DateTime.Now, "Workflow", "WorkflowCount");
            }
            else if (eventData.Action == WorkflowActionEnums.Pass || eventData.Action == WorkflowActionEnums.Reject)
            {
                statService.SetStat(DateTime.Now, "Workflow", "WorkflowExecuted");
            }

            if (!eventData.NoMessageAction())
                eventBus.Trigger(new MessageEventData
                {
                    DataId = eventData.DataId,
                    Title = $"[{action}] 流程 {eventData.Title}。",
                    Type = "workflow",
                    UserIds = eventData.UserIds,
                    Content = eventData.Content,
                });
            eventBus.Trigger(LogEventData.OperationData(action + "流程", true, eventData.Title));
        }
    }
}
