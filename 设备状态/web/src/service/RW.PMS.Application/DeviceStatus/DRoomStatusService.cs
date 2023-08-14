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
    public class DRoomStatusService : CrudApplicationService<DRoomTestRoomStatusEntity, int>, IDRoomStatusService
    {
        private readonly IEventBus _eventBus;
        private readonly ILogService _log;
        private readonly IRepository<ModuleEntity, int> _moduleRepository;
        public DRoomStatusService(IDataValidatorProvider dataValidator,
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

        public int updateDRoomtotalEffectiveRunningTime(string roomName, double totalEffectiveRunningTime)
        {
            var result = Repository.Orm.Update<DRoomTestRoomStatusEntity>().Set(a => a.totalEffectiveRunningTime, totalEffectiveRunningTime).Where(a => a.roomName.Contains(roomName) )
 .ExecuteAffrows();
            return result;
        }

        public int updateDRoomtotalFaultTime(string roomName, double totalFaultTime)
        {
            var result = Repository.Orm.Update<DRoomTestRoomStatusEntity>().Set(a => a.totalFaultTime, totalFaultTime).Where(a => a.roomName.Contains(roomName))
   .ExecuteAffrows();
            return result;
        }

        public int updateDRoomtotalFreeTime(string roomName, double totalFreeTime)
        {
            var result = Repository.Orm.Update<DRoomTestRoomStatusEntity>().Set(a => a.totalFreeTime, totalFreeTime).Where(a => a.roomName.Contains(roomName))
  .ExecuteAffrows();
            return result;
        }

        public int updateDRoomtotalStandbyTime(string roomName, double totalStandbyTime)
        {
            var result = Repository.Orm.Update<DRoomTestRoomStatusEntity>().Set(a => a.totalStandbyTime, totalStandbyTime).Where(a => a.roomName.Contains(roomName))
 .ExecuteAffrows();
            return result;
        }

        public int updateDRoomtotalTestStopTime(string roomName, double totalTestStopTime)
        {
            var result = Repository.Orm.Update<DRoomTestRoomStatusEntity>().Set(a => a.totalTestStopTime, totalTestStopTime).Where(a => a.roomName.Contains(roomName))
   .ExecuteAffrows();
            return result;
        }

        public int updateDRoomtotalUtilizationRate(string roomName, double totalUtilizationRate)
        {
            var result = Repository.Orm.Update<DRoomTestRoomStatusEntity>().Set(a => a.totalUtilizationRate, totalUtilizationRate).Where(a => a.roomName.Contains(roomName))
   .ExecuteAffrows();
            return result;
        }

        public int updateDRoomtotalweibaoTime(string roomName, double totalweibaoTime)
        {
            var result = Repository.Orm.Update<DRoomTestRoomStatusEntity>().Set(a => a.totalweibaoTime, totalweibaoTime).Where(a => a.roomName.Contains(roomName))
 .ExecuteAffrows();
            return result;
        }
        public List<DRoomTestRoomStatusDto> getRoomName(int roomId)
        {
            var list = Repository.Orm.Select<DRoomTestRoomStatusEntity>().Where(t =>  t.Id==roomId).ToList().Select(t => Mapper.Map<DRoomTestRoomStatusDto>(t)).ToList();
            return list;
        }
        public List<DRoomTestRoomStatusDto> getRoomAlllist()
        {
            var list = Repository.Orm.Select<DRoomTestRoomStatusEntity>().ToList().Select(t => Mapper.Map<DRoomTestRoomStatusDto>(t)).ToList();
            return list;
        }
        public int updateDRoomIsClear(int id, int isCLear)
        {
            var result = Repository.Orm.Update<DRoomTestRoomStatusEntity>().Set(a => a.isClear, isCLear).Where(a => a.Id==id)
 .ExecuteAffrows();
            return result;
        }

        public int updateDRoomDeviceDebugRunTime(string roomName, double totalDeviceDebugRunTime)
        {
            var result = Repository.Orm.Update<DRoomTestRoomStatusEntity>().Set(a => a.totalDeviceDebugRunTime, totalDeviceDebugRunTime).Where(a => a.roomName.Contains(roomName))
  .ExecuteAffrows();
            return result;
        }

        public List<DRoomTestRoomStatusDto> getstatusList(string status)
        {
            var list = Repository.Orm.Select<DRoomTestRoomStatusEntity>().Where(t => t.operationStatus .Contains(status)).ToList().Select(t => Mapper.Map<DRoomTestRoomStatusDto>(t)).ToList();
            return list;
        }
    }
}
