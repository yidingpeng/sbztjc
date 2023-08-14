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
    public interface IDeviceBaseDeviceService : ICrudApplicationService<DeviceStatusEntity, int>
    {
        PagedResult<DeviceandRoomDto> getList(DeviceStatusDto input);
        bool Update(DeviceStatusDto input);
        DeviceStatusEntity Insert(DeviceStatusDto input);
    }
}
