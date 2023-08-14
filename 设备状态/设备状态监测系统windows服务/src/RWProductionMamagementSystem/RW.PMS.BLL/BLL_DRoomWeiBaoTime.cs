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
    public class BLL_DRoomWeiBaoTime : IBLL_DRoomWeiBaoTime
    {
        private IDAL_DRoomWeiBaoTime _DAl = null;
        public BLL_DRoomWeiBaoTime()
        {
            _DAl = DIService.GetService<IDAL_DRoomWeiBaoTime>();
        }
        public List<DRoom_weibaoTimeModel> GetOneDRoom_weibaoTimeList(string roomName)
        {
           return _DAl.GetOneDRoom_weibaoTimeList(roomName);
        }
    }
}
