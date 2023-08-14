using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using RW.PMS.Application.Contracts.Basics;
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
    public class WorkCenterService : CrudApplicationService<WorkCenterEntity, int>, IWorkCenterService
    {
        public WorkCenterService(IDataValidatorProvider dataValidator,
                            IRepository<WorkCenterEntity, int> productionRepository,
                            IMapper mapper,
                            Lazy<ICurrentUser> currentUser) :
           base(dataValidator, productionRepository, mapper, currentUser)
        {
        }

        public PagedResult<WorkCenterDto> GetPagedList(WorkCenterSearchDto input)
        {
            var list = Repository.Select.From<DictionaryEntity, DictionaryEntity>((r,d1,d2) => r.LeftJoin(rd => rd.workType == d1.Id).LeftJoin(rc => rc.atAreaID == d2.Id))
                .WhereIf(input.workName.NotNullOrWhiteSpace(), (r, d1, d2) => r.workName.Contains(input.workName))
                .Count(out var total).Page(input.PageNo, input.PageSize).ToList((r, d1, d2) => new { 
                 model = r,
                    workTypeTxt = d1.DictionaryText,
                    atAreaTxt = d2.DictionaryText,
                }).Select(t =>
                {
                    var mapp = Mapper.Map<WorkCenterDto>(t.model);
                    mapp.workTypeTxt = t.workTypeTxt;
                    mapp.atAreaTxt = t.atAreaTxt;
                    return mapp;
                }).ToList(); 
            return new PagedResult<WorkCenterDto>(Mapper.Map<List<WorkCenterDto>>(list), total);
        }

        public bool Repeatjudgment(WorkCenterSearchDto input)
        {
            return Repository.Select.WhereIf(input.Id.HasValue, r => r.Id != input.Id).Where(r => r.workCode == input.workCode).Count() > 0;
        }

    }
}
