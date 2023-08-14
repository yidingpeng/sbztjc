using Autofac;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.EventBus;
using RW.PMS.CrossCutting.EventBus.Handlers;
using RW.PMS.CrossCutting.Localization;
using RW.PMS.CrossCutting.Localization.AutoMapper;
using RW.PMS.CrossCutting.Localization.Base;
using RW.PMS.CrossCutting.Security.Token;
using RW.PMS.CrossCutting.Security.User;
using System.Reflection;
using RW.PMS.CrossCutting.IOC.Modules.StartModule;
using Module = Autofac.Module;

namespace RW.PMS.CrossCutting.IOC.Modules
{
    /// <summary>
    ///     注入
    /// </summary>
    public class CrossCuttingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StringTypeConverter>().SingleInstance();
            builder.RegisterType<EnumList>().As<IEnumList>().SingleInstance();
            builder.RegisterType<DataValidatorProvider>().As<IDataValidatorProvider>().SingleInstance();
            builder.RegisterType<CurrentUser>().As<ICurrentUser>().SingleInstance();
            builder.RegisterType<AuthToken>().As<IAuthToken>().SingleInstance();


            #region EventBus

            builder.RegisterType<EventBus.EventBus>().As<IEventBus>().SingleInstance();
            var assembly = Assembly.Load("RW.PMS.Application");
            builder.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(IEventHandler<>)).InstancePerDependency();

            #endregion

            #region StartModule

            builder.RegisterType<StartForEventBus>().As<IStartable>().SingleInstance();

            #endregion
        }
    }
}