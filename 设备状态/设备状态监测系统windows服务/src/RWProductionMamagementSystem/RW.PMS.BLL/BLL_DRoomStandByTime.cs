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
   public class BLL_DRoomStandByTime: IBLL_DRoomStandByTime
    {
        private IDAL_DRoomStandByTime _DAl = null;
        public BLL_DRoomStandByTime()
        {
            _DAl = DIService.GetService<IDAL_DRoomStandByTime>();
        }

        public int addStandByTime(DRoom_StandByTimeModel model)
        {
          return  _DAl.addStandByTime(model);
        }

        public List<DRoom_StandByTimeModel> GetDRoom_StandByTimesdesc(string deviceName, string roomName)
        {
            return _DAl.GetDRoom_StandByTimesdesc(deviceName, roomName);
        }

        public List<DRoom_StandByTimeModel> GetOneDRoom_StandByTimeList(string roomName)
        {
            return _DAl.GetOneDRoom_StandByTimeList(roomName);
        }
    }
}
