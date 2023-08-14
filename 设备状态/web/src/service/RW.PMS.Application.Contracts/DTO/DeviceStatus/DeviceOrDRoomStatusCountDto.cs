using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.DeviceStatus
{
    public class DeviceOrDRoomStatusCountDto
    {
        public int faultNumber { get; set; }
        public int runNumber { get; set; }
        public int stopNumber { get; set; }
        public int testrunNumber { get; set; }
        public int teststopNumber { get; set; }
        public int standbyNumber { get; set; }
        public int debugrunNumber { get; set; }
    }
}
