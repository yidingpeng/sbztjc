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
    public  interface  IDeviceBaseRoomService : ICrudApplicationService<DRoomTestRoomStatusEntity, int>
    {
        PagedResult<DRoomTestRoomStatusDto> getList(DRoomTestRoomStatusDto input);
        PagedResult<DRoomTestRoomStatusDto> getAllList(DRoomTestRoomStatusDto input);
        bool Update(DRoomTestRoomStatusDto input);
        DRoomTestRoomStatusEntity Insert(DRoomTestRoomStatusDto input);
    }
}
