using Quartz;
using RW.PMS.Application.Contracts.DeviceFile;
using RW.PMS.Application.Contracts.DTO.DeviceFile;
using RW.PMS.CrossCutting.Extender;
using System.Diagnostics;
using System.Globalization;

namespace RW.PMS.API.QuartzService
{
    public class TestJobweek1 : IJob
    {
        private readonly ITestSheetService _testSheetService;
        private readonly ITestRoomService _testRoomService;
        private readonly IConfiguration _configuration;
        private readonly ITestSheetTimeCountService _testSheetTimeCountService;
        public TestJobweek1(ITestSheetService testSheetService,ITestRoomService testRoomService, IConfiguration configuration, ITestSheetTimeCountService testSheetTimeCountService)
        {
            _testSheetService = testSheetService;
            _testRoomService = testRoomService;
            _configuration = configuration;
            _testSheetTimeCountService = testSheetTimeCountService;
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
        public void roomcount(DateTime startTime, DateTime endTime, int week, int month, int year)
        {
         var  roomlist=  _testRoomService.getroomAlllist().List.ToList();
            roomlist.ForEach(t => {
                double totalorderTypeTime = 0;
                double totaloperationTestTime = 0;
                double totalabnormalTime = 0;
                double totalwaitMaterialTime = 0;
                double totalwaitManageTime = 0;
                int totaltestNumber = 0;
             var roomOnetime=   _testSheetTimeCountService.getTestSheetTimeList(t.roomName, startTime, endTime);

              var testMuberlist = roomOnetime.Where((x, i) => roomOnetime.FindIndex(n => n.testNumber == x.testNumber) == i).ToList();//去重
                totaltestNumber= testMuberlist.Count;
                roomOnetime.ForEach(x => {
                    totalorderTypeTime += x.orderTypeTime;
                    totaloperationTestTime += x.operationTestTime;
                    totalabnormalTime += x.abnormalTime;
                    totalwaitMaterialTime += x.waitMaterialTime;
                    totalwaitManageTime+= x.waitManageTime;


                });
                _testSheetTimeCountService.InsertweekCountTime(new DeviceTestSheetWeekCountDto() { 
                testName=t.roomName,testNumber=totaltestNumber,orderTypeTime=totalorderTypeTime,operationTestTime=totaloperationTestTime,abnormalTime=totalabnormalTime,
                    waitMaterialTime=totalwaitMaterialTime,waitManageTime=totalwaitManageTime,weeks=week,months=month,years=year
                });
            });
        }
    }
}
