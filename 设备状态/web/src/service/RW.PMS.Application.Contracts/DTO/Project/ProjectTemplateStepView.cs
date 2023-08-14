using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Project
{
    public class ProjectTemplateStepView
    {
        public int Id { get; set; }
        public int TemplateId { get; set; }
        public string StepName { get; set; }
        public int OrderIndex { get; set; }
    }
}
