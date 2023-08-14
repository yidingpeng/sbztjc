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
    public class BLL_DeviceholidayTime : IBLL_DeviceholidayTime
    {
        private IDAL_DeviceholidayTime _DAl = null;
        public BLL_DeviceholidayTime()
        {
            _DAl = DIService.GetService<IDAL_DeviceholidayTime>();
        }
        public List<Device_holidayTimeModel> GetOneDevice_holidayTimeList(string deviceName, string roomName)
        {
           return _DAl.GetOneDevice_holidayTimeList(deviceName, roomName);
        }
    }
}
