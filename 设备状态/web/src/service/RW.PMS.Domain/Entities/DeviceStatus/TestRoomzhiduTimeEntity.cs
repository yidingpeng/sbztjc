using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.DeviceStatus
{
    [Table(Name = "device_zhiduTime")]
    public class TestRoomzhiduTimeEntity : IEDRoomJiaoZhunTimeEntityntity<int>
    {

        public DateTime startTime { get; set; }

        public DateTime endTime { get; set; }
        public int roomId { get; set; }
    }

}
