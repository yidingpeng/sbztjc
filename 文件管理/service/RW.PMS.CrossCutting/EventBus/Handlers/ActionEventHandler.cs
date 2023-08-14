using System;

namespace RW.PMS.CrossCutting.EventBus.Handlers
{
    internal class ActionEventHandler<TEventData> : IEventHandler<TEventData> where TEventData : IEventData
    {
        public Action<TEventData> Action { get; }

        public ActionEventHandler(Action<TEventData> action)
        {
            Action = action;
        }

        public void HandleEvent(TEventData eventData)
        {
            Action(eventData);
        }
    }
}