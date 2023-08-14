using RW.PMS.Common;
using RW.PMS.Model.watchDevice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.IBLL
{
    public   interface IBLL_DeviceFaultDownTime : IDependence
    {
        List<Device_FaultDownTimeModel> GetDevice_faultDownTimesdesc(string deviceName, string roomName);
        int addFaultDownTime(Device_FaultDownTimeModel model);
        List<Device_FaultDownTimeModel> GetOneDevice_FaultDownTimeList(string deviceName, string roomName);

    }
}
