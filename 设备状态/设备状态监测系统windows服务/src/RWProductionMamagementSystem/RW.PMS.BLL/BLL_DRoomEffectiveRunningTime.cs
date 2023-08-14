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
    public class BLL_DRoomEffectiveRunningTime : IBLL_DRoomEffectiveRunningTime
    {
        private IDAL_DRoomEffectiveRunningTime _DAl = null;
        public BLL_DRoomEffectiveRunningTime()
        {
            _DAl = DIService.GetService<IDAL_DRoomEffectiveRunningTime>();
        }
        public int addEffectiveRunningTime(DRoom_EffectiveRunningTimeModel model)
        {
           return _DAl.addEffectiveRunningTime(model);
        }

        public List<DRoom_EffectiveRunningTimeModel> GetDRoom_EffectiveRunningTimesdesc(string deviceName, string roomName)
        {
            return _DAl.GetDRoom_EffectiveRunningTimesdesc(deviceName, roomName);
        }

        public List<DRoom_EffectiveRunningTimeModel> GetOneDRoom_EffectiveRunningTimeList(string roomName)
        {
            return _DAl.GetOneDRoom_EffectiveRunningTimeList(roomName);
        }
    }
}
