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
    public class BLL_DRoomZhiDuTime : IBLL_DRoomZhiDuTime
    {
        private IDAL_DRoomZhiDuTime _DAl = null;
        public BLL_DRoomZhiDuTime()
        {
            _DAl = DIService.GetService<IDAL_DRoomZhiDuTime>();
        }
        public List<DRoom_zhiduTimeModel> GetDRoom_ZhiduTimeOne(string roomName)
        {
           return _DAl.GetDRoom_ZhiduTimeOne(roomName);
        }
    }
}
