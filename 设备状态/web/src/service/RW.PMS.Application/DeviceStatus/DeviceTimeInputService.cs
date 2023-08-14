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
    public class DeviceTimeInputService:CrudApplicationService<DeviceTrainningTimeEntity, int>, IDeviceTimeInputService
    {
        private readonly IEventBus _eventBus;
        private readonly ILogService _log;
        private readonly IRepository<ModuleEntity, int> _moduleRepository;
        public DeviceTimeInputService(IDataValidatorProvider dataValidator,
      IRepository<DeviceTrainningTimeEntity, int> roleRepository,
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

        public bool InsertDeviceHoliday(DeviceHolidayTimeDto input)
        {
            var entity = Mapper.Map<DeviceHolidayTimeEntity>(input);
            return Repository.Orm.Insert(entity).ExecuteAffrows()>0;
        }

        public bool InsertDeviceTranning(DeviceTrainningTimeDto input)
        {
            var entity = Mapper.Map<DeviceTrainningTimeEntity>(input);
            return Repository.Orm.Insert(entity).ExecuteAffrows() > 0;
        }

        public bool InsertDeviceweibao(DeviceweibaoTimeDto input)
        {
            var entity = Mapper.Map<DeviceweibaoTimeEntity>(input);
            return Repository.Orm.Insert(entity).ExecuteAffrows() > 0;
        }
    }
}
