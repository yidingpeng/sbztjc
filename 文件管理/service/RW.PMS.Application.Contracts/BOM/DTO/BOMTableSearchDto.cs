using RW.PMS.Application.Contracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.BOM.DTO
{
    public class BOMTableSearchDto : PagedQuery
    {
        public string Key { get; set; }
        public string Version { get; set; }
    }
}
