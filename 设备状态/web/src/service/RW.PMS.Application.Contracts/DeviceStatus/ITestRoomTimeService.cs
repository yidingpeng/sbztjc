using RW.PMS.Application.Contracts.DTO.DeviceStatus;
using RW.PMS.Domain.Entities.DeviceStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DeviceStatus
{
    public interface ITestRoomTimeService : ICrudApplicationService<TestRoomweibaoTimeEntity, int>
    {
        bool Insertweibao(TestRoomTimesDto input);
        bool Insertzhidu(TestRoomTimesDto input);
        bool Insertholiday(TestRoomTimesDto input);
    }
}
