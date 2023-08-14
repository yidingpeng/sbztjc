using AutoMapper;
using RW.PMS.Application.Contracts.DeviceStatus;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.DeviceStatus;
using RW.PMS.Application.Contracts.System;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.EventBus;
using RW.PMS.CrossCutting.Extender;
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
    public class DeviceDataCountService : CrudApplicationService<DeviceTrainningTimeEntity, int>, IDeviceDataCountService
    {
        private readonly IEventBus _eventBus;
        private readonly ILogService _log;
        private readonly IRepository<ModuleEntity, int> _moduleRepository;
        public DeviceDataCountService(IDataValidatorProvider dataValidator,
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

        public List<DeviceFaultDownTimesDto> getFaultDownTimeList(string roomName, string deviceName, DateTime startTime, DateTime endTime)
        {
            var list = Repository.Orm.Select<DeviceFaultDownTimesEntity>().Where(t => t.roomName.Contains(roomName) && t.deviceName.Equals(deviceName)&&(t.alarmTime >= startTime && t.alarmTime <= endTime)).ToList().Select(t => Mapper.Map<DeviceFaultDownTimesDto>(t)).ToList();
            return list;
        }

        public List<DeviceFaultNumberTimeDto> getFaultNumberList(string roomName, string deviceName, DateTime startTime, DateTime endTime)
        {
            var list = Repository.Orm.Select<DeviceFaultNumberTimeEntitity>().Where(t => t.roomName.Contains(roomName) && t.deviceName.Equals(deviceName) && (t.alarmTime >= startTime && t.alarmTime <= endTime)).ToList().Select(t => Mapper.Map<DeviceFaultNumberTimeDto>(t)).ToList();
            return list;
        }

        public List<DeviceMonthCountDto> GetMonthCount(int years,string roomName,string deviceName)
        {
            var list = Repository.Orm.Select <DeviceMonthCountEntity > ().Where(t =>  t.years == years&&t.deviceName.Equals(deviceName)&&t.roomName.Contains(roomName)).ToList().Select(t => Mapper.Map<DeviceMonthCountDto>(t)).ToList();
            return list;
        }

        public PagedResult<DeviceMonthCountDto> GetMonthCountvue(DataCountNYRDto input)
        {
            var list = Repository.Orm.Select<DeviceMonthCountEntity>().Where(t => t.months == input.month.ToInt32() && t.years == input.year.ToInt32()).Count(out var total).Page(input.PageNo, input.PageSize).ToList().Select(t => Mapper.Map<DeviceMonthCountDto>(t)).ToList();
            return new PagedResult<DeviceMonthCountDto>(list, total);
        }

       

        public List<DeviceRunTimeDto> getRunTimeList(string roomName, string deviceName, DateTime startTime, DateTime endTime)
        {
            var list = Repository.Orm.Select<DeviceRunTimeEntity>().Where(t => t.roomName.Contains(roomName) && t.deviceName.Equals(deviceName) && (t.alarmTime >= startTime && t.alarmTime <= endTime)).ToList().Select(t => Mapper.Map<DeviceRunTimeDto>(t)).ToList();
            return list;
        }

        public List<DeviceStopTimeDto> getStopTimeList(string roomName, string deviceName, DateTime startTime, DateTime endTime)
        {
            var list = Repository.Orm.Select<DeviceStopTimeEntity>().Where(t => t.roomName.Contains(roomName) && t.deviceName.Equals(deviceName) && (t.alarmTime >= startTime && t.alarmTime <= endTime)).ToList().Select(t => Mapper.Map<DeviceStopTimeDto>(t)).ToList();
            return list;
        }

        public List<DeviceWeekCountDto> getweekcount(int years, int months,string roomName,string deviceName)
        {
            var list = Repository.Orm.Select<DeviceWeekCountEntity>().Where(t =>t.months==months && t.years == years && t.deviceName.Equals(deviceName) && t.roomName.Contains(roomName)).ToList().Select(t => Mapper.Map<DeviceWeekCountDto>(t)).ToList();
            return list;
        }

        public PagedResult<DeviceWeekCountDto> getweekcountvue(DataCountNYRDto input)
        {
            var list = Repository.Orm.Select<DeviceWeekCountEntity>().Where(t => t.months == input. month.ToInt32() && t.years == input.year.ToInt32() && t.weeks== input.week.ToInt32()).Count(out var total).Page(input.PageNo, input.PageSize).ToList().Select(t => Mapper.Map<DeviceWeekCountDto>(t)).ToList();
            return new PagedResult<DeviceWeekCountDto>(list, total);
        }

       
        public List<DeviceWeiBaoTimeOnOffDto> getweibaoTimeList(string roomName, string deviceName, DateTime startTime, DateTime endTime)
        {
            var list = Repository.Orm.Select<DeviceWeiBaoTimeOnOffEntity>().Where(t => t.roomName.Contains(roomName) && t.deviceName.Equals(deviceName) && (t.alarmTime >= startTime && t.alarmTime <= endTime)).ToList().Select(t => Mapper.Map<DeviceWeiBaoTimeOnOffDto>(t)).ToList();
            return list;
        }

        public PagedResult<DeviceYearCountDto> GetyearCountvue(DataCountNYRDto input)
        {
            var list = Repository.Orm.Select<DeviceYearCountEntity>().Where(t =>   t.years == input.year.ToInt32()).Count(out var total).Page(input.PageNo, input.PageSize).ToList().Select(t => Mapper.Map<DeviceYearCountDto>(t)).ToList();
            return new PagedResult<DeviceYearCountDto>(list, total);
        }

       

        public int InsertFaultDownTime(DeviceFaultDownTimesDto input)
        {
            var entity = Mapper.Map<DeviceFaultDownTimesEntity>(input);
            return Repository.Orm.Insert(entity).ExecuteAffrows();
        }

        public int InsertMonthCountTime(DeviceMonthCountDto input)
        {
            var entity = Mapper.Map<DeviceMonthCountEntity>(input);
            return Repository.Orm.Insert(entity).ExecuteAffrows();
        }

        public int InsertRunTimeTime(DeviceRunTimeDto input)
        {
            var entity = Mapper.Map<DeviceRunTimeEntity>(input);
            return Repository.Orm.Insert(entity).ExecuteAffrows();
        }

        public int InsertStopTimeTime(DeviceStopTimeDto input)
        {
            var entity = Mapper.Map<DeviceStopTimeEntity>(input);
            return Repository.Orm.Insert(entity).ExecuteAffrows();
        }

        public int InsertweekCountTime(DeviceWeekCountDto input)
        {
            var entity = Mapper.Map<DeviceWeekCountEntity>(input);
            return Repository.Orm.Insert(entity).ExecuteAffrows();
        }

        public int InsertweibaoTime(DeviceWeiBaoTimeOnOffDto input)
        {
            var entity = Mapper.Map<DeviceWeiBaoTimeOnOffEntity>(input);
            return Repository.Orm.Insert(entity).ExecuteAffrows();
        }

        public int InsertYearCountTime(DeviceYearCountDto input)
        {
            var entity = Mapper.Map<DeviceYearCountEntity>(input);
            return Repository.Orm.Insert(entity).ExecuteAffrows();
        }
        public List<DeviceMonthCountDto> GetMonthCountvueimport(DataCountNYRDto input)
        {
            var list = Repository.Orm.Select<DeviceMonthCountEntity>().Where(t => t.months == input.month.ToInt32() && t.years == input.year.ToInt32()).ToList().Select(t => Mapper.Map<DeviceMonthCountDto>(t)).ToList();
            return list;
        }
        public List<DeviceYearCountDto> GetyearCountvueimport(DataCountNYRDto input)
        {
            var list = Repository.Orm.Select<DeviceYearCountEntity>().Where(t => t.years == input.year.ToInt32()).ToList().Select(t => Mapper.Map<DeviceYearCountDto>(t)).ToList();
            return list;
        }
        public List<DeviceWeekCountDto> getweekcountvueimport(DataCountNYRDto input)
        {
            var list = Repository.Orm.Select<DeviceWeekCountEntity>().Where(t => t.months == input.month.ToInt32() && t.years == input.year.ToInt32() && t.weeks == input.week.ToInt32()).ToList().Select(t => Mapper.Map<DeviceWeekCountDto>(t)).ToList();
            return list;
        }

    }
}
