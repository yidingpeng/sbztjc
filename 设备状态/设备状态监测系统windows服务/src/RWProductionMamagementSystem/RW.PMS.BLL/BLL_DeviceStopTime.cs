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
    public class BLL_DeviceStopTime : IBLL_DeviceStopTime
    {
        private IDAL_DeviceStopTime _DAl = null;
        public BLL_DeviceStopTime()
        {
            _DAl = DIService.GetService<IDAL_DeviceStopTime>();
        }
        public int addDevice_StopTime(Device_StopTimeModel model)
        {
            return _DAl.addDevice_StopTime(model);
        }

        public List<Device_StopTimeModel> GetDevice_StopTimesdesc(string deviceName, string roomName)
        {
            return _DAl.GetDevice_StopTimesdesc(deviceName,roomName);
        }

        public List<Device_StopTimeModel> GetOneDevice_StopTimeList(string deviceName, string roomName)
        {
            return _DAl.GetOneDevice_StopTimeList(deviceName, roomName);
        }
    }
}
