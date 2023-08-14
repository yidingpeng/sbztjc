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
    public class DRoomRefreshTimeService : CrudApplicationService<DRoomEffectiveRunningTimeEntity, int>, IDRoomRefreshTimeService
    {
        private readonly IEventBus _eventBus;
        private readonly ILogService _log;
        private readonly IRepository<ModuleEntity, int> _moduleRepository;
        public DRoomRefreshTimeService(IDataValidatorProvider dataValidator,
      IRepository<DRoomEffectiveRunningTimeEntity, int> roleRepository,
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

        public List<DRoomDeviceDebugRunTimeDto> getDeviceDebugRunTimeList(string roomName)
        {
            var list = Repository.Orm.Select<DRoomDeviceDebugRunTimeEntity>().Where(t => t.roomName.Contains(roomName)).ToList().Select(t => Mapper.Map<DRoomDeviceDebugRunTimeDto>(t)).ToList();
            return list;
        }

        public List<DRoomEffectiveRunningTimeDto> getEffectiveRunningTimeList(string roomName)
        {
            var list = Repository.Orm.Select<DRoomEffectiveRunningTimeEntity>().Where(t=>t.roomName.Contains(roomName)).ToList().Select(t => Mapper.Map<DRoomEffectiveRunningTimeDto>(t)).ToList();
            return list;
        }

        public List<DRoomFaultTimeDto> getFaultTimeTimeList(string roomName)
        {
            var list = Repository.Orm.Select<DRoomFaultTimeEntity>().Where(t => t.roomName.Contains(roomName)).ToList().Select(t => Mapper.Map<DRoomFaultTimeDto>(t)).ToList();
            return list;
        }

        public List<DRoomHolidayTimeDto> getHolidayTimeList(string roomName)
        {
            var list = Repository.Orm.Select<DRoomHolidayTimeEntity>().Where(t => t.roomName.Contains(roomName)).ToList().Select(t => Mapper.Map<DRoomHolidayTimeDto>(t)).ToList();
            return list;
        }

        public List<DRoomJiaoZhunTimeDto> getJiaoZhunTimeList(string roomName)
        {
            var list = Repository.Orm.Select<DRoomJiaoZhunTimeEntity>().Where(t => t.roomName.Contains(roomName)).ToList().Select(t => Mapper.Map<DRoomJiaoZhunTimeDto>(t)).ToList();
            return list;
        }
            public List<DRoomStandbyTimeDto> getStandbyTimeList(string roomName)
            {
                var list = Repository.Orm.Select<DRoomStandbyTimeEntity>().Where(t => t.roomName.Contains(roomName)).ToList().Select(t => Mapper.Map<DRoomStandbyTimeDto>(t)).ToList();
                return list;
            }

        public List<DRoomTestStopTimeDto> getTestStopTimeList(string roomName)
        {
            var list = Repository.Orm.Select<DRoomTestStopTimeEntity>().Where(t => t.roomName.Contains(roomName)).ToList().Select(t => Mapper.Map<DRoomTestStopTimeDto>(t)).ToList();
            return list;
        }

        public List<DRoomweibaoTimeDto> getweibaoTimeList(string roomName)
        {
            var list = Repository.Orm.Select<DRoomweibaoTimeEntity>().Where(t => t.roomName.Contains(roomName)).ToList().Select(t => Mapper.Map<DRoomweibaoTimeDto>(t)).ToList();
            return list;
        }

        public List<DRoomzhiduTimeDto> getZhiDuTimeList(string roomName)
        {
            var list = Repository.Orm.Select<DRoomzhiduTimeEntity>().Where(t => t.roomName.Contains(roomName)).ToList().Select(t => Mapper.Map<DRoomzhiduTimeDto>(t)).ToList();
            return list;
        }
    }
}
