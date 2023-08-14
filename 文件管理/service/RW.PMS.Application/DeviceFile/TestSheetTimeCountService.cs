using AutoMapper;
using RW.PMS.Application.Contracts.DeviceFile;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.DeviceFile;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.Extender;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.DeviceFile;
using RW.PMS.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.DeviceFile
{
    public  class TestSheetTimeCountService : CrudApplicationService<DeviceTestSheetTimeCountEntity, int>, ITestSheetTimeCountService
    {
        public TestSheetTimeCountService(IDataValidatorProvider dataValidator,
                            IRepository<DeviceTestSheetTimeCountEntity, int> productionRepository,
                            IMapper mapper,
                            Lazy<ICurrentUser> currentUser) :
           base(dataValidator, productionRepository, mapper, currentUser)
        {

        }

        public List<DeviceTestSheetMonthCountDto> GetMonthCount(int years, string roomName)
        {
            var list = Repository.Orm.Select<DeviceTestSheetMonthCountEntity>().Where(t => t.years == years && t.testName.Contains(roomName)).ToList().Select(t => Mapper.Map<DeviceTestSheetMonthCountDto>(t)).ToList();
            return list;
        }

        public List<DeviceTestSheetTimeCountDto> getTestSheetTimeList(string testName, DateTime startTime, DateTime endTime)
        {
            var list = Repository.Orm.Select<DeviceTestSheetTimeCountEntity>().Where(t => t.testName.Contains(testName) && (t.alarmTime >= startTime && t.alarmTime <= endTime)).ToList().Select(t => Mapper.Map<DeviceTestSheetTimeCountDto>(t)).ToList();
            return list;
        }

        public List<DeviceTestSheetWeekCountDto> getweekcount(int years, int months, string roomName)
        {
            var list = Repository.Orm.Select<DeviceTestSheetWeekCountEntity>().Where(t => t.months == months && t.years == years && t.testName.Contains(roomName)).ToList().Select(t => Mapper.Map<DeviceTestSheetWeekCountDto>(t)).ToList();
            return list;
        }

        public int InsertMonthCountTime(DeviceTestSheetMonthCountDto input)
        {
            var entity = Mapper.Map<DeviceTestSheetMonthCountEntity>(input);
            return Repository.Orm.Insert(entity).ExecuteAffrows();
        }

        public int insertTestSheet(DeviceTestSheetTimeCountDto input)
        {
            var entity = Mapper.Map<DeviceTestSheetTimeCountEntity>(input);
            return ((int)Repository.Orm.Insert<DeviceTestSheetTimeCountEntity>(entity).ExecuteIdentity());

        }

        public int InsertweekCountTime(DeviceTestSheetWeekCountDto input)
        {
            var entity = Mapper.Map<DeviceTestSheetWeekCountEntity>(input);
            return Repository.Orm.Insert(entity).ExecuteAffrows();
        }

        public int InsertYearCountTime(DeviceTestSheetYearCountDto input)
        {
            var entity = Mapper.Map<DeviceTestSheetYearCountEntity>(input);
            return Repository.Orm.Insert(entity).ExecuteAffrows();
        }




        //导出
        public List<DeviceTestSheetMonthCountDto> GetMonthCountvueimport(DataCountNYRDto input)
        {
            var list = Repository.Orm.Select<DeviceTestSheetMonthCountEntity>().Where(t => t.years == input.year.ToInt32() && t.months == input.month.ToInt32()).ToList().Select(t => Mapper.Map<DeviceTestSheetMonthCountDto>(t)).ToList();
            return list;
        }
        public List<DeviceTestSheetYearCountDto> GetyearCountvueimport(DataCountNYRDto input)
        {
            var list = Repository.Orm.Select<DeviceTestSheetYearCountEntity>().Where(t => t.years == input.year.ToInt32()).ToList().Select(t => Mapper.Map<DeviceTestSheetYearCountDto>(t)).ToList();
            return list;
        }
        public List<DeviceTestSheetWeekCountDto> getweekcountvueimport(DataCountNYRDto input)
        {
            var list = Repository.Orm.Select<DeviceTestSheetWeekCountEntity>().Where(t => t.years == input.year.ToInt32() && t.months == input.month.ToInt32() && t.weeks == input.week.ToInt32()).ToList().Select(t => Mapper.Map<DeviceTestSheetWeekCountDto>(t)).ToList();
            return list;
        }
        //条件查询

        public PagedResult<DeviceTestSheetWeekCountDto> getweekcountvue(DataCountNYRDto input)
        {
            var list = Repository.Orm.Select<DeviceTestSheetWeekCountEntity>().Where(t => t.years == input.year.ToInt32() && t.months == input.month.ToInt32() && t.weeks == input.week.ToInt32()).Count(out var total).Page(input.PageNo, input.PageSize).ToList().Select(t => Mapper.Map<DeviceTestSheetWeekCountDto>(t)).ToList();
            return new PagedResult<DeviceTestSheetWeekCountDto>(list, total);
        }
        public PagedResult<DeviceTestSheetMonthCountDto> GetMonthCountvue(DataCountNYRDto input)
        {
            var list = Repository.Orm.Select<DeviceTestSheetMonthCountEntity>().Where(t => t.years == input.year.ToInt32() && t.months == input.month.ToInt32()).Count(out var total).Page(input.PageNo, input.PageSize).ToList().Select(t => Mapper.Map<DeviceTestSheetMonthCountDto>(t)).ToList();
            return new PagedResult<DeviceTestSheetMonthCountDto>(list, total);
        }

        public PagedResult<DeviceTestSheetYearCountDto> GetyearCountvue(DataCountNYRDto input)
        {
            var list = Repository.Orm.Select<DeviceTestSheetYearCountEntity>().Where(t => t.years == input.year.ToInt32()).Count(out var total).Page(input.PageNo, input.PageSize).ToList().Select(t => Mapper.Map<DeviceTestSheetYearCountDto>(t)).ToList();
            return new PagedResult<DeviceTestSheetYearCountDto>(list, total);
        }
    }
}
