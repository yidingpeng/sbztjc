﻿using AutoMapper;
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
    public  class OPCpointService : CrudApplicationService<OPCpointEntity, int>, IOPCpointService
    {
        private readonly IEventBus _eventBus;
        private readonly ILogService _log;
        private readonly IRepository<ModuleEntity, int> _moduleRepository;
        public OPCpointService(IDataValidatorProvider dataValidator,
      IRepository<OPCpointEntity, int> roleRepository,
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

        public PagedResult<OPCpointDto> getList(OPCpointDto input)
        {
            var list = Repository.WhereIf(input.Address.NotNullOrWhiteSpace(), t => t.Address.Contains(input.Address)).Count(out var total).Page(input.PageNo, input.PageSize).OrderByDescending(t => t.Id).ToList()
        .Select(t => Mapper.Map<OPCpointDto>(t)).ToList();
            return new PagedResult<OPCpointDto>(list, total);
        }

        public OPCpointEntity Insert(OPCpointDto input)
        {
            var result = base.Insert(input);
            return result;
        }

        public bool Update(OPCpointDto input)
        {
            var result = base.Update(input.id, input) > 0;
            return result;
        }
    }
}
