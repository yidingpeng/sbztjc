using System;

namespace RW.PMS.CrossCutting.EventBus
{   
    /// <summary>
    ///     事件源接口
    /// </summary>
    public interface IEventData
    {
        /// <summary>
        ///     时间触发时间
        /// </summary>
        DateTime EventTime { get; set; }

        /// <summary>
        ///     触发事件对象
        /// </summary>
        object EventSource { get; set; }
    }
}