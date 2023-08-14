﻿using RW.PMS.Common;
using RW.PMS.Model.watchDevice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.IBLL
{
   public interface IBLL_DRoomApiStatus : IDependence
    {
        DRoom_ApiStatusModel GetDRoom_ApiStatusOne(string roomName);
        int updateApiStatusOne(string roomName, string status);
    }
}
