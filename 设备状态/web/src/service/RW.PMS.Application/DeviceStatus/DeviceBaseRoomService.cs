
using AutoMapper;
using RW.PMS.Application.Contracts.DeviceStatus;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.DeviceStatus;
using RW.PMS.Application.Contracts.System;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.EventBus;
using RW.PMS.CrossCutting.Extensions;
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
    public class DeviceBaseRoomService : CrudApplicationService<DRoomTestRoomStatusEntity, int>, IDeviceBaseRoomService
    {
        private readonly IEventBus _eventBus;
        private readonly ILogService _log;
        private readonly IRepository<ModuleEntity, int> _moduleRepository;
        public DeviceBaseRoomService(IDataValidatorProvider dataValidator,
      IRepository<DRoomTestRoomStatusEntity, int> roleRepository,
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

        public PagedResult<DRoomTestRoomStatusDto> getList(DRoomTestRoomStatusDto input)
        {

            var list = Repository.WhereIf(input.roomName.NotNullOrWhiteSpace(), t => t.roomName.Contains(input.roomName)).Count(out var total).Page(input.PageNo, input.PageSize).OrderByDescending(t => t.Id).ToList()
        .Select(t => Mapper.Map<DRoomTestRoomStatusDto>(t)).ToList();
            return new PagedResult<DRoomTestRoomStatusDto>(list, total);
        }
        public PagedResult<DRoomTestRoomStatusDto> getAllList(DRoomTestRoomStatusDto input)
        {

            var list = Repository.Select.Count(out var total).OrderBy(t => t.Id).ToList()
        .Select(t => Mapper.Map<DRoomTestRoomStatusDto>(t)).ToList();
            return new PagedResult<DRoomTestRoomStatusDto>(list, total);
        }
        public DRoomTestRoomStatusEntity Insert(DRoomTestRoomStatusDto input)
        {
            var result = base.Insert(input);
            return result;
        }

        public bool Update(DRoomTestRoomStatusDto input)
        {
            var result = base.Update(input.id, input) > 0;
            return result;
        }
    }
    
}
