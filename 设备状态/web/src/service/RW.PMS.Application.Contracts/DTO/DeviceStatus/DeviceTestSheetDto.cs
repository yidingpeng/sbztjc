using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.DeviceStatus
{
    public class DeviceTesSheetDto : PagedQuery
    {
        public  int id { get; set; }
        public string deviceRoom { get; set; }
        public string deviceName { get; set; }
        public string projectName { get; set; }
        public string employeeId { get; set; }
        public string testEngineer { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public string remark { get; set; }
    }
}
