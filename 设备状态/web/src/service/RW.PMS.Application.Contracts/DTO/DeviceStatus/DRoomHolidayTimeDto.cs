using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.DeviceStatus
{
    
    public class DRoomHolidayTimeDto
    {
        public int id { get; set; }
        public string deviceName { get; set; }
        public string roomName { get; set; }
        public DateTime createTime { get; set; }
        public double alarmTime { get; set; }
    }
}
