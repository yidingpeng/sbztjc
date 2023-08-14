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
    public class BLL_DeviceCount : IBLL_DeviceCount
    {
        private IDAL_DeviceCount _DAl = null;
        public BLL_DeviceCount()
        {
            _DAl = DIService.GetService<IDAL_DeviceCount>();
        }
        public int insertDataCount(Device_DataCountModel model)
        {
            return _DAl.insertDataCount(model);
        }
    }
}
