using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.DeviceStatus
{
    public class FaultReportingDto : PagedQuery
    {
        public int id { get; set; }

        public string testRoom { get; set; }
        public DateTime faultDateTime { get; set; }
        public string deviceName { get; set; }
        public string faultReason { get; set; }
        public int reportingStatus { get; set; }
        public int faultTimeid { get; set; }

    }
}
