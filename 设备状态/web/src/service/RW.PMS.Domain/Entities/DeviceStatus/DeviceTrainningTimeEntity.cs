﻿using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.DeviceStatus
{
    [Table(Name = "device_trainingtime")]
    public class DeviceTrainningTimeEntity : IEDRoomJiaoZhunTimeEntityntity<int>
    {
        public string deviceName { get; set; }
        public string roomName { get; set; }
        public DateTime createTime { get; set; }
        public double alarmTime { get; set; }
    }
}
