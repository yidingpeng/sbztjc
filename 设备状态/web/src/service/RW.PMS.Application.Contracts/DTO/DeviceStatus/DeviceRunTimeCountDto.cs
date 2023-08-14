using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.DeviceStatus
{
    public class DeviceRunTimeCountDto
    {
        public int id { get; set; }
        public DateTime CountTime { get; set; }
        public string deviceName { get; set; }
        public string roomName { get; set; }
        public string week { get; set; }
        public string month { get; set; }
        public string year { get; set; }
    }
}
