using RW.PMS.Application.Contracts.DTO.DeviceFile;
using RW.PMS.Domain.Entities.Basics;
using RW.PMS.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DeviceFile
{
    public interface IDeviceService : ICrudApplicationService<DeviceEntity, int>
    {
        PagedResult<DeviceDto> getdeviceAlllist();
    }
}
