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
    public interface IDeviceStatusService : ICrudApplicationService<DeviceStatusTagNameEntity, int>
    {
        PagedResult<DeviceStatusTagNameDto> GetDeviceStatusNameAllList();
        PagedResult<DeviceStatusDto> GetDeviceNameAllList();
        PagedResult<DRoomTestRoomStatusDto> GetDeviceTestRoomAllList();
        List<DeviceStatusDto> GetDeviceNameConditionList(string roomName);
        bool Insert(DeviceTesSheetDto input);
        PagedResult<DeviceTesSheetDto> getTestSheetAllList(DeviceTesSheetDto input);
        bool Repeatjudgment(DeviceTesSheetDto input);
        bool update(DeviceStatusDto input);
        bool Update(DeviceTesSheetDto input);

        int updateDevicetotalFaultDownTime(string deviceName, int roomid, double totalFaultDownTime);
        int updateDevicetotalFreeTime(string deviceName, int roomid, double totalFreeTime);
        int updateDevicetotalRunTime(string deviceName, int roomid, double totalRunTime);
        int updateDevicetotalStopTime(string deviceName, int roomid, double totalStopTime);
        int updateDevicetotalweibaoTime(string deviceName, int roomid, double weibaoTime);
        List<DeviceStatusDto> getdeviceAlllist();
        int updateIsClear( int id, int isClear);

        List<DeviceStatusDto> getstatusList(string status);
    }
}
