using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using RW.PMS.CrossCutting.EventBus.Handlers;
using RW.PMS.CrossCutting.Extensions;

namespace RW.PMS.CrossCutting.EventBus.Store
{
    public class InMemoryEventStore : IEventStore
    {
        private readonly ConcurrentDictionary<Type, List<Type>> _handlers;

        public InMemoryEventStore()
        {
            _handlers = new ConcurrentDictionary<Type, List<Type>>();
        }

        public IList<Type> Get(Type eventType)
        {
            return _handlers.GetOrAdd(eventType, new List<Type>());
        }

        public IList<Type> Get<TEventData>() where TEventData : IEventData
        {
            return Get(typeof(TEventData));
        }

        public void Add(Type eventType, Type handlerType)
        {
            Get(eventType).Locking(t =>
            {
                if (!t.Contains(handlerType))
                {
                    t.Add(handlerType);
                }
            });
        }

        public void Remove(Type eventType, Type handlerType)
        {
            Get(eventType).Locking(t =>
            {
                t.Remove(handlerType);
            });
        }

        public void Remove<TEventData, TEventHandler>() where TEventData : IEventData where TEventHandler : IEventHandler
        {
            Remove(typeof(TEventData), typeof(TEventHandler));
        }
    }
}