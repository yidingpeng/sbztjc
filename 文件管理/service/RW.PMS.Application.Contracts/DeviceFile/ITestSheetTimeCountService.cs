using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.DeviceFile;
using RW.PMS.Domain.Entities.DeviceFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DeviceFile
{
    public interface ITestSheetTimeCountService : ICrudApplicationService<DeviceTestSheetTimeCountEntity, int>
    {
        int insertTestSheet(DeviceTestSheetTimeCountDto input);
        List<DeviceTestSheetTimeCountDto> getTestSheetTimeList(string testName,DateTime startTime,DateTime endTime);

        int InsertweekCountTime(DeviceTestSheetWeekCountDto input);
        int InsertMonthCountTime(DeviceTestSheetMonthCountDto input);
        int InsertYearCountTime(DeviceTestSheetYearCountDto input);
        List<DeviceTestSheetWeekCountDto> getweekcount(int years, int months, string roomName);
        List<DeviceTestSheetMonthCountDto> GetMonthCount(int years, string roomName);

        List<DeviceTestSheetWeekCountDto> getweekcountvueimport(DataCountNYRDto input);
        List<DeviceTestSheetMonthCountDto> GetMonthCountvueimport(DataCountNYRDto input);
        List<DeviceTestSheetYearCountDto> GetyearCountvueimport(DataCountNYRDto input);

        PagedResult<DeviceTestSheetWeekCountDto> getweekcountvue(DataCountNYRDto input);
        PagedResult<DeviceTestSheetMonthCountDto> GetMonthCountvue(DataCountNYRDto input);
        PagedResult<DeviceTestSheetYearCountDto> GetyearCountvue(DataCountNYRDto input);
    }
}
