using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Basics;
using RW.PMS.Domain.Entities.Basics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.Basics
{
    public interface IDevicestatusService: ICrudApplicationService<DevicestatusEntity, int>
    {
        PagedResult<DevicestatusDto> GetPagedList(DevicestatusSearchDto input);

        bool Repeatjudgment(DevicestatusSearchDto input);
    }
}
