using RW.PMS.Application.Contracts.Orders;
using RW.PMS.WorkerService.Extensions;
using Opc.Ua;
using Opc.Ua.Client;
namespace RW.PMS.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IOdersService _ordersService;
        private readonly IFreeSql _freeSql;
        private readonly WriteLogFile _writeLogFile;
       
        public Worker(ILogger<Worker> logger)
        {
            var servicesPrvider = ConfigurationService.Injection();
            
            _logger = logger;
            _freeSql = servicesPrvider.GetService<IFreeSql>();
              _writeLogFile = servicesPrvider.GetService<WriteLogFile>();
            var endpointUrl = "opc.tcp://localhost:4840"; // OPC UA��������URL
            //var client = new UaClient(endpointUrl);
        }
        /// <summary>
        /// ��ʼʱ����
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            _writeLogFile.WriteLog("����ʱ��Ϊ: " + DateTimeOffset.Now);

            await base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _writeLogFile.WriteLog("ִ��ʱ��Ϊ: " + DateTimeOffset.Now);
                _freeSql.Insert(new user() { id = 1, name = Directory.GetCurrentDirectory() }).ExecuteAffrows();
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
        /// <summary>
        /// ֹͣʱ����
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _writeLogFile.WriteLog("ֹͣʱ��Ϊ: " + DateTimeOffset.Now);
            await base.StopAsync(cancellationToken);
        }

    }
}