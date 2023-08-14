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
    public class BLL_DeviceweibaoTime : IBLL_DeviceweibaoTime
    {
        private IDAL_DeviceweibaoTime _DAl = null;
        public BLL_DeviceweibaoTime()
        {
            _DAl = DIService.GetService<IDAL_DeviceweibaoTime>();
        }
        public List<Device_weibaoTimeModel> GetOneDevice_weibaoTimeList(string deviceName, string roomName)
        {
            return _DAl.GetOneDevice_weibaoTimeList(deviceName, roomName);
        }
    }
}
