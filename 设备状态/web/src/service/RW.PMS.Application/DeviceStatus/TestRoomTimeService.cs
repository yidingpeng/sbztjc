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
    public class TestRoomTimeService : CrudApplicationService<TestRoomweibaoTimeEntity, int>, ITestRoomTimeService
    {
        private readonly IEventBus _eventBus;
        private readonly ILogService _log;
        private readonly IRepository<ModuleEntity, int> _moduleRepository;
        public TestRoomTimeService(IDataValidatorProvider dataValidator,
      IRepository<TestRoomweibaoTimeEntity, int> roleRepository,
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
        public bool Insertweibao(TestRoomTimesDto input)
        {
            var entity = Mapper.Map<TestRoomweibaoTimeEntity>(input);
            Repository.Orm.Insert<TestRoomweibaoTimeEntity>(entity).ExecuteIdentity();

            _log.AddOperationLog(true, "添加成功", $"添加了维保时间");
            return true;
        }
        public bool Insertzhidu(TestRoomTimesDto input)
        {
            var entity = Mapper.Map<TestRoomzhiduTimeEntity>(input);
            Repository.Orm.Insert<TestRoomzhiduTimeEntity>(entity).ExecuteIdentity();

            _log.AddOperationLog(true, "添加成功", $"添加了制度时间");
            return true;
        }
        public bool Insertholiday(TestRoomTimesDto input)
        {
            var entity = Mapper.Map<TestRoomHolidayTimeEntity>(input);
            Repository.Orm.Insert<TestRoomHolidayTimeEntity>(entity).ExecuteIdentity();

            _log.AddOperationLog(true, "添加成功", $"添加了节假日时间");
            return true;
        }
    }
}
