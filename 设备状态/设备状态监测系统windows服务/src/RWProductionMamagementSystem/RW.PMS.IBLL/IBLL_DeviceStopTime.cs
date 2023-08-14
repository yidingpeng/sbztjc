using RW.PMS.Common;
using RW.PMS.Model.watchDevice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.IBLL
{
   public interface IBLL_DeviceStopTime : IDependence
    {
        List<Device_StopTimeModel> GetDevice_StopTimesdesc(string deviceName, string roomName);
        int addDevice_StopTime(Device_StopTimeModel model);
        List<Device_StopTimeModel> GetOneDevice_StopTimeList(string deviceName, string roomName);
    }
}
