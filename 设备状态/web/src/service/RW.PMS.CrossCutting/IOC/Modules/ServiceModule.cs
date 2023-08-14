using Autofac;
using Autofac.Extras.DynamicProxy;
using RW.PMS.CrossCutting.AOP.Interceptors;
using System;
using System.Collections.Generic;
using System.Reflection;
using Module = Autofac.Module;

namespace RW.PMS.CrossCutting.IOC.Modules
{
    /// <summary>
    ///     应用服务注册
    /// </summary>
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DatabaseTransaction>();
            builder.RegisterType<DatabaseTransactionAsync>();

            var interceptorType = new List<Type>
            {
                typeof(DatabaseTransaction)
            };

            var assembly = Assembly.Load("RW.PMS.Application");

            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Service"));

            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .PropertiesAutowired()
                .InterceptedBy(interceptorType.ToArray())
                .EnableInterfaceInterceptors();
        }
    }
}