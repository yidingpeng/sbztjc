using Hangfire.Common;
using NPOI.HPSF;
using Quartz;
using RW.PMS.Application.Contracts.DeviceStatus;
using RW.PMS.Application.Contracts.DTO.DeviceStatus;

namespace RW.PMS.API.QuartzService
{
    public class TestJob2Month : IJob
    {
        private readonly IDeviceStatusService _DeviceStatusService;
      
        private readonly IDeviceDataCountService _deviceDataCountService;
        private readonly IDRoomDataCountService _dRoomDataCountService;
        private readonly IConfiguration _configuration;
        private readonly IDRoomStatusService _DRoomStatusService;
        public TestJob2Month(IDeviceStatusService deviceStatusService, IConfiguration configuration,
            IDRoomDataCountService dRoomDataCountService, IDeviceDataCountService deviceDataCountService, IDRoomStatusService DRoomStatusService)
        {
            _DeviceStatusService = deviceStatusService;
            _configuration = configuration;
            _deviceDataCountService = deviceDataCountService;
            _dRoomDataCountService = dRoomDataCountService;
            _DRoomStatusService = DRoomStatusService;
        }
        public Task Execute(IJobExecutionContext context)
        {
            string hour = _configuration["datacounttime:hour"];
            DateTime endTime = DateTime.Now;
            //DateTime thisMonthStart = new DateTime(startTime.Year, startTime.Month, 1);
            DateTime startTime = endTime.AddMonths(-1).Date;
            int month = startTime.Month;//获取月
            int year = startTime.Year;//获取年
            roomcount(month, year);
            devicecount(month, year);

            //Console.WriteLine("上月初0点时间：" + startTime.ToString("yyyy-MM-dd HH:mm:ss"));

            return Task.CompletedTask;
        }
        public void roomcount( int month, int year)
        {
            var roomlist = _DRoomStatusService.getRoomAlllist();
            roomlist.ForEach(t =>
            {
                double totalEffectiveRunning = 0;
                double totaltestStop = 0;
                double totalFault = 0;
                double totalstandBy = 0;
                double totalDeviceDebugRun = 0;
                double totalweibao = 0;
                double totaljiadonglv = 0;
                double totalfreetime = 0;
              var roomweeklist=  _dRoomDataCountService.getweekcount(year, month, t.roomName.Substring(4));
                roomweeklist.ForEach(x => {
                    totalEffectiveRunning += x.totalEffectiveRunningTime;
                    totaltestStop += x.totalTestStopTime;
                    totalFault+= x.totalFaultTime;
                    totalstandBy += x.totalStandbyTime;
                    totalDeviceDebugRun += x.deviceDebugRun;
                    totalweibao += x.totalweibaoTime;
                    totaljiadonglv += x.totalUtilizationRate;
                    totalfreetime += x.totalFreeTime;
                });
                _dRoomDataCountService.InsertMonthCountTime(new DRoomMonthCountDto
                {
                    roomName = t.roomName,
                    totalEffectiveRunningTime = totalEffectiveRunning,
                    totalTestStopTime = totaltestStop,
                    totalFaultTime = totalFault,
                    totalStandbyTime = totalstandBy,
                    totalFreeTime = totalfreetime,
                    totalweibaoTime = totalweibao,
                    totalUtilizationRate = totaljiadonglv,
                    deviceDebugRun = totalDeviceDebugRun,
                    months = month,
                    years = year
                });
            });
            }
        public void devicecount( int month, int year)
        {
            var devicelist = _DeviceStatusService.getdeviceAlllist();//获取所有设备
            devicelist.ForEach(x =>
            {
                double totalRun =0;
                double totalStop =0;
                double totalFaultDown = 0;
                double totalweibao = 0;
                int totalNumber = 0;
                var roomNameone = _DRoomStatusService.getRoomName(x.roomId);//房间名字
                
                double totalfree = 0;
               var deviceweeklist= _deviceDataCountService.getweekcount(year, month, roomNameone[0].roomName.Substring(4), x.deviceName.Substring(7));
                deviceweeklist.ForEach(t => {
                    totalRun += t.totalRunTime;
                    totalStop += t.totalStopTime;
                    totalFaultDown += t.totalFaultDownTime;
                    totalweibao += t.weibaoTime;
                    totalNumber += t.toalFaultNumber;
                    totalfree += t.totalFreeTime;
                });
                _deviceDataCountService.InsertMonthCountTime(new DeviceMonthCountDto()
                {
                    roomName = roomNameone[0].roomName,
                    deviceName = x.deviceName.Substring(7),
                    totalRunTime=totalRun, totalStopTime=totalStop,
                    totalFaultDownTime= totalFaultDown,
                    toalFaultNumber=totalNumber,
                    totalFreeTime=totalfree,
                    weibaoTime=totalweibao,
                    months=month,years=year
                }) ;
            });
            }
    }
}
