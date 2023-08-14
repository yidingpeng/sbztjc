using AutoMapper;
using RW.PMS.Application.Contracts.DeviceStatus;
using RW.PMS.Application.Contracts.DTO;
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
    public class FaultReportingService : CrudApplicationService<FaultReportingEntity, int>, IFaultReportingService
    {
        private readonly IEventBus _eventBus;
        private readonly ILogService _log;
        private readonly IRepository<ModuleEntity, int> _moduleRepository;
        public FaultReportingService(IDataValidatorProvider dataValidator,
      IRepository<FaultReportingEntity, int> roleRepository,
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

        public PagedResult<FaultReportingDto> getList(FaultReportingDto input)
        {
            var lst = Repository
                .WhereIf(!string.IsNullOrEmpty(input.testRoom), x => x.testRoom.Contains (input.testRoom))
               
                .Count(out long total)
                .Page(input.PageNo, input.PageSize)
                .OrderByDescending(x => x.faultDateTime)
                .ToList()
                .Select(t =>
                
                     Mapper.Map<FaultReportingDto>(t)
                    
                )
                .ToList();
            return new PagedResult<FaultReportingDto>(lst, total);
        }
        public bool Update(FaultReportingDto input)
        {
            var result = base.Update(input.id, input) > 0;

           // _log.AddOperationLog(true, "更新成功", $"更新了喷漆量[{input.sprayingParameter}]");
            return result;
        }
    }
}
