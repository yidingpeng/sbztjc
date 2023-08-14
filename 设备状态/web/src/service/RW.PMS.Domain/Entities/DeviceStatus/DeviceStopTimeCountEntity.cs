using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.DeviceStatus
{
    [Table(Name = "device_StopTimeCount")]
    public   class DeviceStopTimeCountEntity : IEDRoomJiaoZhunTimeEntityntity<int>
    {
    
        public DateTime CountTime { get; set; }
        public string deviceName { get; set; }
        public string roomName { get; set; }
        public string week { get; set; }
        public string month { get; set; }
        public string year { get; set; }
    }
}
