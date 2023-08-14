using RW.PMS.Application.Contracts.DTO.DeviceStatus;
using RW.PMS.Domain.Entities.DeviceStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DeviceStatus
{
    public interface IDeviceWeiBaoOnOffService : ICrudApplicationService<DeviceWeiBaoTimeOnOffEntity, int>
    {
        public int InsertTime(DeviceWeiBaoTimeOnOffDto input);
        List<DeviceWeiBaoTimeOnOffDto> getweibaoTimeList(string roomName, String deviceName);
    }
}
