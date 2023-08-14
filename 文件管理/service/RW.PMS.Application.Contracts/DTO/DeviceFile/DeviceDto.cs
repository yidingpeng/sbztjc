using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.DeviceFile
{
    public class DeviceDto
    {
        public int id { get; set; }
        public string deviceName { get; set; }//设备名称
        public int roomId { get; set; }//试验间id
    }
}
