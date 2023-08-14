using RW.PMS.WorkerService;
using RW.PMS.WorkerService.Extensions;


//IHost host = Host.CreateDefaultBuilder(args).UseWindowsService(options =>
//{
//��������//�����������������ʾ��Windows�����еķ���������
//��������options.ServiceName = "ListenKepservice";
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
                     //��������������Windows�����еĻ���ʾ����
                     options.ServiceName = "ListenKepservice";
                })
               .ConfigureServices(services =>
               {
                   services.AddHostedService<Worker>();
               })
               .Build();

host.Run();




