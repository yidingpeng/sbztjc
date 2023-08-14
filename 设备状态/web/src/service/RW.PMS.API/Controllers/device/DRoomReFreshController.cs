using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.DeviceStatus;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.DeviceStatus;

namespace RW.PMS.API.Controllers.device
{
    public class DRoomReFreshController : RWBaseController
    {
        private readonly IDRoomStatusService _DRoomStatusService;
        private readonly IDRoomRefreshTimeService _DRoomRefreshTimeService;
        public DRoomReFreshController(IDRoomStatusService dRoomStatusService, IDRoomRefreshTimeService dRoomRefreshTimeService)
        {
            _DRoomStatusService = dRoomStatusService;
            _DRoomRefreshTimeService = dRoomRefreshTimeService;
        }

        [HttpGet("droomReFresh")]
        public IResponseDto droomReFresh()
        {
           var roomlist= _DRoomStatusService.getRoomAlllist();
            roomlist.ForEach(t => {
                TimeSpan totalEffectiveRunning = TimeSpan.Zero;
                TimeSpan totaltestStop = TimeSpan.Zero;
                TimeSpan totalFault = TimeSpan.Zero;
                TimeSpan totalstandBy = TimeSpan.Zero;
                TimeSpan totalDeviceDebugRun = TimeSpan.Zero;
                double totalholiday = 0;
                double totalweibao = 0;
                double totaljiaozhun = 0;
                double totalzhidu = 0;

                DateTime now = DateTime.Now;
               var effectiveRunList= _DRoomRefreshTimeService.getEffectiveRunningTimeList(t.roomName.Substring(4));
               var testStoplist= _DRoomRefreshTimeService.getTestStopTimeList(t.roomName.Substring(4));
                var faultlist = _DRoomRefreshTimeService.getFaultTimeTimeList(t.roomName.Substring(4));
                var standbylist = _DRoomRefreshTimeService.getStandbyTimeList(t.roomName.Substring(4));
                var holidaylist = _DRoomRefreshTimeService.getHolidayTimeList(t.roomName.Substring(4));
               // var weibaolist = _DRoomRefreshTimeService.getweibaoTimeList(t.roomName.Substring(4));
                var jioazhunlist = _DRoomRefreshTimeService.getJiaoZhunTimeList(t.roomName.Substring(4));
                var zhidulist = _DRoomRefreshTimeService.getZhiDuTimeList(t.roomName.Substring(4));
                var deviceDebugRunlist=_DRoomRefreshTimeService.getDeviceDebugRunTimeList(t.roomName.Substring(4));


                //有效时间
                if (effectiveRunList.Count % 2 != 0)
                {
                    effectiveRunList.Add(new DRoomEffectiveRunningTimeDto { deviceName="all",roomName=t.roomName,onOrOff="off",alarmTime=now });
                }
               
                    for (int i = 0; i < effectiveRunList.Count; i += 2)
                {
                    totalEffectiveRunning += TimeSpan.FromTicks(effectiveRunList[i+1].alarmTime.Ticks - effectiveRunList[i].alarmTime.Ticks);
                }
                //暂停时间
                if (testStoplist.Count % 2 != 0)
                {
                    testStoplist.Add(new DRoomTestStopTimeDto { deviceName = "all", roomName = t.roomName, onOrOff = "off", alarmTime = now });
                }
                
                    for (int i = 0; i < testStoplist.Count; i += 2)
                {
                    totaltestStop += TimeSpan.FromTicks(testStoplist[i + 1].alarmTime.Ticks - testStoplist[i].alarmTime.Ticks);
                }
                //故障时间
                if (faultlist.Count % 2 != 0)
                {
                    faultlist.Add(new DRoomFaultTimeDto { deviceName = "all", roomName = t.roomName, onOrOff = "off", alarmTime = now });
                }
                
                    for (int i = 0; i < faultlist.Count; i += 2)
                {
                    totalFault += TimeSpan.FromTicks(faultlist[i + 1].alarmTime.Ticks - faultlist[i ].alarmTime.Ticks);
                }
                //待机时间
                if (standbylist.Count % 2 != 0)
                {
                    standbylist.Add(new DRoomStandbyTimeDto { deviceName = "all", roomName = t.roomName, onOrOff = "off", alarmTime = now });
                }
                
                    for (int i = 0; i < standbylist.Count; i += 2)
                {
                    totalstandBy += TimeSpan.FromTicks(standbylist[i + 1].alarmTime.Ticks - standbylist[i].alarmTime.Ticks);
                }
                //调试时间
                if (deviceDebugRunlist.Count % 2 != 0)
                {
                    deviceDebugRunlist.Add(new DRoomDeviceDebugRunTimeDto { deviceName = "all", roomName = t.roomName, onOrOff = "off", alarmTime = now });
                }

                for (int i = 0; i < deviceDebugRunlist.Count; i += 2)
                {
                    totalDeviceDebugRun += TimeSpan.FromTicks(deviceDebugRunlist[i + 1].alarmTime.Ticks - deviceDebugRunlist[i].alarmTime.Ticks);
                }
                //闲置时间
                holidaylist.ForEach(t => totalholiday += t.alarmTime);
              //  weibaolist.ForEach(t => totalweibao += t.alarmTime);
                jioazhunlist.ForEach(t => totaljiaozhun += t.alarmTime);
                var freetime = totalstandBy.TotalSeconds -( totalholiday * 60 * 60 + totalweibao * 60 * 60 + totaljiaozhun * 60 * 60);
               // var jiadonglv = (totalEffectiveRunning + totaltestStop).TotalSeconds / zhidulist[0].alarmTime * 60 * 60;
                _DRoomStatusService.updateDRoomtotalEffectiveRunningTime(t.roomName.Substring(4), totalEffectiveRunning.TotalSeconds);
                _DRoomStatusService.updateDRoomtotalTestStopTime(t.roomName.Substring(4), totaltestStop.TotalSeconds);
                _DRoomStatusService.updateDRoomtotalFaultTime(t.roomName.Substring(4), totalFault.TotalSeconds);
                _DRoomStatusService.updateDRoomtotalStandbyTime(t.roomName.Substring(4), totalstandBy.TotalSeconds);
                _DRoomStatusService.updateDRoomDeviceDebugRunTime(t.roomName.Substring(4), totalDeviceDebugRun.TotalSeconds);
                if (!double.IsNaN(freetime))
                    _DRoomStatusService.updateDRoomtotalFreeTime(t.roomName.Substring(4), freetime);
              //  _DRoomStatusService.updateDRoomtotalUtilizationRate(t.roomName.Substring(4), jiadonglv);
               // _DRoomStatusService.updateDRoomtotalweibaoTime(t.roomName.Substring(4), totalweibao*60*60);

            });
            return ResponseDto.Success("刷新成功！");
        }
    }
}
