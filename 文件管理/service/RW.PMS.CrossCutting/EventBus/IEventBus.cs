using System;
using System.Threading.Tasks;
using RW.PMS.CrossCutting.EventBus.Handlers;

namespace RW.PMS.CrossCutting.EventBus
{
    /// <summary>
    ///     事件总线接口
    /// </summary>
    public interface IEventBus
    {
        /// <summary>
        ///     注册事件
        /// </summary>
        void Register<TEventData, TEventHandler>() where TEventData : IEventData where TEventHandler : IEventHandler;

        /// <summary>
        ///     注册事件
        /// </summary>
        void Register(Type eventType, Type handlerType);

        /// <summary>
        ///     解除注册
        /// </summary>
        void UnRegister<TEventData, TEventHandler>() where TEventData : IEventData where TEventHandler : IEventHandler;

        /// <summary>
        ///     解除注册
        /// </summary>
        void UnRegister(Type eventType, Type handlerType);

        /// <summary>
        ///     触发事件
        /// </summary>
        void Trigger<TEventData>(TEventData eventData) where TEventData : IEventData;

        /// <summary>
        ///     触发事件(异步)
        /// </summary>
        Task TriggerAsync<TEventData>(TEventData eventData) where TEventData : IEventData;
    }
}