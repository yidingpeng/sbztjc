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
    internal class ProductExtendService : CrudApplicationService<ProductExtendEntity, int>, IProductExtendService
    {
        public ProductExtendService(IDataValidatorProvider dataValidator,
                              IRepository<ProductExtendEntity, int> productionRepository,
                              IMapper mapper,
                              Lazy<ICurrentUser> currentUser
                             ) :
             base(dataValidator, productionRepository, mapper, currentUser)
        { }

        public PagedResult<ProductExtendDto> GetPagedList(ProductExtendSearchDto input)
        {
            var list = Repository.Select.From<ProductionModelEntity>((r, p) => r.LeftJoin(rp => rp.PModelID == p.Id))
                .WhereIf(input.PModelID.HasValue, (r, p) => r.PModelID == input.PModelID.Value)
                .WhereIf(input.colName.NotNullOrWhiteSpace(), (r, p) => r.colName.Contains(input.colName))
                .Count(out var total).Page(input.PageNo, input.PageSize).ToList((r, p) => new
                {
                    model = r,
                    ptxt = p.Pname,
                }).Select(t =>
                {
                    var mapp = Mapper.Map<ProductExtendDto>(t.model);
                    mapp.PModelTxt = t.ptxt;
                    return mapp;
                }).ToList();

            return new PagedResult<ProductExtendDto>(Mapper.Map<List<ProductExtendDto>>(list), total);
        }

        public bool Repeatjudgment(ProductExtendSearchDto input)
        {
            return Repository.Select.WhereIf(input.Id.HasValue, r => r.Id != input.Id).WhereIf(input.PModelID.HasValue, r => r.PModelID == input.PModelID).Where(r => r.colName == input.colName).Count() > 0;
        }
    }
}
