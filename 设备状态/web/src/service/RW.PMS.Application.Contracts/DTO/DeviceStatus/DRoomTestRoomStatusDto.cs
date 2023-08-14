using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.DeviceStatus
{
    public  class DRoomTestRoomStatusDto : PagedQuery
    
    {
        public int id { get; set; }
        public string roomName { get; set; }//台架名称
        public string operationStatus { get; set; }//运行状态
        public double totalEffectiveRunningTime { get; set; }//总有效运行时间
        public double totalTestStopTime { get; set; }//总试验暂停时间
        public double totalFaultTime { get; set; }//总故障时间
        public double totalStandbyTime { get; set; }//总待机时间
        public double totalFreeTime { get; set; }//总闲置时间
        public double totalweibaoTime { get; set; }//总维保时间
        public double totalUtilizationRate { get; set; }//总稼动率
        public double totalDeviceDebugRunTime { get; set; }
        public int isClear { get; set; }//是否清零

    }
}
