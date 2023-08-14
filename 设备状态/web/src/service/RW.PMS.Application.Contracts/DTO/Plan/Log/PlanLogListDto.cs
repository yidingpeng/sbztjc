using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Plan.Log
{
    public class PlanLogListDto
    {
        public int Id { get; set; }
        public DateTime LogTime { get; set; }
        public string Content { get; set; }
    }
}
