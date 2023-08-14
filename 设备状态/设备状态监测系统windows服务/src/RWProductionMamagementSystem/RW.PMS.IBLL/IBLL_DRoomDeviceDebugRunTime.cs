using RW.PMS.Common;
using RW.PMS.Model.watchDevice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.IBLL
{
   public interface IBLL_DRoomDeviceDebugRunTime : IDependence
    {
        List<DRoom_DeviceDebugRunTimeModel> GetDRoom_DeviceDebugRunTimesdesc(string deviceName, string roomName);
        int addDeviceDebugRunTime(DRoom_DeviceDebugRunTimeModel model);
        List<DRoom_DeviceDebugRunTimeModel> GetOneDRoom_DeviceDebugRunTimeList(string roomName);
    }
}
