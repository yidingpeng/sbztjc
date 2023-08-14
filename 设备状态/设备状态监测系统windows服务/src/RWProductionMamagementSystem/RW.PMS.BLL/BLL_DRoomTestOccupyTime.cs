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
    public class BLL_DRoomTestOccupyTime : IBLL_DRoomTestOccupyTime
    {
        private IDAL_DRoomTestOccupyTime _DAl = null;
        public BLL_DRoomTestOccupyTime()
        {
            _DAl = DIService.GetService<IDAL_DRoomTestOccupyTime>();
        }
        public int addRoom_TestOccupyTime(DRoom_TestOccupyTimeModel model)
        {
          return  _DAl.addRoom_TestOccupyTime(model);
        }

        public List<DRoom_TestOccupyTimeModel> GetRoom_TestOccupyTimesdesc(string deviceName, string roomName)
        {
            return _DAl.GetRoom_TestOccupyTimesdesc(deviceName, roomName);
        }
    }
}
