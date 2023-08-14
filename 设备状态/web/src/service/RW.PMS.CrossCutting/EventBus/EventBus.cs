using Autofac;
using RW.PMS.CrossCutting.EventBus.Handlers;
using RW.PMS.CrossCutting.EventBus.Store;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RW.PMS.CrossCutting.EventBus
{
    public class EventBus : IEventBus
    {
        private readonly ILifetimeScope _lifetimeScope;
        private readonly IEventStore _eventStore;
        public EventBus(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
            _eventStore ??= new InMemoryEventStore();
        }


        public void Register<TEventData, TEventHandler>() where TEventData : IEventData where TEventHandler : IEventHandler
        {
            Register(typeof(TEventData), typeof(TEventHandler));
        }

        public void Register(Type eventType, Type handlerType)
        {
            _eventStore.Add(eventType, handlerType);
        }

        public void UnRegister<TEventData, TEventHandler>() where TEventData : IEventData where TEventHandler : IEventHandler
        {
            UnRegister(typeof(TEventData), typeof(TEventHandler));
        }

        public void UnRegister(Type eventType, Type handlerType)
        {
            _eventStore.Remove(eventType, handlerType);
        }


        public void Trigger<TEventData>(TEventData eventData) where TEventData : IEventData
        {
            var handlers = _eventStore.Get<TEventData>();
            if (!handlers.Any()) return;
            using var scope = _lifetimeScope.BeginLifetimeScope();
            foreach (var handler in handlers)
            {
                var eventHandler = (IEventHandler<TEventData>) scope.Resolve(handler);
                eventHandler.HandleEvent(eventData);
            }
        }

        public async Task TriggerAsync<TEventData>(TEventData eventData) where TEventData : IEventData
        {
            await Task.Run(() => { Trigger(eventData); });
        }
    }
}