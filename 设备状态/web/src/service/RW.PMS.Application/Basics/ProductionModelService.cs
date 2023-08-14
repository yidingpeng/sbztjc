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
using RW.PMS.Application.Contracts.DTO.Problem;
using System.Dynamic;

namespace RW.PMS.Application.Basics
{
    public class ProductionModelService : CrudApplicationService<ProductionModelEntity, int>, IProductionModelService
    {
        public ProductionModelService(IDataValidatorProvider dataValidator,
                         IRepository<ProductionModelEntity, int> productionRepository,
                         IMapper mapper,
                         Lazy<ICurrentUser> currentUser
                       ) :
        base(dataValidator, productionRepository, mapper, currentUser)
        {}

        public PagedResult<ProductionModelDto> GetPagedList(ProductionModelSearchDto input)
        {
            var list = Repository.Select.From<DictionaryEntity, DictionaryEntity>((r, d1, d2) => r.LeftJoin(rd => rd.ProductionModelType == d1.Id).LeftJoin(rc => rc.ProductionType == d2.Id))
                .WhereIf(input.Pid.HasValue, (r, d1, d2) => r.PID == input.Pid.Value)
                .WhereIf(input.Pname.NotNullOrWhiteSpace(), (r, d1, d2) => r.Pname.Contains(input.Pname))
                .OrderBy((r, d1, d2) => r.OrderIndex)
                .Count(out var total).Page(input.PageNo, input.PageSize).ToList((r, d1, d2) => new {
                    model = r,
                    ProductionModelTypeTxt = d1.DictionaryText,
                    ProductionTypeTxt = d2.DictionaryText,
                }).Select(t =>
                {
                    var mapp = Mapper.Map<ProductionModelDto>(t.model);
                    mapp.ProductionModelTypeTxt = t.ProductionModelTypeTxt;
                    mapp.ProductionTypeTxt = t.ProductionTypeTxt;
                    return mapp;
                }).ToList();

            return new PagedResult<ProductionModelDto>(Mapper.Map<List<ProductionModelDto>>(list), total);
        }

        public bool Repeatjudgment(ProductionModelSearchDto input)
        {
            return Repository.Select.WhereIf(input.Pid.HasValue, r => r.PID == input.Pid).WhereIf(input.Id.HasValue, r => r.Id != input.Id).Where(r => r.PmodelCode == input.PmodelCode).Count() > 0;
        }
    }
}
