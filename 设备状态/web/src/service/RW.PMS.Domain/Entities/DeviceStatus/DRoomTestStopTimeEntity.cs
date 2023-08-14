using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.DeviceStatus
{
    [Table(Name = "droom_TestStopTime")]
    public class DRoomTestStopTimeEntity : IEDRoomJiaoZhunTimeEntityntity<int>
    {
        public string deviceName { get; set; }
        public string roomName { get; set; }
        public string onOrOff { get; set; }
        public DateTime alarmTime { get; set; }
    }
}
