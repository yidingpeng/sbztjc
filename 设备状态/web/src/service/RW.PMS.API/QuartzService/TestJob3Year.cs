using Quartz;
using RW.PMS.Application.Contracts.DeviceStatus;
using RW.PMS.Application.Contracts.DTO.DeviceStatus;
using System.Globalization;

namespace RW.PMS.API.QuartzService
{
    public class TestJob3Year : IJob
    {
        private readonly IDeviceStatusService _DeviceStatusService;
        private readonly IDeviceDataCountService _deviceDataCountService;
        private readonly IDRoomDataCountService _dRoomDataCountService;
        private readonly IConfiguration _configuration;
        private readonly IDRoomStatusService _DRoomStatusService;

        public TestJob3Year(IDeviceStatusService deviceStatusService, IConfiguration configuration,
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


            // 获取当前时间
            DateTime endTime = DateTime.Now;

            // 获取今年初0点时间
            // DateTime thisYearStartTime = new DateTime(startTime.Year, 1, 1, 0, 0, 0);

            // 计算去年初0点时间
            DateTime startTime = endTime.AddYears(-1).Date;
            int year = startTime.Year;//获取年
            roomcount(year);
            devicecount(year);




            return Task.CompletedTask;
        }
        public void roomcount( int year)
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
                var roomweeklist = _dRoomDataCountService.GetMonthCount(year,  t.roomName.Substring(4));
                roomweeklist.ForEach(x => {
                    totalEffectiveRunning += x.totalEffectiveRunningTime;
                    totaltestStop += x.totalTestStopTime;
                    totalFault += x.totalFaultTime;
                    totalstandBy += x.totalStandbyTime;
                    totalDeviceDebugRun += x.deviceDebugRun;
                    totalweibao += x.totalweibaoTime;
                    totaljiadonglv += x.totalUtilizationRate;
                    totalfreetime += x.totalFreeTime;
                });
                _dRoomDataCountService.InsertYearCountTime(new DRoomYearCountDto
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
                    
                    years = year
                });
            });
        }
        public void devicecount( int year)
        {
            var devicelist = _DeviceStatusService.getdeviceAlllist();//获取所有设备
            devicelist.ForEach(x =>
            {
                double totalRun = 0;
                double totalStop = 0;
                double totalFaultDown = 0;
                double totalweibao = 0;
                int totalNumber = 0;
                var roomNameone = _DRoomStatusService.getRoomName(x.roomId);//房间名字

                double totalfree = 0;
                var deviceweeklist = _deviceDataCountService.GetMonthCount(year,  roomNameone[0].roomName.Substring(4), x.deviceName.Substring(7));
                deviceweeklist.ForEach(t => {
                    totalRun += t.totalRunTime;
                    totalStop += t.totalStopTime;
                    totalFaultDown += t.totalFaultDownTime;
                    totalweibao += t.weibaoTime;
                    totalNumber += t.toalFaultNumber;
                    totalfree += t.totalFreeTime;
                });
                _deviceDataCountService.InsertYearCountTime(new DeviceYearCountDto()
                {
                    roomName = roomNameone[0].roomName,
                    deviceName = x.deviceName.Substring(7),
                    totalRunTime = totalRun,
                    totalStopTime = totalStop,
                    totalFaultDownTime = totalFaultDown,
                    toalFaultNumber = totalNumber,
                    totalFreeTime = totalfree,
                    weibaoTime = totalweibao,
                    years = year
                });
            });
        }
    }
    

}
