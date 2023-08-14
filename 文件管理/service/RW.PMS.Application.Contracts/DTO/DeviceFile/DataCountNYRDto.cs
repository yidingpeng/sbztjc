using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.DeviceFile
{
    public class DataCountNYRDto : PagedQuery
    {
        public string week { get; set; }

        public string month { get; set; }
        public string year { get; set; }
    }
}
