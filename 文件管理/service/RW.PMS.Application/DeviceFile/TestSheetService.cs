using AutoMapper;
using RW.PMS.Application.Contracts.DeviceFile;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.DeviceFile;
using RW.PMS.Application.Contracts.System;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.EventBus;
using RW.PMS.CrossCutting.Extensions;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.DeviceFile;
using RW.PMS.Domain.Entities.System;
using RW.PMS.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.DeviceFile
{
    public class TestSheetService : CrudApplicationService<TestSheetEntity, int>, ITestSheetService
    {
        private readonly IEventBus _eventBus;
        private readonly ILogService _log;
        private readonly IRepository<ModuleEntity, int> _moduleRepository;
        public TestSheetService(IDataValidatorProvider dataValidator,
      IRepository<TestSheetEntity, int> roleRepository,
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

        public int insertTestSheet(TestSheetDto input)
        {
            var entity = Mapper.Map<TestSheetEntity>(input);
           return ((int)Repository.Orm.Insert<TestSheetEntity>(entity).ExecuteIdentity());
        }
        public PagedResult<TestSheetDto> getTestSheetAllList(TestSheetDto input)
        {
            var list = Repository.Orm.Select<TestSheetEntity>().WhereIf(input.roomName.NotNullOrWhiteSpace(), t => t.roomName.Contains(input.roomName)).Count(out var total).Page(input.PageNo, input.PageSize).OrderByDescending(t => t.Id).ToList()
      .Select(t => Mapper.Map<TestSheetDto>(t)).ToList();

            return new PagedResult<TestSheetDto>(list, total);
        }
        public bool Update(TestSheetDto input)
        {
            var item = Mapper.Map<TestSheetEntity>(input);
            var result = Repository.Orm.Update<TestSheetEntity>(input.id).SetSourceIgnore(item, col => col == null)
     .ExecuteAffrows() > 0;
            return result;
        }
        public bool Repeatjudgment(TestSheetDto input)
        {
           
            return Repository.Orm.Select<TestSheetEntity>().WhereIf(input.id.HasValue, r => r.Id != input.id).Where(r => r.testNumber.Equals( input.testNumber)).Count() > 0;
        }

        public List<TestSheetDto> getTestSheetAllListDateTime()
        {
            var list = Repository.Orm.Select<TestSheetEntity>().ToList().Select(t => Mapper.Map<TestSheetDto>(t)).ToList();
            return list;
        }
    }
}
