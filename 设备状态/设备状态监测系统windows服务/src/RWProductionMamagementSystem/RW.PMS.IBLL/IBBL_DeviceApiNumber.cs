using RW.PMS.Common;
using RW.PMS.Model.watchDevice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.IBLL
{
   public interface IBBL_DeviceApiNumber : IDependence
    {
        List<Device_ApiNumberModel> getOneApiNumber(string roomName);
    }
}
