using RW.PMS.Common;
using RW.PMS.Model.watchDevice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.IBLL
{
   public interface IBLL_DRoomTestOccupyTime : IDependence
    {
        List<DRoom_TestOccupyTimeModel> GetRoom_TestOccupyTimesdesc(string deviceName, string roomName);
        int addRoom_TestOccupyTime(DRoom_TestOccupyTimeModel model);
    }
}
