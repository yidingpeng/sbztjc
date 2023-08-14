using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.DeviceStatus;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.DeviceStatus;

namespace RW.PMS.API.Controllers.device
{
    public class DevicveReFreshController : RWBaseController
    {
        private readonly IDeviceStatusService _DeviceStatusService;
        private readonly IDeviceRefreshTimeService _DeviceRefreshTimeService;
        private readonly IDRoomStatusService _DRoomStatusService;
        public DevicveReFreshController(IDeviceStatusService deviceStatusService, IDeviceRefreshTimeService deviceRefreshTimeService  , IDRoomStatusService DRoomStatusService)
        {
            _DeviceStatusService = deviceStatusService;
            _DeviceRefreshTimeService = deviceRefreshTimeService;
            _DRoomStatusService = DRoomStatusService;
        }

        [HttpGet("deviceReFresh")]
        public IResponseDto deviceReFresh()
        {
            var devicelist = _DeviceStatusService.getdeviceAlllist();
            devicelist.ForEach(t => {
                TimeSpan totalRun = TimeSpan.Zero;
                TimeSpan totalStop = TimeSpan.Zero;
                TimeSpan totalFaultDown = TimeSpan.Zero;
               
                double totalholiday = 0;
                double totalweibao = 0;
                double totaltrainning = 0;
                

                DateTime now = DateTime.Now;
                var roomNamelist = _DRoomStatusService.getRoomName(t.roomId);
                var RunList = _DeviceRefreshTimeService.getRunTimeList(roomNamelist[0].roomName.Substring(4),t.deviceName.Substring(7));
                var Stoplist = _DeviceRefreshTimeService.getStopTimeList(roomNamelist[0].roomName.Substring(4), t.deviceName.Substring(7));
                var faultdownlist = _DeviceRefreshTimeService.getFaultDownTimeList(roomNamelist[0].roomName.Substring(4), t.deviceName);
                
                var holidaylist = _DeviceRefreshTimeService.getHolidayTimeList(roomNamelist[0].roomName.Substring(4), t.deviceName.Substring(7));
            //    var weibaolist = _DeviceRefreshTimeService.getweibaoTimeList(roomNamelist[0].roomName.Substring(4), t.deviceName.Substring(7));
                var trainninglist = _DeviceRefreshTimeService.getTrainningTimeList(roomNamelist[0].roomName.Substring(4), t.deviceName.Substring(7));
                
                //运行时间
                if (RunList.Count % 2 != 0)
                {
                    RunList.Add(new DeviceRunTimeDto { deviceName = t.deviceName.Substring(7), roomName = roomNamelist[0].roomName.Substring(4), onOrOff = "off", alarmTime = now });
                }
                
                for (int i = 0; i < RunList.Count; i += 2)
                {
                    totalRun += TimeSpan.FromTicks(RunList[i + 1].alarmTime.Ticks - RunList[i].alarmTime.Ticks);
                }
                //停机时间
                if (Stoplist.Count % 2 != 0)
                {
                    Stoplist.Add(new DeviceStopTimeDto { deviceName = t.deviceName.Substring(7), roomName = roomNamelist[0].roomName.Substring(4), onOrOff = "off", alarmTime = now });
                }
                
                    for (int i = 0; i < Stoplist.Count; i += 2)
                {
                    totalStop += TimeSpan.FromTicks(Stoplist[i + 1].alarmTime.Ticks - Stoplist[i].alarmTime.Ticks);
                }
                //故障时间
                if (faultdownlist.Count % 2 != 0)
                {
                    faultdownlist.Add(new DeviceFaultDownTimesDto { deviceName = t.deviceName.Substring(7), roomName = roomNamelist[0].roomName.Substring(4), onOrOff = "off", alarmTime = now });
                }
                
                    for (int i = 0; i < faultdownlist.Count; i += 2)
                {
                    totalFaultDown += TimeSpan.FromTicks(faultdownlist[i + 1].alarmTime.Ticks - faultdownlist[i].alarmTime.Ticks);
                }

                //闲置时间
                holidaylist.ForEach(t => totalholiday += t.alarmTime);
             //   weibaolist.ForEach(t => totalweibao += t.alarmTime);
                trainninglist.ForEach(t => totaltrainning += t.alarmTime);
                var freetime = totalStop.TotalSeconds - (totalFaultDown.TotalSeconds+ totalweibao * 60 * 60 + totalholiday * 60 * 60+totaltrainning * 60 * 60);

                _DeviceStatusService.updateDevicetotalRunTime(t.deviceName, t.roomId, totalRun.TotalSeconds);
                _DeviceStatusService.updateDevicetotalStopTime(t.deviceName, t.roomId, totalStop.TotalSeconds);
                _DeviceStatusService.updateDevicetotalFaultDownTime(t.deviceName, t.roomId, totalFaultDown.TotalSeconds);
             //   _DeviceStatusService.updateDevicetotalweibaoTime(t.deviceName, t.roomId, totalweibao*60*60);
                _DeviceStatusService.updateDevicetotalFreeTime(t.deviceName, t.roomId, freetime);
            
                
            });
            return ResponseDto.Success("刷新成功！");
        }
    }
}
