using RW.PMS.Common;
using RW.PMS.Model.watchDevice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.IBLL
{
  public  interface IBLL_DRoomjiaozhunTime: IDependence
    {
        List<DRoom_jiaozhunTimeModel> GetOneDRoom_jiaozhunTimeList(string roomName);
    }
}
