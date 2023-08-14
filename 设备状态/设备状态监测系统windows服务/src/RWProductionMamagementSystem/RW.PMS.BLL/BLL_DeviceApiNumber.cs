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
    public class BLL_DeviceApiNumber : IBBL_DeviceApiNumber
    {
        private IDAL_DeviceApiNumber _DAl = null;
        public BLL_DeviceApiNumber()
        {
            _DAl = DIService.GetService<IDAL_DeviceApiNumber>();
        }
        public List<Device_ApiNumberModel> getOneApiNumber(string roomName)
        {
            return _DAl.getOneApiNumber(roomName);
        }
    }
}
