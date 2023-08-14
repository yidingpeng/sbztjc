using Autofac;
using RW.PMS.CrossCutting.EventBus;
using RW.PMS.CrossCutting.EventBus.Handlers;
using System.Linq;

namespace RW.PMS.CrossCutting.IOC.Modules.StartModule
{
    public class StartForEventBus : IStartable
    {
        private readonly ILifetimeScope _lifetimeScope;
        private readonly IEventBus _eventBus;

        public StartForEventBus(ILifetimeScope lifetimeScope, IEventBus eventBus)
        {
            _lifetimeScope = lifetimeScope;
            _eventBus = eventBus;
        }

        public void Start()
        {
            using var scope = _lifetimeScope.BeginLifetimeScope();
            //获取所有IEventHandler接口的实现
            var handlers = scope.ComponentRegistry.Registrations.Where(t =>
                    typeof(IEventHandler).IsAssignableFrom(t.Activator.LimitType)).Select(t => t.Activator.LimitType)
                .ToList();
            if (!handlers.Any()) return;
            foreach (var handler in handlers)
            {
                var interfaces = handler.GetInterfaces();
                foreach (var @interface in interfaces)
                {
                    if (!typeof(IEventHandler).IsAssignableFrom(@interface)) continue;
                    var genericArgs = @interface.GetGenericArguments();
                    if (genericArgs.Length == 1)
                    {
                        _eventBus.Register(genericArgs[0], handler);
                    }
                }
            }
        }
    }
}