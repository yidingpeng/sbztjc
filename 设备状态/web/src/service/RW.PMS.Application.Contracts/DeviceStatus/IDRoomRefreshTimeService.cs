using RW.PMS.Application.Contracts.DTO.DeviceStatus;
using RW.PMS.Domain.Entities.DeviceStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DeviceStatus
{
    public interface IDRoomRefreshTimeService : ICrudApplicationService<DRoomEffectiveRunningTimeEntity, int>
    {
        List<DRoomEffectiveRunningTimeDto> getEffectiveRunningTimeList(string roomName);
        List<DRoomTestStopTimeDto> getTestStopTimeList(string roomName);
        List<DRoomFaultTimeDto> getFaultTimeTimeList(string roomName);
        List<DRoomStandbyTimeDto> getStandbyTimeList(string roomName);
        List<DRoomDeviceDebugRunTimeDto> getDeviceDebugRunTimeList(string roomName);
        List<DRoomHolidayTimeDto> getHolidayTimeList(string roomName);
        List<DRoomweibaoTimeDto> getweibaoTimeList(string roomName);
        List<DRoomJiaoZhunTimeDto> getJiaoZhunTimeList(string roomName);
        List<DRoomzhiduTimeDto> getZhiDuTimeList(string roomName);
    }
}
