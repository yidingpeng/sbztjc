using AutoMapper;
using RW.PMS.Application.Contracts.DeviceStatus;
using RW.PMS.Application.Contracts.DTO.DeviceStatus;
using RW.PMS.Application.Contracts.System;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.EventBus;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.DeviceStatus;
using RW.PMS.Domain.Entities.System;
using RW.PMS.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.DeviceStatus
{
    public class DRoomTimeInputService:CrudApplicationService<DRoomJiaoZhunTimeEntity, int>, IDRoomTimeInputService
    {

        private readonly IEventBus _eventBus;
        private readonly ILogService _log;
        private readonly IRepository<ModuleEntity, int> _moduleRepository;
        public DRoomTimeInputService(IDataValidatorProvider dataValidator,
      IRepository<DRoomJiaoZhunTimeEntity, int> roleRepository,
      IMapper mapper,
      Lazy<ICurrentUser> currentUser,
      IRepository<ModuleEntity, int> moduleRepository,
      IEventBus eventBus,
      ILogService log) : base(dataValidator,
      roleRepository, mapper, currentUser)
        {

            _moduleRepository = moduleRepository;
            _eventBus = eventBus;
            _log = log;
        }

        public bool InsertDroomHoliday(DRoomHolidayTimeDto input)
        {
            var entity = Mapper.Map<DRoomHolidayTimeEntity>(input);
            return Repository.Orm.Insert(entity).ExecuteAffrows() > 0;
        }

        public bool InsertDroomjioazhun(DRoomJiaoZhunTimeDto input)
        {
            var entity = Mapper.Map<DRoomJiaoZhunTimeEntity>(input);
            return Repository.Orm.Insert(entity).ExecuteAffrows() > 0;
        }

        public bool InsertDroomweibao(DRoomweibaoTimeDto input)
        {
            var entity = Mapper.Map<DRoomweibaoTimeEntity>(input);
            return Repository.Orm.Insert(entity).ExecuteAffrows() > 0;
        }

        public bool InsertDroomzhidu(DRoomzhiduTimeDto input)
        {
            var entity = Mapper.Map<DRoomzhiduTimeEntity>(input);
            return Repository.Orm.Insert(entity).ExecuteAffrows() > 0;
        }

        public bool updateDroomzhidu(DRoomzhiduTimeDto input)
        {
            var result = Repository.Orm.Update<DRoomzhiduTimeEntity>().Set(a => a.alarmTime, input.alarmTime).Where(a=>a.roomName.Equals(input.roomName))
   .ExecuteAffrows() > 0;
            return result;
        }
    }
}
