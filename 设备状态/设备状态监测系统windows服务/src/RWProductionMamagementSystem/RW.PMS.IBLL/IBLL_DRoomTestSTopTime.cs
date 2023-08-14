using RW.PMS.Common;
using RW.PMS.Model.watchDevice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.IBLL
{
  public   interface IBLL_DRoomTestSTopTime : IDependence
    {
        List<DRoom_TestSTopTimeModel> GetDRoom_TestStopTimedesc(string deviceName, string roomName);
        int addTestStopTime(DRoom_TestSTopTimeModel model);
        List<DRoom_TestSTopTimeModel> GetOneDRoom_TestSTopTimeList(string roomName);
    }
}
