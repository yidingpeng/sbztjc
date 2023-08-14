using RW.PMS.WorkerService;
using RW.PMS.WorkerService.Extensions;


//IHost host = Host.CreateDefaultBuilder(args).UseWindowsService(options =>
//{
//　　　　//服务进行命名，会显示在Windows服务中的服务名称上
//　　　　options.ServiceName = "ListenKepservice";
//})
//    .ConfigureServices(services =>
//    {
//        services.AddHostedService<Worker>();
//    })
//    .Build();

//await host.RunAsync();

IHost host = Host.CreateDefaultBuilder(args)
                .UseWindowsService(options =>
                {
                     //给服务命名，在Windows服务中的会显示名字
                     options.ServiceName = "ListenKepservice";
                })
               .ConfigureServices(services =>
               {
                   services.AddHostedService<Worker>();
               })
               .Build();

host.Run();




