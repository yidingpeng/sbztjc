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
    public interface IToolService : ICrudApplicationService<ToolEntity, int>
    {
        PagedResult<ToolDto> GetPagedList(ToolSearchDto input);

        bool Repeatjudgment(ToolSearchDto input);
    }
}
