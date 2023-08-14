using RW.PMS.Common;
using RW.PMS.Model.watchDevice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.IDAL
{
   public interface IDAL_DRoomStandByTime : IDependence
    {
        List<DRoom_StandByTimeModel> GetDRoom_StandByTimesdesc(string deviceName, string roomName);
        int addStandByTime(DRoom_StandByTimeModel model);
        List<DRoom_StandByTimeModel> GetOneDRoom_StandByTimeList(string roomName);
    }

}
