using RW.PMS.Common;
using RW.PMS.Model.watchDevice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.IBLL
{
   public interface IBLL_DRoomFaultTime : IDependence
    {
        List<DRoom_FaultTimeModel> GetDevice_faultTimesdesc(string deviceName, string roomName);
        int addFaultTime(DRoom_FaultTimeModel model);
        List<DRoom_FaultTimeModel> GetOneDRoom_FaultTimeList(string roomName);
    }
}
