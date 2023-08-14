using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.DeviceStatus
{
    public class DRoomTimeInputDto 
    {
       public string roomName { get; set; }
       public double holidayTime { get; set; }
       public double weibaoTime { get; set; }
       public double jiaozhunTime { get; set; }
     public   double zhiduTime { get; set; }
    }
}
