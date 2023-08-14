using RW.PMS.Application.Contracts.DTO.DeviceStatus;
using RW.PMS.Domain.Entities.DeviceStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DeviceStatus
{
    public interface IDeviceRefreshTimeService : ICrudApplicationService<DeviceTrainningTimeEntity, int>
    {
        List<DeviceRunTimeDto> getRunTimeList(string roomName,String deviceName);
        List<DeviceStopTimeDto> getStopTimeList(string roomName,String deviceName);
        List<DeviceFaultDownTimesDto> getFaultDownTimeList(string roomName,String deviceName);
        List<DeviceweibaoTimeDto> getweibaoTimeList(string roomName,String deviceName);
        List<DeviceHolidayTimeDto> getHolidayTimeList(string roomName,String deviceName);
        List<DeviceTrainningTimeDto> getTrainningTimeList(string roomName,String deviceName);
    }
}
