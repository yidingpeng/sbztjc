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
    public class ProcessInfoService : CrudApplicationService<ProcessInfoEntity, int>, IProcessInfoService
    {
        private readonly IToolDetailService _toolDetailService;
        public ProcessInfoService(IDataValidatorProvider dataValidator,
                           IRepository<ProcessInfoEntity, int> productionRepository,
                           IMapper mapper,
                           Lazy<ICurrentUser> currentUser,
                           IToolDetailService toolDetailService) :
          base(dataValidator, productionRepository, mapper, currentUser)
        {
            _toolDetailService = toolDetailService;
        }

        public PagedResult<ProcessInfoDto> GetPagedList(ProcessInfoSearchDto input)
        {
            var list = Repository.Select.From<ProcessInfoEntity>((r, p) => r.LeftJoin(rp => p.Id == rp.ParentId))
                .WhereIf(input.pcsName.NotNullOrWhiteSpace(), (r, p) => r.pcsName.Contains(input.pcsName))
                .Distinct()
                .Count(out var total).Page(input.PageNo, input.PageSize).ToList((r, p) => new
                {
                    model = r,
                    pcsParentTxt = p,
                }).Select(t =>
                {
                    var mapp = Mapper.Map<ProcessInfoDto>(t.model);
                    mapp.pcsParentTxt = t.pcsParentTxt.pcsName;
                    return mapp;
                }).ToList();

            return new PagedResult<ProcessInfoDto>(Mapper.Map<List<ProcessInfoDto>>(list), total);
        }

        public List<ProcessInfoDto> GetParentProcessList(ProcessInfoSearchDto input)
        {
            return Mapper.Map<List<ProcessInfoDto>>(Repository.Select.WhereIf(input.Id.HasValue, r => r.ParentId != input.Id.Value && r.Id != input.Id).Where(r => r.usingFlag != -1).ToList());
        }

        public IList<ProcessInfoDto> GetTreeList(ProcessInfoSearchDto input)
        {
            var list = Repository.Select.WhereIf(input.pcsName.NotNullOrWhiteSpace(), r => r.pcsName.Contains(input.pcsName)).ToList();
            var treeList = BuildTreeList<ProcessInfoDto, ProcessInfoEntity>(list, 0);

            return treeList;
        }

        public bool Repeatjudgment(ProcessInfoSearchDto input)
        {
            return Repository.Select.WhereIf(input.Id.HasValue, r => r.Id != input.Id).Where(r => r.pcsNo == input.pcsNo).Count() > 0;
        }
    }
}

