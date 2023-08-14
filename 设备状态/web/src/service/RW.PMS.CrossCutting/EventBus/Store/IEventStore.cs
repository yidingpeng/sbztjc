using System;
using System.Collections.Generic;
using RW.PMS.CrossCutting.EventBus.Handlers;

namespace RW.PMS.CrossCutting.EventBus.Store
{
    public interface IEventStore
    {
        IList<Type> Get(Type eventType);

        IList<Type> Get<TEventData>() where TEventData : IEventData;

        void Add(Type eventType, Type handlerType);

        void Remove(Type eventType, Type handlerType);

        void Remove<TEventData, TEventHandler>() where TEventData : IEventData where TEventHandler : IEventHandler;
    }
}