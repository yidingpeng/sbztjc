using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.DeviceStatus
{
    public class DeviceandRoomDto :PagedQuery
    {
        public int id { get; set; }
        public string deviceName { get; set; }
        public string roomName { get; set; }
        public int roomid { get; set; }


    }
}
