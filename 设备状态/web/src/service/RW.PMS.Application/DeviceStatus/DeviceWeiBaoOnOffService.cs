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
    public class DeviceWeiBaoOnOffService : CrudApplicationService<DeviceWeiBaoTimeOnOffEntity, int>, IDeviceWeiBaoOnOffService
    {
        private readonly IEventBus _eventBus;
        private readonly ILogService _log;
        private readonly IRepository<ModuleEntity, int> _moduleRepository;
        public DeviceWeiBaoOnOffService(IDataValidatorProvider dataValidator,
      IRepository<DeviceWeiBaoTimeOnOffEntity, int> roleRepository,
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

        public List<DeviceWeiBaoTimeOnOffDto> getweibaoTimeList(string roomName, string deviceName)
        {
            var list = Repository.Orm.Select<DeviceWeiBaoTimeOnOffEntity>().Where(t => t.roomName.Contains(roomName) && t.deviceName.Equals(deviceName)).ToList().Select(t => Mapper.Map<DeviceWeiBaoTimeOnOffDto>(t)).ToList();
            return list;
        }

        public int InsertTime(DeviceWeiBaoTimeOnOffDto input)
        {
            var entity = Mapper.Map<DeviceWeiBaoTimeOnOffEntity>(input);
            
            return ((int)Repository.Orm.Insert<DeviceWeiBaoTimeOnOffEntity>(entity).ExecuteIdentity());
        }
    }
}
