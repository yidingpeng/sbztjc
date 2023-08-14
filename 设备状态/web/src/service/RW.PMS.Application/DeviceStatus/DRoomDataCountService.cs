using AutoMapper;
using iTextSharp.text.pdf.parser.clipper;
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
    public class DRoomDataCountService : CrudApplicationService<DRoomEffectiveRunningTimeEntity, int>, IDRoomDataCountService
    {
        private readonly IEventBus _eventBus;
        private readonly ILogService _log;
        private readonly IRepository<ModuleEntity, int> _moduleRepository;
        public DRoomDataCountService(IDataValidatorProvider dataValidator,
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

        public List<DRoomDeviceDebugRunTimeDto> getDeviceDebugRunTimeList(string roomName, DateTime startTime, DateTime endTime)
        {
            var list = Repository.Orm.Select<DRoomDeviceDebugRunTimeEntity>().Where(t => t.roomName.Contains(roomName)&&(t.alarmTime>=startTime&& t.alarmTime<=endTime)).ToList().Select(t => Mapper.Map<DRoomDeviceDebugRunTimeDto>(t)).ToList();
            return list;
        }
                
        public List<DRoomEffectiveRunningTimeDto> getEffectiveRunningTimeList(string roomName, DateTime startTime, DateTime endTime)
        {
            var list = Repository.Orm.Select<DRoomEffectiveRunningTimeEntity>().Where(t => t.roomName.Contains(roomName) && (t.alarmTime >= startTime && t.alarmTime <= endTime)).ToList().Select(t => Mapper.Map<DRoomEffectiveRunningTimeDto>(t)).ToList();
            return list;
        }

        public List<DRoomFaultTimeDto> getFaultTimeTimeList(string roomName, DateTime startTime, DateTime endTime)
        {
            var list = Repository.Orm.Select<DRoomFaultTimeEntity>().Where(t => t.roomName.Contains(roomName) && (t.alarmTime >= startTime && t.alarmTime <= endTime)).ToList().Select(t => Mapper.Map<DRoomFaultTimeDto>(t)).ToList();
            return list;
        }

        public List<DRoomMonthCountDto> GetMonthCount(int years,string roomName)
        {
            var list = Repository.Orm.Select<DRoomMonthCountEntity>().Where(t => t.years == years&&t.roomName.Contains(roomName)).ToList().Select(t => Mapper.Map<DRoomMonthCountDto>(t)).ToList();
            return list;
        }

      
      

        public List<DRoomStandbyTimeDto> getStandbyTimeList(string roomName, DateTime startTime, DateTime endTime)
        {
            var list = Repository.Orm.Select<DRoomStandbyTimeEntity>().Where(t => t.roomName.Contains(roomName) && (t.alarmTime >= startTime && t.alarmTime <= endTime)).ToList().Select(t => Mapper.Map<DRoomStandbyTimeDto>(t)).ToList();
            return list;
        }

        public List<DRoomTestStopTimeDto> getTestStopTimeList(string roomName, DateTime startTime, DateTime endTime)
        {
            var list = Repository.Orm.Select<DRoomTestStopTimeEntity>().Where(t => t.roomName.Contains(roomName) && (t.alarmTime >= startTime && t.alarmTime <= endTime)).ToList().Select(t => Mapper.Map<DRoomTestStopTimeDto>(t)).ToList();
            return list;
        }

        public List<DRoomWeekCountDto> getweekcount(int years, int months, string roomName)
        {
            var list = Repository.Orm.Select<DRoomWeekCountEntity>().Where(t => t.months==months && t.years==years &&t.roomName.Contains(roomName)).ToList().Select(t => Mapper.Map<DRoomWeekCountDto>(t)).ToList();
            return list;
        }

       

       

        public List<DRoomWeiBaoTimeOnOffDto> getweibaoTimeList(string roomName, DateTime startTime, DateTime endTime)
        {
            var list = Repository.Orm.Select<DRoomWeiBaoTimeOnOffEntity>().Where(t => t.roomName.Contains(roomName) && (t.alarmTime >= startTime && t.alarmTime <= endTime) ).ToList().Select(t => Mapper.Map<DRoomWeiBaoTimeOnOffDto>(t)).ToList();
            return list;
        }

       

       

        public DRoomzhiduTimeDto getZhiDuTimeList(string roomName)
        {
            var list = Repository.Orm.Select<DRoomzhiduTimeEntity>().Where(t => t.roomName.Contains(roomName)).ToList().Select(t => Mapper.Map<DRoomzhiduTimeDto>(t)).ToList().FirstOrDefault();
            return list;
        }

        public int InsertDeviceDebugRunTime(DRoomDeviceDebugRunTimeDto input)
        {
            var entity = Mapper.Map<DRoomDeviceDebugRunTimeEntity>(input);
            return Repository.Orm.Insert(entity).ExecuteAffrows();
        }

        public int InsertEffectiveRunningTime(DRoomEffectiveRunningTimeDto input)
        {
            var entity = Mapper.Map<DRoomEffectiveRunningTimeEntity>(input);
            return Repository.Orm.Insert(entity).ExecuteAffrows();
        }

        public int InsertFaultTime(DRoomFaultTimeDto input)
        {
            var entity = Mapper.Map<DRoomFaultTimeEntity>(input);
            return Repository.Orm.Insert(entity).ExecuteAffrows();
        }

        public int InsertMonthCountTime(DRoomMonthCountDto input)
        {
            var entity = Mapper.Map<DRoomMonthCountEntity>(input);
            return Repository.Orm.Insert(entity).ExecuteAffrows();
        }

        public int InsertStandbyTime(DRoomStandbyTimeDto input)
        {
            var entity = Mapper.Map<DRoomStandbyTimeEntity>(input);
            return Repository.Orm.Insert(entity).ExecuteAffrows();
        }

        public int InsertTestStopTime(DRoomTestStopTimeDto input)
        {
            var entity = Mapper.Map<DRoomTestStopTimeEntity>(input);
            return Repository.Orm.Insert(entity).ExecuteAffrows();
        }

        public int InsertweekCountTime(DRoomWeekCountDto input)
        {
            var entity = Mapper.Map<DRoomWeekCountEntity>(input);
            return Repository.Orm.Insert(entity).ExecuteAffrows();
        }

        public int InsertweibaoTime(DRoomWeiBaoTimeOnOffDto input)
        {
            var entity = Mapper.Map<DRoomWeiBaoTimeOnOffEntity>(input);
            return Repository.Orm.Insert(entity).ExecuteAffrows();
        }

        public int InsertYearCountTime(DRoomYearCountDto input)
        {
            var entity = Mapper.Map<DRoomYearCountEntity>(input);
            return Repository.Orm.Insert(entity).ExecuteAffrows();
        }


        //导出
        public List<DRoomMonthCountDto> GetMonthCountvueimport(DataCountNYRDto input)
        {
            var list = Repository.Orm.Select<DRoomMonthCountEntity>().Where(t => t.years == input.year.ToInt32() && t.months == input.month.ToInt32()).ToList().Select(t => Mapper.Map<DRoomMonthCountDto>(t)).ToList();
            return list;
        }
        public List<DRoomYearCountDto> GetyearCountvueimport(DataCountNYRDto input)
        {
            var list = Repository.Orm.Select<DRoomYearCountEntity>().Where(t => t.years == input.year.ToInt32()).ToList().Select(t => Mapper.Map<DRoomYearCountDto>(t)).ToList();
            return list;
        }
        public List<DRoomWeekCountDto> getweekcountvueimport(DataCountNYRDto input)
        {
            var list = Repository.Orm.Select<DRoomWeekCountEntity>().Where(t => t.years == input.year.ToInt32() && t.months == input.month.ToInt32() && t.weeks == input.week.ToInt32()).ToList().Select(t => Mapper.Map<DRoomWeekCountDto>(t)).ToList();
            return list;
        }
        //条件查询

        public PagedResult<DRoomWeekCountDto> getweekcountvue(DataCountNYRDto input)
        {
            var list = Repository.Orm.Select<DRoomWeekCountEntity>().Where(t => t.years == input.year.ToInt32() && t.months == input.month.ToInt32() && t.weeks == input.week.ToInt32()).Count(out var total).Page(input.PageNo, input.PageSize).ToList().Select(t => Mapper.Map<DRoomWeekCountDto>(t)).ToList();
            return new PagedResult<DRoomWeekCountDto>(list, total);
        }
        public PagedResult<DRoomMonthCountDto> GetMonthCountvue(DataCountNYRDto input)
        {
            var list = Repository.Orm.Select<DRoomMonthCountEntity>().Where(t => t.years == input.year.ToInt32() && t.months == input.month.ToInt32()).Count(out var total).Page(input.PageNo, input.PageSize).ToList().Select(t => Mapper.Map<DRoomMonthCountDto>(t)).ToList();
            return new PagedResult<DRoomMonthCountDto>(list, total);
        }

        public PagedResult<DRoomYearCountDto> GetyearCountvue(DataCountNYRDto input)
        {
            var list = Repository.Orm.Select<DRoomYearCountEntity>().Where(t => t.years == input.year.ToInt32()).Count(out var total).Page(input.PageNo, input.PageSize).ToList().Select(t => Mapper.Map<DRoomYearCountDto>(t)).ToList();
            return new PagedResult<DRoomYearCountDto>(list, total);
        }
    }
}
