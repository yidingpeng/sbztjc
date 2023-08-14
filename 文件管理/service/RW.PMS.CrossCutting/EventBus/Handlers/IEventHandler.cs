namespace RW.PMS.CrossCutting.EventBus.Handlers
{
    /// <summary>
    ///     事件处理接口
    /// </summary>
    public interface IEventHandler
    {
    }

    /// <summary>
    ///     泛型事件处理接口
    /// </summary>
    /// <typeparam name="TEventData"></typeparam>
    public interface IEventHandler<in TEventData> : IEventHandler where TEventData : IEventData
    {
        /// <summary>
        ///     事件处理器实现该方法来处理事件
        /// </summary>
        /// <param name="eventData"><see cref="IEventData"/>事件源</param>
        void HandleEvent(TEventData eventData);
    }
}