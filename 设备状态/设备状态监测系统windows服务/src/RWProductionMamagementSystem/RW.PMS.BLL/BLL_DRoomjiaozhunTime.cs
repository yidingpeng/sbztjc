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
   public class BLL_DRoomjiaozhunTime: IBLL_DRoomjiaozhunTime
    {
        private IDAL_DRoomjiaozhunTime _DAl = null;
        public BLL_DRoomjiaozhunTime()
        {
            _DAl = DIService.GetService<IDAL_DRoomjiaozhunTime>();
        }

        public List<DRoom_jiaozhunTimeModel> GetOneDRoom_jiaozhunTimeList(string roomName)
        {
          return  _DAl.GetOneDRoom_jiaozhunTimeList(roomName);
        }
    }
}
