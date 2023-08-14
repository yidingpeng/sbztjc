using iTextSharp.text.pdf.parser.clipper;
using Quartz;
using RW.PMS.Application.Contracts.DeviceFile;
using RW.PMS.Application.Contracts.DTO.DeviceFile;
using RW.PMS.Application.DeviceFile;

namespace RW.PMS.API.QuartzService
{
    public class TestJobYear3 : IJob
    {
        private readonly ITestSheetService _testSheetService;
        private readonly ITestRoomService _testRoomService;
        private readonly ITestSheetTimeCountService _testSheetTimeCountService;

        public TestJobYear3(ITestSheetService testSheetService, ITestRoomService testRoomService, ITestSheetTimeCountService testSheetTimeCountService)
        {
            _testSheetService = testSheetService;
            _testRoomService = testRoomService;
            _testSheetTimeCountService = testSheetTimeCountService;
        }
        public Task Execute(IJobExecutionContext context)
        {
            DateTime endTime = DateTime.Now;
            DateTime startTime = endTime.AddYears(-1).Date;
            int year = startTime.Year;//获取年
            roomcount(year);
            return Task.CompletedTask;
        }
        public void roomcount(int year)
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
                var roomOneMonthtime = _testSheetTimeCountService.GetMonthCount(year, t.roomName);
                roomOneMonthtime.ForEach(x => {
                    totalorderTypeTime += x.orderTypeTime;
                    totaloperationTestTime += x.operationTestTime;
                    totalabnormalTime += x.abnormalTime;
                    totalwaitMaterialTime += x.waitMaterialTime;
                    totalwaitManageTime += x.waitManageTime;
                    totaltestNumber += x.testNumber;
                });
                _testSheetTimeCountService.InsertYearCountTime(new DeviceTestSheetYearCountDto()
                {
                    testName = t.roomName,
                    testNumber = totaltestNumber,
                    orderTypeTime = totalorderTypeTime,
                    operationTestTime = totaloperationTestTime,
                    abnormalTime = totalabnormalTime,
                    waitMaterialTime = totalwaitMaterialTime,
                    waitManageTime = totalwaitManageTime,
                    years = year
                });
            });
        }
    }
}
