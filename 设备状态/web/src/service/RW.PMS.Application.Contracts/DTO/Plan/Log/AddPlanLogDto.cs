using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Plan.Log
{
    public class AddPlanLogDto
    {
        public int Id { get; set; }
        public int PlanId { get; set; }
        public string PlanName { get; set; }
        public Guid TaskId { get; set; }
        public string TaskName { get; set; }
        public string Content { get; set; }
    }
}
