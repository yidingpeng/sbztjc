using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Plan
{
    public class ImportDto
    {
        public int CurrentPlanId { get; set; }
        public int TemplatePlanId { get; set; }
    }
}
