using RW.PMS.Common;
using RW.PMS.Model.watchDevice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.IDAL
{
   public interface IDAL_DeviceRunTime : IDependence
    {
        List<Device_RunTimeModel> GetDevice_RunTimesdesc(string deviceName, string roomName);
        int addRunTime(Device_RunTimeModel model);
        List<Device_RunTimeModel> GetOneDevice_RunTimeList(string deviceName, string roomName);
    }
}
