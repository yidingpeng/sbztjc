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
    public class DeviceRefreshTimeService : CrudApplicationService<DeviceTrainningTimeEntity, int>, IDeviceRefreshTimeService
    {
        private readonly IEventBus _eventBus;
        private readonly ILogService _log;
        private readonly IRepository<ModuleEntity, int> _moduleRepository;
        public DeviceRefreshTimeService(IDataValidatorProvider dataValidator,
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

        public List<DeviceFaultDownTimesDto> getFaultDownTimeList(string roomName, string deviceName)
        {
            var list = Repository.Orm.Select<DeviceFaultDownTimesEntity>().Where(t => t.roomName.Contains(roomName) && t.deviceName.Equals(deviceName)).ToList().Select(t => Mapper.Map<DeviceFaultDownTimesDto>(t)).ToList();
            return list;
        }

        public List<DeviceHolidayTimeDto> getHolidayTimeList(string roomName, string deviceName)
        {
            var list = Repository.Orm.Select<DeviceHolidayTimeEntity>().Where(t => t.roomName.Contains(roomName) && t.deviceName.Equals(deviceName)).ToList().Select(t => Mapper.Map<DeviceHolidayTimeDto>(t)).ToList();
            return list;
        }

        public List<DeviceRunTimeDto> getRunTimeList(string roomName, string deviceName)
        {
            var list = Repository.Orm.Select<DeviceRunTimeEntity>().Where(t => t.roomName.Contains(roomName) && t.deviceName.Equals(deviceName)).ToList().Select(t => Mapper.Map<DeviceRunTimeDto>(t)).ToList();
            return list;
        }

        public List<DeviceStopTimeDto> getStopTimeList(string roomName, string deviceName)
        {
            var list = Repository.Orm.Select<DeviceStopTimeEntity>().Where(t => t.roomName.Contains(roomName) && t.deviceName.Equals(deviceName)).ToList().Select(t => Mapper.Map<DeviceStopTimeDto>(t)).ToList();
            return list;
        }

        public List<DeviceTrainningTimeDto> getTrainningTimeList(string roomName, string deviceName)
        {
            var list = Repository.Orm.Select<DeviceTrainningTimeEntity>().Where(t => t.roomName.Contains(roomName) && t.deviceName.Equals(deviceName)).ToList().Select(t => Mapper.Map<DeviceTrainningTimeDto>(t)).ToList();
            return list;
        }

        public List<DeviceweibaoTimeDto> getweibaoTimeList(string roomName, string deviceName)
        {
            var list = Repository.Orm.Select<DeviceweibaoTimeEntity>().Where(t => t.roomName.Contains(roomName)&&t.deviceName.Equals(deviceName)).ToList().Select(t => Mapper.Map<DeviceweibaoTimeDto>(t)).ToList();
            return list;
        }
    }
}
