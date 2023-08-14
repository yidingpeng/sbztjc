using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.DeviceStatus
{
    [Table(Name = "device_FaultReporting")]
    public class FaultReportingEntity : IEDRoomJiaoZhunTimeEntityntity<int>
    {
        public string testRoom { get; set; }
        public DateTime faultDateTime { get; set; }
        public string deviceName { get; set; }
        public string faultReason { get; set; }
        public int reportingStatus { get; set; }

        public int faultTimeid { get; set; }

    }
}
