using RW.PMS.Application.Contracts.DTO.DeviceFile;
using RW.PMS.Domain.Entities.Common;
using RW.PMS.Domain.Entities.DeviceFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DeviceFile
{
    public interface ITestRoomService : ICrudApplicationService<TestRoomEntity, int>
    {
        PagedResult<TestRoomDto> getroomAlllist();
    }
}
