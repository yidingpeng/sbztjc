using RW.PMS.Common;
using RW.PMS.IBLL;
using RW.PMS.IDAL;
using RW.PMS.Model.watchDevice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.BLL
{
    public class BLL_DRoomDeviceDebugRunTime : IBLL_DRoomDeviceDebugRunTime
    {
        private IDAL_DRoomDeviceDebugRunTime _DAl = null;
        public BLL_DRoomDeviceDebugRunTime()
        {
            _DAl = DIService.GetService<IDAL_DRoomDeviceDebugRunTime>();
        }
        public int addDeviceDebugRunTime(DRoom_DeviceDebugRunTimeModel model)
        {
            return _DAl.addDeviceDebugRunTime(model);
        }

        public List<DRoom_DeviceDebugRunTimeModel> GetDRoom_DeviceDebugRunTimesdesc(string deviceName, string roomName)
        {
            return _DAl.GetDRoom_DeviceDebugRunTimesdesc(deviceName, roomName);
        }

        public List<DRoom_DeviceDebugRunTimeModel> GetOneDRoom_DeviceDebugRunTimeList(string roomName)
        {
            return _DAl.GetOneDRoom_DeviceDebugRunTimeList(roomName);
        }
    }
}
