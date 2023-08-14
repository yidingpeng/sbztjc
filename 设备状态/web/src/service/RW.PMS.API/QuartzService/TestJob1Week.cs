using iTextSharp.text.pdf.parser.clipper;
using MySqlX.XDevAPI.Relational;
using NPOI.SS.Formula.Functions;
using Quartz;
using RW.PMS.Application.Contracts.DeviceStatus;
using RW.PMS.Application.Contracts.DTO.DeviceStatus;
using RW.PMS.Application.DeviceStatus;
using RW.PMS.CrossCutting.Extender;
using System.Globalization;

namespace RW.PMS.API.QuartzService
{
    public class TestJob1Week : IJob
    {
        private readonly IDeviceStatusService _DeviceStatusService;
        private readonly IDeviceDataCountService _deviceDataCountService;
        private readonly IDRoomDataCountService _dRoomDataCountService;
        private readonly IConfiguration _configuration;
        private readonly IDRoomStatusService _DRoomStatusService;
        public TestJob1Week(IDeviceStatusService deviceStatusService, IConfiguration configuration,
            IDRoomDataCountService dRoomDataCountService,IDeviceDataCountService deviceDataCountService, IDRoomStatusService DRoomStatusService)
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
            DateTime startTime = endTime.AddDays(-7).Date.AddHours(hour.ToInt32());//获取上周同一时间

            int week = getWeek(endTime);//获取周
            int month = endTime.Month;//获取月
            int year = endTime.Year;//获取年

            roomcount(startTime, endTime, week, month, year);
            devicecount(startTime, endTime, week, month, year);
            return Task.CompletedTask;
        }

        public int getWeek(DateTime dateTime)
        {
            DateTime currentDate = dateTime;
            CultureInfo culture = CultureInfo.CurrentCulture;
            Calendar calendar = culture.Calendar;
            int weekNumber = calendar.GetWeekOfYear(currentDate, culture.DateTimeFormat.CalendarWeekRule, culture.DateTimeFormat.FirstDayOfWeek);

            DateTime firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
            int firstWeekNumber = calendar.GetWeekOfYear(firstDayOfMonth, culture.DateTimeFormat.CalendarWeekRule, culture.DateTimeFormat.FirstDayOfWeek);

            int currentMonthWeekNumber = weekNumber - firstWeekNumber + 1;

           return currentMonthWeekNumber;
        }
        public void roomcount(DateTime startTime,DateTime endTime, int week, int month, int year)
        {
            var roomlist = _DRoomStatusService.getRoomAlllist();
            roomlist.ForEach(t => {
                TimeSpan totalEffectiveRunning = TimeSpan.Zero;
                TimeSpan totaltestStop = TimeSpan.Zero;
                TimeSpan totalFault = TimeSpan.Zero;
                TimeSpan totalstandBy = TimeSpan.Zero;
                TimeSpan totalDeviceDebugRun = TimeSpan.Zero;
                TimeSpan totalweibao = TimeSpan.Zero;
                DateTime now = DateTime.Now;
                double totaljiadonglv = 0;
                var effectiveRunList = _dRoomDataCountService.getEffectiveRunningTimeList(t.roomName.Substring(4),startTime,endTime);
                var testStoplist = _dRoomDataCountService.getTestStopTimeList(t.roomName.Substring(4),startTime,endTime);
                var faultlist = _dRoomDataCountService.getFaultTimeTimeList(t.roomName.Substring(4),startTime,endTime);
                var standbylist = _dRoomDataCountService.getStandbyTimeList(t.roomName.Substring(4), startTime, endTime);
                var deviceDebugRunlist = _dRoomDataCountService.getDeviceDebugRunTimeList(t.roomName.Substring(4), startTime, endTime);
                var weibaolist = _dRoomDataCountService.getweibaoTimeList(t.roomName.Substring(4),startTime, endTime);
              var zhidutime=  _dRoomDataCountService.getZhiDuTimeList(t.roomName.Substring(4));
                //有效时间
                if (effectiveRunList.Count > 0)
                {
                    if (effectiveRunList.Last().onOrOff.Equals("on"))
                    {
                        effectiveRunList.Add(new DRoomEffectiveRunningTimeDto { deviceName = "all", roomName = t.roomName, onOrOff = "off", alarmTime = now });
                        _dRoomDataCountService.InsertEffectiveRunningTime(new DRoomEffectiveRunningTimeDto { deviceName = "all", roomName = t.roomName, onOrOff = "off", alarmTime = now });
                        _dRoomDataCountService.InsertEffectiveRunningTime(new DRoomEffectiveRunningTimeDto { deviceName = "all", roomName = t.roomName, onOrOff = "on", alarmTime = now });
                    }
                    if (effectiveRunList.First().onOrOff.Equals("off"))
                    {
                        effectiveRunList.Insert(0, new DRoomEffectiveRunningTimeDto { deviceName = "all", roomName = t.roomName, onOrOff = "on", alarmTime = startTime });
                    }
                }
                for (int i = 0; i < effectiveRunList.Count; i += 2)
                {
                    totalEffectiveRunning += TimeSpan.FromTicks(effectiveRunList[i + 1].alarmTime.Ticks - effectiveRunList[i].alarmTime.Ticks);
                }
                //暂停时间
                if (testStoplist.Count > 0)
                {
                    if (testStoplist.Last().onOrOff.Equals("on"))
                    {
                        testStoplist.Add(new DRoomTestStopTimeDto { deviceName = "all", roomName = t.roomName, onOrOff = "off", alarmTime = now });
                        _dRoomDataCountService.InsertTestStopTime(new DRoomTestStopTimeDto { deviceName = "all", roomName = t.roomName, onOrOff = "off", alarmTime = now });
                        _dRoomDataCountService.InsertTestStopTime(new DRoomTestStopTimeDto { deviceName = "all", roomName = t.roomName, onOrOff = "on", alarmTime = now });
                    }
                    if (testStoplist.First().onOrOff.Equals("off"))
                    {
                        testStoplist.Insert(0, new DRoomTestStopTimeDto { deviceName = "all", roomName = t.roomName, onOrOff = "on", alarmTime = startTime });
                    }
                }
                for (int i = 0; i < testStoplist.Count; i += 2)
                {
                    totaltestStop += TimeSpan.FromTicks(testStoplist[i + 1].alarmTime.Ticks - testStoplist[i].alarmTime.Ticks);
                    
                }
                //故障时间
                if (faultlist.Count > 0)
                {
                    if (faultlist.Last().onOrOff.Equals("on"))
                    {
                        faultlist.Add(new DRoomFaultTimeDto { deviceName = "all", roomName = t.roomName, onOrOff = "off", alarmTime = now });
                        _dRoomDataCountService.InsertFaultTime(new DRoomFaultTimeDto { deviceName = "all", roomName = t.roomName, onOrOff = "off", alarmTime = now });
                        _dRoomDataCountService.InsertFaultTime(new DRoomFaultTimeDto { deviceName = "all", roomName = t.roomName, onOrOff = "on", alarmTime = now });

                    }
                    if (faultlist.First().onOrOff.Equals("off"))
                    {
                        faultlist.Insert(0, new DRoomFaultTimeDto { deviceName = "all", roomName = t.roomName, onOrOff = "on", alarmTime = startTime });
                    }
                }
                for (int i = 0; i < faultlist.Count; i += 2)
                {
                    totalFault += TimeSpan.FromTicks(faultlist[i + 1].alarmTime.Ticks - faultlist[i].alarmTime.Ticks);
                }
                //待机时间
                if (standbylist.Count > 0)
                {
                    if (standbylist.Last().onOrOff.Equals("on"))
                    {
                        standbylist.Add(new DRoomStandbyTimeDto { deviceName = "all", roomName = t.roomName, onOrOff = "off", alarmTime = now });
                        _dRoomDataCountService.InsertStandbyTime(new DRoomStandbyTimeDto { deviceName = "all", roomName = t.roomName, onOrOff = "off", alarmTime = now });
                        _dRoomDataCountService.InsertStandbyTime(new DRoomStandbyTimeDto { deviceName = "all", roomName = t.roomName, onOrOff = "on", alarmTime = now });
                    }
                    if (standbylist.First().onOrOff.Equals("off"))
                    {
                        standbylist.Insert(0, new DRoomStandbyTimeDto { deviceName = "all", roomName = t.roomName, onOrOff = "on", alarmTime = startTime });
                    }
                }
                for (int i = 0; i < standbylist.Count; i += 2)
                {
                    totalstandBy += TimeSpan.FromTicks(standbylist[i + 1].alarmTime.Ticks - standbylist[i].alarmTime.Ticks);
                }
                //调试时间
                if (deviceDebugRunlist.Count > 0)
                {
                    if (deviceDebugRunlist.Last().onOrOff.Equals("on"))
                    {
                        deviceDebugRunlist.Add(new DRoomDeviceDebugRunTimeDto { deviceName = "all", roomName = t.roomName, onOrOff = "off", alarmTime = now });
                        _dRoomDataCountService.InsertDeviceDebugRunTime(new DRoomDeviceDebugRunTimeDto { deviceName = "all", roomName = t.roomName, onOrOff = "off", alarmTime = now });
                        _dRoomDataCountService.InsertDeviceDebugRunTime(new DRoomDeviceDebugRunTimeDto { deviceName = "all", roomName = t.roomName, onOrOff = "on", alarmTime = now });
                    }
                    if (deviceDebugRunlist.First().onOrOff.Equals("off"))
                    {
                        deviceDebugRunlist.Insert(0, new DRoomDeviceDebugRunTimeDto { deviceName = "all", roomName = t.roomName, onOrOff = "on", alarmTime = startTime });
                    }
                }
                for (int i = 0; i < deviceDebugRunlist.Count; i += 2)
                {
                    totalDeviceDebugRun += TimeSpan.FromTicks(deviceDebugRunlist[i + 1].alarmTime.Ticks - deviceDebugRunlist[i].alarmTime.Ticks);

                }
                //维保时间
                
                if (weibaolist.Count > 0)
                {
                    if (weibaolist.Last().onOrOff.Equals("on"))
                    {
                        weibaolist.Add(new DRoomWeiBaoTimeOnOffDto { deviceName = "all", roomName = t.roomName, onOrOff = "off", alarmTime = now });
                        _dRoomDataCountService.InsertweibaoTime(new DRoomWeiBaoTimeOnOffDto { deviceName = "all", roomName = t.roomName, onOrOff = "off", alarmTime = now });
                        _dRoomDataCountService.InsertweibaoTime(new DRoomWeiBaoTimeOnOffDto { deviceName = "all", roomName = t.roomName, onOrOff = "on", alarmTime = now });
                    }
                    if (weibaolist.First().onOrOff.Equals("off"))
                    {
                        weibaolist.Insert(0, new DRoomWeiBaoTimeOnOffDto { deviceName = "all", roomName = t.roomName, onOrOff = "on", alarmTime = startTime });
                    }
                }
                for (int i = 0; i < weibaolist.Count; i += 2)
                {
                    totalweibao += TimeSpan.FromTicks(weibaolist[i + 1].alarmTime.Ticks - weibaolist[i].alarmTime.Ticks);
                }
                var freetime = totalstandBy.TotalSeconds - totalweibao.TotalSeconds;
                if (zhidutime != null)
                    totaljiadonglv = (totalEffectiveRunning.TotalSeconds + totaltestStop.TotalSeconds) / (zhidutime.alarmTime * 60 * 60);
                else
                    totaljiadonglv = (totalEffectiveRunning.TotalSeconds + totaltestStop.TotalSeconds);
                _dRoomDataCountService.InsertweekCountTime(new DRoomWeekCountDto {roomName=t.roomName,totalEffectiveRunningTime=totalEffectiveRunning.TotalSeconds,
                totalTestStopTime=totaltestStop.TotalSeconds,totalFaultTime=totalFault.TotalSeconds,totalStandbyTime=totalstandBy.TotalSeconds,
                totalFreeTime=freetime,totalweibaoTime=totalweibao.TotalSeconds,totalUtilizationRate=totaljiadonglv,deviceDebugRun=totalDeviceDebugRun.TotalSeconds,
                weeks=week,months=month,years=year
                });
            });
            }

        public void devicecount(DateTime startTime, DateTime endTime, int week,int month,int year)
        {
            var devicelist = _DeviceStatusService.getdeviceAlllist();//获取所有设备
            devicelist.ForEach(x => {
                TimeSpan totalRun = TimeSpan.Zero;
                TimeSpan totalStop = TimeSpan.Zero;
                TimeSpan totalFaultDown = TimeSpan.Zero;
                TimeSpan totalweibao = TimeSpan.Zero;
                int totalNumber = 0;
                var roomNameone = _DRoomStatusService.getRoomName(x.roomId);//房间名字
                DateTime now = DateTime.Now;

                var RunList = _deviceDataCountService.getRunTimeList(roomNameone[0].roomName.Substring(4), x.deviceName.Substring(7),startTime,endTime);
                var Stoplist = _deviceDataCountService.getStopTimeList(roomNameone[0].roomName.Substring(4), x.deviceName.Substring(7),startTime,endTime);
                var faultdownlist = _deviceDataCountService.getFaultDownTimeList(roomNameone[0].roomName.Substring(4), x.deviceName,startTime,endTime);
                var weibaolist = _deviceDataCountService.getweibaoTimeList(roomNameone[0].roomName.Substring(4), x.deviceName,startTime,endTime);
                var faltdownnumberlist = _deviceDataCountService.getFaultNumberList(roomNameone[0].roomName.Substring(4), x.deviceName, startTime, endTime);
                //运行时间
                if (RunList.Count > 0)
                {
                    if (RunList.Last().onOrOff.Equals("on"))
                    {
                        RunList.Add(new DeviceRunTimeDto { deviceName = x.deviceName.Substring(7), roomName = roomNameone[0].roomName.Substring(4), onOrOff = "off", alarmTime = now });
                        _deviceDataCountService.InsertRunTimeTime(new DeviceRunTimeDto { deviceName = x.deviceName.Substring(7), roomName = roomNameone[0].roomName.Substring(4), onOrOff = "off", alarmTime = now });
                        _deviceDataCountService.InsertRunTimeTime(new DeviceRunTimeDto { deviceName = x.deviceName.Substring(7), roomName = roomNameone[0].roomName.Substring(4), onOrOff = "on", alarmTime = now });
                    }
                    if (RunList.First().onOrOff.Equals("off"))
                    {
                        RunList.Insert(0, new DeviceRunTimeDto { deviceName = x.deviceName.Substring(7), roomName = roomNameone[0].roomName.Substring(4), onOrOff = "on", alarmTime = startTime });
                    }
                }
                for (int i = 0; i < RunList.Count; i += 2)
                {
                    totalRun += TimeSpan.FromTicks(RunList[i + 1].alarmTime.Ticks - RunList[i].alarmTime.Ticks);
                }
                //停机时间
                if (Stoplist.Count > 0)
                {
                    if (Stoplist.Last().onOrOff.Equals("on"))
                    {
                        Stoplist.Add(new DeviceStopTimeDto { deviceName = x.deviceName.Substring(7), roomName = roomNameone[0].roomName.Substring(4), onOrOff = "off", alarmTime = now });
                        _deviceDataCountService.InsertStopTimeTime(new DeviceStopTimeDto { deviceName = x.deviceName.Substring(7), roomName = roomNameone[0].roomName.Substring(4), onOrOff = "off", alarmTime = now });
                        _deviceDataCountService.InsertStopTimeTime(new DeviceStopTimeDto { deviceName = x.deviceName.Substring(7), roomName = roomNameone[0].roomName.Substring(4), onOrOff = "on", alarmTime = now });
                    }
                    if (Stoplist.First().onOrOff.Equals("off"))
                    {
                        Stoplist.Insert(0, new DeviceStopTimeDto { deviceName = x.deviceName.Substring(7), roomName = roomNameone[0].roomName.Substring(4), onOrOff = "on", alarmTime = startTime });
                    }
                }
                for (int i = 0; i < Stoplist.Count; i += 2)
                {
                    totalStop += TimeSpan.FromTicks(Stoplist[i + 1].alarmTime.Ticks - Stoplist[i].alarmTime.Ticks);
                }
                //故障时间
                //if (faultdownlist.Count > 0)
                //{
                //    if (faultdownlist.Last().onOrOff.Equals("on"))
                //    {
                //        faultdownlist.Add(new DeviceFaultDownTimesDto { deviceName = x.deviceName.Substring(7), roomName = roomNameone[0].roomName.Substring(4), onOrOff = "off", alarmTime = now });
                //    }
                //}
                for (int i = 0; i < faultdownlist.Count; i += 2)
                {
                    totalFaultDown += TimeSpan.FromTicks(faultdownlist[i + 1].alarmTime.Ticks - faultdownlist[i].alarmTime.Ticks);
                }
                //维保时间
                if (weibaolist.Count > 0)
                {
                    if (weibaolist.Last().onOrOff.Equals("on"))
                    {
                        weibaolist.Add(new DeviceWeiBaoTimeOnOffDto { deviceName = x.deviceName.Substring(7), roomName = roomNameone[0].roomName.Substring(4), onOrOff = "off", alarmTime = now });
                        _deviceDataCountService.InsertweibaoTime(new DeviceWeiBaoTimeOnOffDto { deviceName = x.deviceName.Substring(7), roomName = roomNameone[0].roomName.Substring(4), onOrOff = "off", alarmTime = now });
                        _deviceDataCountService.InsertweibaoTime(new DeviceWeiBaoTimeOnOffDto { deviceName = x.deviceName.Substring(7), roomName = roomNameone[0].roomName.Substring(4), onOrOff = "on", alarmTime = now });
                    }
                    if (weibaolist.First().onOrOff.Equals("off"))
                    {
                        weibaolist.Insert(0, new DeviceWeiBaoTimeOnOffDto { deviceName = x.deviceName.Substring(7), roomName = roomNameone[0].roomName.Substring(4), onOrOff = "on", alarmTime = startTime });
                    }
                }
                for (int i = 0; i < weibaolist.Count; i += 2)
                {
                    totalweibao += TimeSpan.FromTicks(weibaolist[i + 1].alarmTime.Ticks - weibaolist[i].alarmTime.Ticks);
                }
                //故障次数
                faltdownnumberlist.ForEach(x => { totalNumber++; }) ;
                //闲置时间
                var freetime = totalStop.TotalSeconds - (totalFaultDown.TotalSeconds+totalweibao.TotalSeconds);
                _deviceDataCountService.InsertweekCountTime(new DeviceWeekCountDto { deviceName= x.deviceName.Substring(7),roomName= roomNameone[0].roomName, totalRunTime=totalRun.TotalSeconds, totalStopTime=totalStop.TotalSeconds, totalFaultDownTime=totalFaultDown.TotalSeconds,toalFaultNumber=totalNumber,totalFreeTime=freetime,weibaoTime=totalweibao.TotalSeconds,weeks=week,months=month,years=year });

            });
        }
    }
}
