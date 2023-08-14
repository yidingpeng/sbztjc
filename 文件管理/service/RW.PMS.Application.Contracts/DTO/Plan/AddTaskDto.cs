using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Plan
{
    public class AddTaskDto
    {
        public int PlanId { get; set; }
        public string TaskName { get; set; }

        public Guid ParentId { get; set; }
    }
}
