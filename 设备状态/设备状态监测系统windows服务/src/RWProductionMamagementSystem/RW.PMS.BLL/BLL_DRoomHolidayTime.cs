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
    public class BLL_DRoomHolidayTime : IBLL_DRoomHolidayTime
    {
        private IDAL_DRoomHolidayTime _DAl = null;
        public BLL_DRoomHolidayTime()
        {
            _DAl = DIService.GetService<IDAL_DRoomHolidayTime>();
        }

        public List<DRoom_HolidayTimeModel> GetOneDRoom_holidayTimeList(string roomName)
        {
            return _DAl.GetOneDRoom_holidayTimeList(roomName);
        }
    }
}
