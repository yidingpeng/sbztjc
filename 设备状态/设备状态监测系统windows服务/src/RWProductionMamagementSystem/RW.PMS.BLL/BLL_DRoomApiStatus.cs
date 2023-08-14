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
    public class BLL_DRoomApiStatus : IBLL_DRoomApiStatus
    {
        private IDAL_DRoomApiStatus _DAl = null;
        public BLL_DRoomApiStatus()
        {
            _DAl = DIService.GetService<IDAL_DRoomApiStatus>();
        }
        public DRoom_ApiStatusModel GetDRoom_ApiStatusOne(string roomName)
        {
            return _DAl.GetDRoom_ApiStatusOne(roomName);
        }

        public int updateApiStatusOne(string roomName, string status)
        {
            return _DAl.updateApiStatusOne(roomName, status);
        }
    }
}
