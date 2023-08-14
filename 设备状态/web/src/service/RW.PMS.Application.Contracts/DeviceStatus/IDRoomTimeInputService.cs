using RW.PMS.Application.Contracts.DTO.DeviceStatus;
using RW.PMS.Domain.Entities.Base;
using RW.PMS.Domain.Entities.DeviceStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DeviceStatus
{
    public interface IDRoomTimeInputService : ICrudApplicationService<DRoomJiaoZhunTimeEntity, int>
    {
        bool InsertDroomjioazhun(DRoomJiaoZhunTimeDto input);
        bool InsertDroomHoliday(DRoomHolidayTimeDto input);
        bool InsertDroomweibao(DRoomweibaoTimeDto input);
        bool InsertDroomzhidu(DRoomzhiduTimeDto input);
        bool updateDroomzhidu(DRoomzhiduTimeDto input);
    }
}
