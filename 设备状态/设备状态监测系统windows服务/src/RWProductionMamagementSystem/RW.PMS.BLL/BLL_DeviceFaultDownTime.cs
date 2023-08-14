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
   public class BLL_DeviceFaultDownTime: IBLL_DeviceFaultDownTime
    {
        private IDAL_DeviceFaultDownTime _DAl = null;
        public BLL_DeviceFaultDownTime()
        {
            _DAl = DIService.GetService<IDAL_DeviceFaultDownTime>();
        }
        public int addFaultDownTime(Device_FaultDownTimeModel model)
        {
            return _DAl.addFaultDownTime(model);
        }

        public List<Device_FaultDownTimeModel> GetDevice_faultDownTimesdesc(string deviceName, string roomName)
        {
            return _DAl.GetDevice_faultDownTimesdesc(deviceName, roomName);
        }

        public List<Device_FaultDownTimeModel> GetOneDevice_FaultDownTimeList(string deviceName, string roomName)
        {
            return _DAl.GetOneDevice_FaultDownTimeList(deviceName, roomName);
        }
    }
}
