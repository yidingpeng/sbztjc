
using RW.PMS.CrossCutting.EventBus;

namespace RW.PMS.Application.Event
{
    public class MessageEventData : EventData
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int[] UserIds { get; set; }
        public string Type { get; set; }
        public int DataId { get; set; }
    }

}