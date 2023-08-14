using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Project;
using RW.PMS.Domain.Entities.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.Project
{
    public interface IProject_infoService : ICrudApplicationService<Project_infoEntity, int>
    {
        public PagedResult<Project_infoDto> GetProject_infoPagedList(Project_infoSearchDto input);

    }
}


