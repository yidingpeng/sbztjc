using System;

namespace RW.PMS.CrossCutting.EventBus
{
    public class EventData : IEventData
    {
        public EventData()
        {
            EventTime = DateTime.Now;
        }

        public DateTime EventTime { get; set; }
        public object EventSource { get; set; }
    }
}