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
    public class DeviceStatusService : CrudApplicationService<DeviceStatusTagNameEntity, int>, IDeviceStatusService
    {
        private readonly IEventBus _eventBus;
        private readonly ILogService _log;
        private readonly IRepository<ModuleEntity, int> _moduleRepository;
        public DeviceStatusService(IDataValidatorProvider dataValidator,
      IRepository<DeviceStatusTagNameEntity, int> roleRepository,
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

        public PagedResult<DeviceStatusDto> GetDeviceNameAllList()
        {
           var list = Repository.Orm.Select<DeviceStatusEntity>().Count(out var total).ToList()
          .Select(t => Mapper.Map<DeviceStatusDto>(t)).ToList();
       
               
          
            
            return new PagedResult<DeviceStatusDto>(list, total);
        }

        public List<DeviceStatusDto> GetDeviceNameConditionList(string roomName)
        {
            
                //   var list = Repository.Orm.Select<DeviceTestRoomEntity, DeviceNameEntity>().LeftJoin(x => x.t1.Id == x.t2.roomId).Count(out var total).ToList(x => new { x.t1,x.t2})
                //.Select(t => new DeviceTestRoomDto
                //{
                //    id = t.t1.Id,
                //    roomName = t.t1.roomName,
                //    deviceName = t.t2.deviceName
                //}).ToList();
                var list = Repository.Orm.Select<DRoomTestRoomStatusEntity, DeviceStatusEntity>().LeftJoin(x => x.t1.Id == x.t2.roomId).Where(x => x.t1.roomName.Equals(roomName)).Count(out var total).ToList(x => new { x.t1, x.t2 })
                .Select(t => new DeviceStatusDto { deviceName = t.t2.deviceName }).ToList();

                return  list;
            
        }

        public PagedResult<DeviceStatusTagNameDto> GetDeviceStatusNameAllList()
        {
            var list = Repository.Select.Count(out var total).ToList()
          .Select(t => Mapper.Map<DeviceStatusTagNameDto>(t)).ToList();
            return new PagedResult<DeviceStatusTagNameDto>(list, total);
        }

        public PagedResult<DRoomTestRoomStatusDto> GetDeviceTestRoomAllList()
        {
            //   var list = Repository.Orm.Select<DeviceTestRoomEntity, DeviceNameEntity>().LeftJoin(x => x.t1.Id == x.t2.roomId).Count(out var total).ToList(x => new { x.t1,x.t2})
            //.Select(t => new DeviceTestRoomDto
            //{
            //    id = t.t1.Id,
            //    roomName = t.t1.roomName,
            //    deviceName = t.t2.deviceName
            //}).ToList();
            var list = Repository.Orm.Select<DRoomTestRoomStatusEntity>().Count(out var total).ToList()
            .Select(t => Mapper.Map<DRoomTestRoomStatusDto>(t)).ToList();
            
            return new PagedResult<DRoomTestRoomStatusDto>(list,total);
        }

        public PagedResult<DeviceTesSheetDto> getTestSheetAllList(DeviceTesSheetDto input)
        {
            var list = Repository.Orm.Select<DeviceTestSheetEntity>().WhereIf(input.projectName.NotNullOrWhiteSpace(), t => t.projectName.Contains(input.projectName)).Count(out var total).Page(input.PageNo, input.PageSize).OrderByDescending(t => t.Id).ToList()
      .Select(t => Mapper.Map<DeviceTesSheetDto>(t)).ToList();
          
            return new PagedResult<DeviceTesSheetDto>(list, total);
        }

        public bool Insert(DeviceTesSheetDto input)
        {
            var entity = Mapper.Map<DeviceTestSheetEntity>(input);
            Repository.Orm.Insert<DeviceTestSheetEntity>(entity).ExecuteIdentity();
            
            _log.AddOperationLog(true, "添加成功", $"添加了试验单[{input.projectName}]");
            return true;
        }

        public bool Repeatjudgment(DeviceTesSheetDto input)
        {
            return true;
            //return Repository.Orm.Select<DeviceTestSheetEntity>().WhereIf( r => r.Id != input.id).Where(r => r.employeeId.Equals( input.employeeId)).Count() > 0;
        }
        public bool update(DeviceStatusDto input)
        {
            var result = Repository.Orm.Update<DeviceStatusEntity>(input.id).Set(a => a.toalFaultNumber, input.toalFaultNumber)
   .ExecuteAffrows() > 0;
            return result;
        }

        public bool Update(DeviceTesSheetDto input)
        {
            var item = Mapper.Map<DeviceTestSheetEntity>(input);
            var result = Repository.Orm.Update<DeviceTestSheetEntity>(input.id).SetSourceIgnore(item, col => col == null)
     .ExecuteAffrows() > 0;
            return result;
        }

        public int updateDevicetotalFaultDownTime(string deviceName, int roomid, double totalFaultDownTime)
        {
            var result = Repository.Orm.Update<DeviceStatusEntity>().Set(a => a.totalFaultDownTime, totalFaultDownTime).Where(a => a.deviceName.Equals(deviceName)&&a.roomId==roomid)
 .ExecuteAffrows() ;
            return result;
        }

        public int updateDevicetotalFreeTime(string deviceName, int roomid, double totalFreeTime)
        {
            var result = Repository.Orm.Update<DeviceStatusEntity>().Set(a => a.totalFreeTime, totalFreeTime).Where(a => a.deviceName.Equals(deviceName) && a.roomId == roomid)
.ExecuteAffrows();
            return result;
        }

        public int updateDevicetotalRunTime(string deviceName, int roomid, double totalRunTime)
        {
            var result = Repository.Orm.Update<DeviceStatusEntity>().Set(a => a.totalRunTime, totalRunTime).Where(a => a.deviceName.Equals(deviceName) && a.roomId == roomid)
.ExecuteAffrows();
            return result;
        }

        public int updateDevicetotalStopTime(string deviceName, int roomid, double totalStopTime)
        {
            var result = Repository.Orm.Update<DeviceStatusEntity>().Set(a => a.totalStopTime, totalStopTime).Where(a => a.deviceName.Equals(deviceName) && a.roomId == roomid)
.ExecuteAffrows();
            return result;
        }

        public int updateDevicetotalweibaoTime(string deviceName, int roomid, double weibaoTime)
        {
            var result = Repository.Orm.Update<DeviceStatusEntity>().Set(a => a.weibaoTime, weibaoTime).Where(a => a.deviceName.Equals(deviceName) && a.roomId == roomid)
.ExecuteAffrows();
            return result;
        }
        public List<DeviceStatusDto> getdeviceAlllist()
        {
            var list = Repository.Orm.Select<DeviceStatusEntity>().ToList().Select(t => Mapper.Map<DeviceStatusDto>(t)).ToList();
            return list;
        }
        public int updateIsClear( int id, int isClear)
        {
            var result = Repository.Orm.Update<DeviceStatusEntity>().Set(a => a.isClear, isClear).Where(a => a.Id==id)
        .ExecuteAffrows();
            return result;
        }

        public List<DeviceStatusDto> getstatusList(string status)
        {
            var list = Repository.Orm.Select<DeviceStatusEntity>().Where(t => t.operationStatus.Contains(status)).ToList().Select(t => Mapper.Map<DeviceStatusDto>(t)).ToList();
            return list;
        }
    }
}
