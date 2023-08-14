using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Basics
{
    public class DevicestatusSearchDto : PagedQuery
    {
        public int? Id { get; set; }

        public string Path { get; set; }
        //public string DeviceName { get; set; }
        //public string RoomNumber { get; set; }
        //public string IP { get; set; }
    }
}
