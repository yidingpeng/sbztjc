using Autofac;
using System.Reflection;
using Module = Autofac.Module;

namespace RW.PMS.CrossCutting.IOC.Modules
{
    /// <summary>
    ///     仓储服务注册
    /// </summary>
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = Assembly.Load("RW.PMS.Infrastructure");
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}