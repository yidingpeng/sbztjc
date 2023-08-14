using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.DeviceStatus;
using RW.PMS.Domain.Entities.DeviceStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DeviceStatus
{
    public interface IDRoomDataCountService : ICrudApplicationService<DRoomEffectiveRunningTimeEntity, int>
    {
        List<DRoomEffectiveRunningTimeDto> getEffectiveRunningTimeList(string roomName,DateTime startTime,DateTime endTime);
        List<DRoomTestStopTimeDto> getTestStopTimeList(string roomName, DateTime startTime, DateTime endTime);
        List<DRoomFaultTimeDto> getFaultTimeTimeList(string roomName, DateTime startTime, DateTime endTime);
        List<DRoomStandbyTimeDto> getStandbyTimeList(string roomName, DateTime startTime, DateTime endTime);
        List<DRoomDeviceDebugRunTimeDto> getDeviceDebugRunTimeList(string roomName, DateTime startTime, DateTime endTime);
        List<DRoomWeiBaoTimeOnOffDto> getweibaoTimeList(string roomName, DateTime startTime, DateTime endTime);
        DRoomzhiduTimeDto getZhiDuTimeList(string roomName);
        int InsertEffectiveRunningTime(DRoomEffectiveRunningTimeDto input);
        int InsertTestStopTime(DRoomTestStopTimeDto input);
        int InsertFaultTime(DRoomFaultTimeDto input);
        int InsertStandbyTime(DRoomStandbyTimeDto input);
        int InsertDeviceDebugRunTime(DRoomDeviceDebugRunTimeDto input);
        int InsertweibaoTime(DRoomWeiBaoTimeOnOffDto input);
        int InsertweekCountTime(DRoomWeekCountDto input);
        int InsertMonthCountTime(DRoomMonthCountDto input);
        int InsertYearCountTime(DRoomYearCountDto input);

        List<DRoomWeekCountDto> getweekcount(int years, int months, string roomName);
        List<DRoomMonthCountDto> GetMonthCount(int years, string roomName);

        PagedResult<DRoomWeekCountDto> getweekcountvue(DataCountNYRDto input);
        PagedResult<DRoomMonthCountDto> GetMonthCountvue(DataCountNYRDto input);
        PagedResult<DRoomYearCountDto> GetyearCountvue(DataCountNYRDto input);

        List<DRoomWeekCountDto> getweekcountvueimport(DataCountNYRDto input);
        List<DRoomMonthCountDto> GetMonthCountvueimport(DataCountNYRDto input);
        List<DRoomYearCountDto> GetyearCountvueimport(DataCountNYRDto input);
    }
}
