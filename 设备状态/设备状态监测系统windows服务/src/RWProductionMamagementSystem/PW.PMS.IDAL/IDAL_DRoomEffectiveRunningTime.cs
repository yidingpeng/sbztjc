using RW.PMS.Common;
using RW.PMS.Model.watchDevice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.IDAL
{
  public  interface IDAL_DRoomEffectiveRunningTime : IDependence
    {
        List<DRoom_EffectiveRunningTimeModel> GetDRoom_EffectiveRunningTimesdesc(string deviceName, string roomName);
        int addEffectiveRunningTime(DRoom_EffectiveRunningTimeModel model);
        List<DRoom_EffectiveRunningTimeModel> GetOneDRoom_EffectiveRunningTimeList(string roomName);
    }
}
