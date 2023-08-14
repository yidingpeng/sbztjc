using Quartz;
using RW.PMS.Common;
using RW.PMS.IBLL;
using RW.PMS.Model.watchDevice;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.WinForm
{
    public class TestJob : IJob
    {
        private static IBLL_DeviceCount _DeviceCount = DIService.GetService<IBLL_DeviceCount>();
        private static IBLL_DeviceStatus _deviceStatus = DIService.GetService<IBLL_DeviceStatus>();
        private static IBLL_DeviceTimes _device = DIService.GetService<IBLL_DeviceTimes>();

        void IJob.Execute(IJobExecutionContext context)
        {
            //DateTime now = DateTime.Now;
            //DateTime lastFriday = now;

            //// 将当前时间往前推移直到找到上周五
            //while (lastFriday.DayOfWeek != DayOfWeek.Friday)
            //{
            //    lastFriday = lastFriday.AddDays(-1);
            //}

            //// 设置时间为上周五的3:00
            //DateTime startTime = new DateTime(lastFriday.Year, lastFriday.Month, lastFriday.Day, 3, 0, 0);
            ////Debug.WriteLine(lastFriday3AM);
           
            //int weekOfMonth = GetWeekOfMonth(now);
            //Debug.WriteLine("现在时间是本月的第 {0} 周", weekOfMonth);
            //int currentMonth = DateTime.Now.Month;
            //int currentYear = DateTime.Now.Year;
            //TimeSpan opertiontricks = TimeSpan.Zero;
            //TimeSpan stopRuntricks = TimeSpan.Zero;

            //var listdevice = _deviceStatus.getDevices();
            //    listdevice.ForEach(t => {
            //        var faultNumberlist = _device.GetFaultNumberTimeWeek(t.deviceName.Substring(0, 6), t.deviceName, startTime, DateTime.Now);
            //        var runTimelist = _device.GettotalOperationTimeWeek(t.deviceName.Substring(0, 6), t.deviceName, startTime, DateTime.Now);
            //        for (int i = 0; i < runTimelist.Count; i += 2)
            //        {
            //            opertiontricks += TimeSpan.FromTicks(faultNumberlist[i + 1].alarmTime.Ticks - faultNumberlist[i].alarmTime.Ticks);
            //        }
            //        //停机时间
            //        for (int i = 1; i < runTimelist.Count - 1; i += 2)
            //        {
            //            stopRuntricks += TimeSpan.FromTicks(runTimelist[i + 1].alarmTime.Ticks - runTimelist[i].alarmTime.Ticks);
            //        }
            //     //   _DeviceCount.insertDataCount(new Device_DataCountModel { runTimeCount = new DateTime(opertiontricks.Ticks),stopTimeCount= new DateTime(stopRuntricks.Ticks),faultNumberCount = faultNumberlist.Count(), deviceName = t.deviceName, roomName = t.deviceName.Substring(0, 6), week = weekOfMonth.ToString(), month = currentMonth.ToString(), year = currentYear.ToString() });
                   

            //    });
                    
                
                //Debug.WriteLine($"当前时间：{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
                //Debug.WriteLine("Hello!");
               // BLL.insertRunTimeCount(new Device_RunTimeCountModel { CountTime = DateTime.Now, deviceName = "1", roomName = "1", week = "1", month = "1", year = "1" });
               // BLL.insertStopTimeCount(new Device_StopTimeCountModel { CountTime = DateTime.Now, deviceName = "1", roomName = "1", week = "1", month = "1", year = "1" });
               // BLL.insertFaultNumberCount(new device_FaultNumberCountModel { CountNumber = 1, deviceName = "1", roomName = "1", week = "1", month = "1", year = "1" });
            
        }
        static int GetWeekOfMonth(DateTime date)
        {
            Calendar calendar = CultureInfo.InvariantCulture.Calendar;
            return calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Friday) -
                   calendar.GetWeekOfYear(new DateTime(date.Year, date.Month, 1), CalendarWeekRule.FirstDay, DayOfWeek.Friday) + 1;
        }
    }

}
