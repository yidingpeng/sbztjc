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
    public interface IDeviceDataCountService : ICrudApplicationService<DeviceTrainningTimeEntity, int>
    {
        List<DeviceRunTimeDto> getRunTimeList(string roomName, String deviceName, DateTime startTime, DateTime endTime);
        List<DeviceStopTimeDto> getStopTimeList(string roomName, String deviceName, DateTime startTime, DateTime endTime);
        List<DeviceFaultDownTimesDto> getFaultDownTimeList(string roomName, String deviceName ,DateTime startTime, DateTime endTime);
        List<DeviceWeiBaoTimeOnOffDto> getweibaoTimeList(string roomName, String deviceName, DateTime startTime, DateTime endTime);
        List<DeviceFaultNumberTimeDto> getFaultNumberList(string roomName, String deviceName, DateTime startTime, DateTime endTime);
        int InsertRunTimeTime(DeviceRunTimeDto input);
        int InsertStopTimeTime(DeviceStopTimeDto input);
        int InsertFaultDownTime(DeviceFaultDownTimesDto input);
        int InsertweibaoTime(DeviceWeiBaoTimeOnOffDto input);
        int InsertweekCountTime(DeviceWeekCountDto input);
        int InsertMonthCountTime(DeviceMonthCountDto input);
        int InsertYearCountTime(DeviceYearCountDto input);

        List<DeviceWeekCountDto> getweekcount(int years,int months,string roomName, string deviceName);
        List<DeviceMonthCountDto> GetMonthCount(int years, string roomName, string deviceName);

        PagedResult<DeviceWeekCountDto> getweekcountvue(DataCountNYRDto input);
        PagedResult<DeviceMonthCountDto> GetMonthCountvue(DataCountNYRDto input);
        PagedResult<DeviceYearCountDto> GetyearCountvue(DataCountNYRDto input);

        List<DeviceWeekCountDto> getweekcountvueimport(DataCountNYRDto input);
        List<DeviceMonthCountDto> GetMonthCountvueimport(DataCountNYRDto input);
        List<DeviceYearCountDto> GetyearCountvueimport(DataCountNYRDto input);
    }
}
