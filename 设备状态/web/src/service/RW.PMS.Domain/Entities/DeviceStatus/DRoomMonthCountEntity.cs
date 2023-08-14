using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.DeviceStatus
{
    [Table(Name = "droom_droommonthcount")]

    public class DRoomMonthCountEntity : IEDRoomJiaoZhunTimeEntityntity<int>
    {
        public string roomName { get; set; }
        public double totalEffectiveRunningTime { get; set; }//总有效运行时间
        public double totalTestStopTime { get; set; }//总试验暂停时间
        public double totalFaultTime { get; set; }//总故障时间
        public double totalStandbyTime { get; set; }//总待机时间
        public double totalFreeTime { get; set; }//总闲置时间
        public double totalweibaoTime { get; set; }//总维保时间
        public double totalUtilizationRate { get; set; }//总稼动率
        public double deviceDebugRun { get; set; }

        public int months { get; set; }
        public int years { get; set; }
        public double holidayTime { get; set; }
        public double jiaozhunTime { get; set; }
        public double zhiduTime { get; set; }
    }
}
