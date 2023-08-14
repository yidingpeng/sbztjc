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
    public class BLL_DeviceRunTime : IBLL_DeviceRunTime
    {
        private IDAL_DeviceRunTime _DAl = null;
        public BLL_DeviceRunTime()
        {
            _DAl = DIService.GetService<IDAL_DeviceRunTime>();
        }

        public int addRunTime(Device_RunTimeModel model)
        {
            return _DAl.addRunTime(model);
        }

        public List<Device_RunTimeModel> GetDevice_RunTimesdesc(string deviceName, string roomName)
        {
            return _DAl.GetDevice_RunTimesdesc(deviceName,roomName);
        }

        public List<Device_RunTimeModel> GetOneDevice_RunTimeList(string deviceName, string roomName)
        {
            return _DAl.GetOneDevice_RunTimeList(deviceName, roomName);
        }
    }
}
