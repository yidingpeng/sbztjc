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
    public class BLL_DRoomFaultTime : IBLL_DRoomFaultTime
    {
        private IDAL_DRoomFaultTime _DAl = null;
        public BLL_DRoomFaultTime()
        {
            _DAl = DIService.GetService<IDAL_DRoomFaultTime>();
        }
        public int addFaultTime(DRoom_FaultTimeModel model)
        {
           return _DAl.addFaultTime(model);
        }

        public List<DRoom_FaultTimeModel> GetDevice_faultTimesdesc(string deviceName, string roomName)
        {
            return _DAl.GetDevice_faultTimesdesc(deviceName, roomName);
        }

        public List<DRoom_FaultTimeModel> GetOneDRoom_FaultTimeList(string roomName)
        {
            return _DAl.GetOneDRoom_FaultTimeList(roomName);
        }
    }
}
