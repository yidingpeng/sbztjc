using iTextSharp.text.pdf.parser.clipper;
using Quartz;
using RW.PMS.Application.Contracts.DeviceFile;
using RW.PMS.Application.Contracts.DTO.DeviceFile;

namespace RW.PMS.API.QuartzService
{
    public class TestJobmonth2 : IJob
    {
        private readonly ITestSheetService _testSheetService;
        private readonly ITestRoomService _testRoomService;
        private readonly ITestSheetTimeCountService _testSheetTimeCountService;
        public TestJobmonth2(ITestSheetService testSheetService, ITestRoomService testRoomService, ITestSheetTimeCountService testSheetTimeCountService)
        {
            _testSheetService = testSheetService;
            _testRoomService = testRoomService;
            _testSheetTimeCountService = testSheetTimeCountService;
        }
        public Task Execute(IJobExecutionContext context)
        {
            
            DateTime endTime = DateTime.Now;
            //DateTime thisMonthStart = new DateTime(startTime.Year, startTime.Month, 1);
            DateTime startTime = endTime.AddMonths(-1).Date;
            int month = startTime.Month;//获取月
            int year = startTime.Year;//获取年
            roomcount(month, year);
            return Task.CompletedTask;
        }
        public void roomcount(int month, int year)
        {
            var roomlist = _testRoomService.getroomAlllist().List.ToList();
            roomlist.ForEach(t =>
            {
                double totalorderTypeTime = 0;
                double totaloperationTestTime = 0;
                double totalabnormalTime = 0;
                double totalwaitMaterialTime = 0;
                double totalwaitManageTime = 0;
                int totaltestNumber = 0;
                var roomOneweektime = _testSheetTimeCountService.getweekcount(year,month,t.roomName);
                roomOneweektime.ForEach(x => {
                    totalorderTypeTime += x.orderTypeTime;
                    totaloperationTestTime+= x.operationTestTime;
                    totalabnormalTime+= x.abnormalTime;
                    totalwaitMaterialTime+= x.waitMaterialTime;
                    totalwaitManageTime+= x.waitManageTime;
                    totaltestNumber+= x.testNumber;
                });
                _testSheetTimeCountService.InsertMonthCountTime(new DeviceTestSheetMonthCountDto() { 
                  testName= t.roomName,testNumber=totaltestNumber,orderTypeTime=totalorderTypeTime,operationTestTime=totaloperationTestTime,abnormalTime=totalabnormalTime,
                  waitMaterialTime=totalwaitMaterialTime, waitManageTime=totalwaitManageTime,months=month,years=year
                });
            });
        }
    }
}
