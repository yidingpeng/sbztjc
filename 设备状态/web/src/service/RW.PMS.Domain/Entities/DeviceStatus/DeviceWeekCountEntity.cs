using AutoMapper.Configuration.Conventions;
using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.DeviceStatus
{
    [Table(Name = "device_deviceweekcount")]

    public class DeviceWeekCountEntity : IEDRoomJiaoZhunTimeEntityntity<int>
    {
        public double totalRunTime { get; set; }//总运行时间
        public double totalStopTime { get; set; }//总停机时间
        public double totalFaultDownTime { get; set; }//总故障停机时间
        public int toalFaultNumber { get; set; }//总故障次数
        public double totalFreeTime { get; set; }//总闲置时间

        public double weibaoTime { get; set; }//总维保时间
       public int weeks { get; set; }
        public int months { get; set; }

        public int years { get; set; }
        public double holidayTime { get; set; }
        public double trainningTime { get; set; }
        public string roomName { get; set; }
        public string deviceName { get; set; }
    }
}
