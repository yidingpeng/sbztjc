using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Workflow;
using RW.PMS.Domain.Entities.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.Service
{
    public interface IWorkflowService : ICrudApplicationService<WorkflowEntity, int>
    {
        PagedResult<WorkflowListDto> GetPagedList(WorkflowQueryDto input);

        bool Add(AddWorkflowDto input);

        bool Modify(int id, EditWorkflowDto input);

        bool Remove(int id);

        EditWorkflowDto GetFlow(int id);

    }
}
