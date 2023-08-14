using AutoMapper;
using RW.PMS.Application.Contracts.Basics;
using System;
using System.Collections.Generic;
using System.Linq;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Basics;
using RW.PMS.Application.Contracts.DTO.Configuration;
using RW.PMS.Application.Contracts.DTO.Project;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.Extensions;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.Basics;
using RW.PMS.Domain.Entities.System;
using RW.PMS.Domain.Repositories;

namespace RW.PMS.Application.Basics
{
    public class DeviceService : CrudApplicationService<DeviceEntity, int>, IDeviceService
    {
        public DeviceService(IDataValidatorProvider dataValidator,
                           IRepository<DeviceEntity, int> productionRepository,
                           IMapper mapper,
                           Lazy<ICurrentUser> currentUser) :
          base(dataValidator, productionRepository, mapper, currentUser)
        {
        }

        public PagedResult<DeviceDto> GetPagedList(DeviceSearchDto input)
        {
            var list = Repository.Select
                .WhereIf(input.devName.NotNullOrWhiteSpace(), r => r.devName.Contains(input.devName))
                .Count(out var total).Page(input.PageNo, input.PageSize).ToList();
            return new PagedResult<DeviceDto>(Mapper.Map<List<DeviceDto>>(list), total);
        }

        public bool Repeatjudgment(DeviceSearchDto input)
        {
            return Repository.Select.WhereIf(input.Id.HasValue, r => r.Id != input.Id).Where(r => r.devNo == input.devNo).Count() > 0;
        }
    }
}
