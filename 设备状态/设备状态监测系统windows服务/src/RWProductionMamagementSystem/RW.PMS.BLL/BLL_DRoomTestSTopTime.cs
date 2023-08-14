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
    public class BLL_DRoomTestSTopTime : IBLL_DRoomTestSTopTime
    {
        private IDAL_DRoomTestSTopTime _DAl = null;
        public BLL_DRoomTestSTopTime()
        {
            _DAl = DIService.GetService<IDAL_DRoomTestSTopTime>();
        }
        public int addTestStopTime(DRoom_TestSTopTimeModel model)
        {
          return  _DAl.addTestStopTime(model);
        }

        public List<DRoom_TestSTopTimeModel> GetDRoom_TestStopTimedesc(string deviceName, string roomName)
        {
          return  _DAl.GetDRoom_TestStopTimedesc(deviceName, roomName);
        }

        public List<DRoom_TestSTopTimeModel> GetOneDRoom_TestSTopTimeList(string roomName)
        {
            return _DAl.GetOneDRoom_TestSTopTimeList(roomName);
        }
    }
}
