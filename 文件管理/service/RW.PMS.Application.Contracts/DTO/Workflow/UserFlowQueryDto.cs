using RW.PMS.Domain.Entities.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Workflow
{
    public class UserFlowQueryDto : PagedQuery
    {
        public string Title { get; set; }
        public UserFlowStatus? Status { get; set; }
    }
}
