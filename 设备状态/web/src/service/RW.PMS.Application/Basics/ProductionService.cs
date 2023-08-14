using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using RW.PMS.Application.Contracts.Basics;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Basics;
using RW.PMS.Application.Contracts.DTO.Configuration;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.Extensions;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.Basics;
using RW.PMS.Domain.Repositories;

namespace RW.PMS.Application.Basics
{
    public class ProductionService : CrudApplicationService<ProductionEntity, int>, IProductionService
    {
        public ProductionService(IDataValidatorProvider dataValidator,
                            IRepository<ProductionEntity, int> productionRepository,
                            IMapper mapper,
                            Lazy<ICurrentUser> currentUser) :
           base(dataValidator, productionRepository, mapper, currentUser)
        {
        }

        public PagedResult<ProductionDto> GetPagedList(ProductionSearchDto input)
        {
            var list = Repository.Select
                .WhereIf(input.pname.NotNullOrWhiteSpace(),r => r.Pname.Contains(input.pname))
                .Count(out var total).Page(input.PageNo, input.PageSize).ToList();
            return new PagedResult<ProductionDto>(Mapper.Map<List<ProductionDto>>(list), total);
        }

        public bool Repeatjudgment(ProductionSearchDto input)
        {
            return Repository.Select.WhereIf(input.Id.HasValue, r => r.Id != input.Id).Where(r => r.PCode == input.PCode).Count() > 0;
        }

    }
}
