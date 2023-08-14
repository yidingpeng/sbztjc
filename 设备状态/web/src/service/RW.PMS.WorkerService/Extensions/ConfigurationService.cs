using Mapster;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.WorkerService.Extensions
{
    public static class ConfigurationService
    {
        public static IServiceCollection Services;
        public static IConfiguration config;







        public static IServiceProvider Injection()
        {
            config = new ConfigurationBuilder()
              .SetBasePath(@"D:\Project\GJCX22006_车轴自动喷漆\src\service\RW.PMS.WorkerService")
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
              .Build();
            Services = new ServiceCollection();

            var serviceProvider = Services
                .AddLogging(logBuilder =>
                {
                    logBuilder.AddConfiguration(config.GetSection("Logging"));
                    logBuilder.AddConsole();

                    //logBuilder.AddConsole();
                })
                .AddCustomServices(config)
                .BuildServiceProvider();

            return serviceProvider;
        }

        public static IServiceCollection AddCustomServices(this IServiceCollection service, IConfiguration config)
        {
            // 注入FreeSql

            var dbStr = config["MySqlConnectionStrings:DbConnectionStr"];

            Func<IServiceProvider, IFreeSql> fsqlFactory = r =>
            {
                IFreeSql fsql = new FreeSql.FreeSqlBuilder()
                    .UseConnectionString(FreeSql.DataType.MySql,  dbStr)
                    //.UseMonitorCommand(cmd => Console.WriteLine($"Sql：{cmd.CommandText}"))//监听SQL语句
                    .UseAutoSyncStructure(true) //自动同步实体结构到数据库，FreeSql不会扫描程序集，只有CRUD时才会生成表。
                    .Build();
                return fsql;
            };
            // 创建 Socket 对象
           // Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // 连接服务器
          

            service.AddSingleton(fsqlFactory);
            // Mapster注入
            var mapsterConfig = new TypeAdapterConfig();
            // Or
            // var config = TypeAdapterConfig.GlobalSettings;
            service.AddSingleton(mapsterConfig);
           
           //service.AddSingleton<Worker>();
            service.AddSingleton<WriteLogFile>();
            
            
            // service.AddSingleton(socket);

            //var assembly = typeof(OnMessagePOSServers).Assembly;
            //var types = assembly.GetTypes().Where(t => t.Namespace == "RW.Position.websocketServers");

            ////注册所有类
            //foreach (var type in types)
            //{
            //    service.AddSingleton(type);
            //}
            return service;
        }
    }
}
