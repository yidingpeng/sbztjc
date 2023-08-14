using AutoMapper;
using RW.PMS.Application.Contracts.DeviceStatus;
using RW.PMS.Application.Contracts.DTO;
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
    public class DroomGetTimesService : CrudApplicationService<DRoomzhiduTimeEntity, int>, IDroomGetTimesService
    {
        private readonly IEventBus _eventBus;
        private readonly ILogService _log;
        private readonly IRepository<ModuleEntity, int> _moduleRepository;
        public DroomGetTimesService(IDataValidatorProvider dataValidator,
      IRepository<DRoomzhiduTimeEntity, int> roleRepository,
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

        public PagedResult<DRoomzhiduTimeDto> getzhiduTimeList()
        {

            var list = Repository.Orm.Select<DRoomzhiduTimeEntity>().ToList().Select(t => Mapper.Map<DRoomzhiduTimeDto>(t)).ToList();
            return new PagedResult<DRoomzhiduTimeDto>(list, list.Count);
        }
    }
}
