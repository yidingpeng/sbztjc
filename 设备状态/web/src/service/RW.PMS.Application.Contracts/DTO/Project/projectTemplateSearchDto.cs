using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Project
{
    public class projectTemplateSearchDto : PagedQuery
    {
        public int? Id { get; set; }
        public string Name { get; set; }
    }
}
