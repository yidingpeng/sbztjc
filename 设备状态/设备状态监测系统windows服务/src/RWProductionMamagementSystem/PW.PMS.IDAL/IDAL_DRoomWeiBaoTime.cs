﻿using RW.PMS.Common;
using RW.PMS.Model.watchDevice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.IDAL
{
  public  interface IDAL_DRoomWeiBaoTime: IDependence
    {
        List<DRoom_weibaoTimeModel> GetOneDRoom_weibaoTimeList(string roomName);
    }
}
