using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Project
{
    public class ProjectDynamicSearchDto : PagedQuery
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
    }
}
