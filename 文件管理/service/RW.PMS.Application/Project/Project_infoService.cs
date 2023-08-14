using AutoMapper;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Project;
using RW.PMS.Application.Contracts.Project;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.Extensions;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.Project;
using RW.PMS.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.BaseInfo
{
    public class Project_infoService : CrudApplicationService<Project_infoEntity, int>, IProject_infoService
    {
        public Project_infoService(IDataValidatorProvider dataValidator, IRepository<Project_infoEntity, int> repository, IMapper mapper, Lazy<ICurrentUser> currentUser) : base(dataValidator, repository, mapper, currentUser)
        {

        }

        public PagedResult<Project_infoDto> GetProject_infoPagedList(Project_infoSearchDto input)
        {
            var list = Repository.Select
                .WhereIf(input.pt_Code.NotNullOrWhiteSpace(), t => t.pt_Code.Contains(input.pt_Code))
                .Count(out var total).Page(input.PageNo, input.PageSize)
                 .ToList((t) => new
                 {
                     en = t
                 })
                 .Select(t =>
                 {
                     var output = Mapper.Map<Project_infoDto>(t.en);
                     return output;
                 });
            return new PagedResult<Project_infoDto>(Mapper.Map<List<Project_infoDto>>(list), total);
        }

    }
}
