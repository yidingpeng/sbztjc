
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
    public class DeviceFaultTimesService : CrudApplicationService<DeviceFaultDownTimesEntity, int>, IDeviceFaultTimesService
    {
        private readonly IEventBus _eventBus;
        private readonly ILogService _log;
        private readonly IRepository<ModuleEntity, int> _moduleRepository;
        public DeviceFaultTimesService(IDataValidatorProvider dataValidator,
      IRepository<DeviceFaultDownTimesEntity, int> roleRepository,
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

        public DeviceFaultDownTimesDto getFirstList(DeviceFaultDownTimesDto input)
        {
            
          
          var list=  Repository.Select
           .Where(u =>  u.roomName.Contains( input.roomName) && u.onOrOff .Equals( input.onOrOff))
           .OrderByDescending(u => u.Id).Count(out var total).ToList().Select(t => Mapper.Map<DeviceFaultDownTimesDto>(t)).ToList()
           .FirstOrDefault();
            return list;
        }

        public bool Update(DeviceFaultDownTimesDto input)
        {
            var result = Repository.Orm.Update<DeviceFaultDownTimesEntity>(input.id).Set(a=>a.deviceName,input.deviceName)
  .ExecuteAffrows()>0;
            return result;
        }
    }
}
