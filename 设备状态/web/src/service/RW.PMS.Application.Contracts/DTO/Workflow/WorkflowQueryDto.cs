using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Workflow
{
    public class WorkflowQueryDto : PagedQuery
    {
        public bool? Enabled { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
    }
}
