using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.DeviceStatus
{
    public class DeviceTimeInputDto
    {
        public string deviceRoom { get; set; }
       public string deviceName { get; set; }
      public  double holidayTime { get; set; }
       public double weibaoTime { get; set; }
       public double trainningTime { get; set; }
        
    }
}
