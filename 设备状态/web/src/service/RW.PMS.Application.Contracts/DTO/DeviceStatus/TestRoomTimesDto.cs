﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.DeviceStatus
{
    public  class TestRoomTimesDto
    {
        public int id { get; set; }
        public DateTime startTime { get; set; }

        public DateTime endTime { get; set; }
        public int roomId { get; set; }
    }
}
