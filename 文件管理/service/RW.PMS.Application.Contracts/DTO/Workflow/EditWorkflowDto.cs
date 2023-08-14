using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Workflow
{
    public class EditWorkflowDto : AddWorkflowDto
    {
        public int Id { get; set; }
    }
}
