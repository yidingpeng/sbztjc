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
    public class DeviceBaseDeviceService : CrudApplicationService<DeviceStatusEntity, int>, IDeviceBaseDeviceService
    {
        private readonly IEventBus _eventBus;
        private readonly ILogService _log;
        private readonly IRepository<ModuleEntity, int> _moduleRepository;
        public DeviceBaseDeviceService(IDataValidatorProvider dataValidator,
      IRepository<DeviceStatusEntity, int> roleRepository,
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
        public PagedResult<DeviceandRoomDto> getList(DeviceStatusDto input)
        {

            var list = Repository.Orm.Select<DRoomTestRoomStatusEntity, DeviceStatusEntity>().LeftJoin(x => x.t1.Id == x.t2.roomId).WhereIf(input.deviceName.NotNullOrWhiteSpace(), x => x.t2.deviceName.Contains(input.deviceName)).Count(out var total).Page(input.PageNo, input.PageSize).OrderByDescending(t => t.t2.Id).ToList(x => new { x.t1, x.t2 })
        .Select(x => new DeviceandRoomDto
        {
            id = x.t2.Id,
            roomName = x.t1.roomName,
            deviceName = x.t2.deviceName,
            roomid=x.t1.Id
        }).ToList();

            return new PagedResult<DeviceandRoomDto>(list, total);
        }
        public DeviceStatusEntity Insert(DeviceStatusDto input)
        {
            var result = base.Insert(input);
            return result;
        }

        public bool Update(DeviceStatusDto input)
        {
            var result = base.Update(input.id, input) > 0;
            return result;
        }
    }
}
