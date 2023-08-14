using RW.PMS.Common;
using RW.PMS.Model.watchDevice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.IDAL
{
  public  interface IDAL_DeviceholidayTime : IDependence
    {
        List<Device_holidayTimeModel> GetOneDevice_holidayTimeList(string deviceName, string roomName);
    }
}
