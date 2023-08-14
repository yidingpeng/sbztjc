using RW.PMS.Application.Contracts.DTO.DeviceStatus;
using RW.PMS.Domain.Entities.DeviceStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DeviceStatus
{
    public interface IDRoomStatusService : ICrudApplicationService<DRoomTestRoomStatusEntity, int>
    {
        int updateDRoomtotalEffectiveRunningTime(string roomName, double totalEffectiveRunningTime);
        int updateDRoomtotalTestStopTime(string roomName, double totalTestStopTime);
        int updateDRoomtotalFaultTime(string roomName, double totalFaultTime);
        int updateDRoomtotalStandbyTime(string roomName, double totalStandbyTime);
        int updateDRoomtotalFreeTime(string roomName, double totalFreeTime);
        int updateDRoomtotalweibaoTime(string roomName, double totalweibaoTime);
        int updateDRoomDeviceDebugRunTime(string roomName, double totalDeviceDebugRunTime);
        int updateDRoomtotalUtilizationRate(string roomName, double totalUtilizationRate);
        List<DRoomTestRoomStatusDto> getRoomAlllist();
        List<DRoomTestRoomStatusDto> getRoomName(int roomId);
        int updateDRoomIsClear(int id, int isCLear);

        List<DRoomTestRoomStatusDto> getstatusList(string status);
    }
}
