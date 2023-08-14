using RW.PMS.Application.Contracts.DTO.DeviceStatus;
using RW.PMS.Domain.Entities.DeviceStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DeviceStatus
{
    public interface IDRoomWeiBaoOnOffService: ICrudApplicationService<DRoomWeiBaoTimeOnOffEntity, int>
    {
        int InsertTime(DRoomWeiBaoTimeOnOffDto input);
        List<DRoomWeiBaoTimeOnOffDto> getweibaoTimeList(string roomName, string deviceName);
    }
}
