using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.DeviceStatus
{
    [Table(Name = "device_TestSheet")]
    public  class DeviceTestSheetEntity : IEDRoomJiaoZhunTimeEntityntity<int>
    {
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
