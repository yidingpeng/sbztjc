using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Basics;
using RW.PMS.Application.Contracts.DTO.Configuration;
using RW.PMS.Domain.Entities.Basics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.Basics
{
    public interface IProcessInfoService : ICrudApplicationService<ProcessInfoEntity, int>
    {
        PagedResult<ProcessInfoDto> GetPagedList(ProcessInfoSearchDto input);
        List<ProcessInfoDto> GetParentProcessList(ProcessInfoSearchDto input);
        IList<ProcessInfoDto> GetTreeList(ProcessInfoSearchDto input);
        bool Repeatjudgment(ProcessInfoSearchDto input);
    }
}
