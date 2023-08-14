using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.DeviceStatus
{
    public class OPCpointDto : PagedQuery
    {
        public int id { get; set; }

        public string TagName { get; set; }
        public string Address { get; set; }
        public string ExplainInfo { get; set; }
        
    }
}
