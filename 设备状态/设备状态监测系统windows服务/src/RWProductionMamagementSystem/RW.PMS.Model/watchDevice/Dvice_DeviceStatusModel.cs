using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Model
{
    public class Dvice_DeviceStatusModel
    {



        public int id { get; set; }

        public string deviceName { get; set; }//设备名称

        public string operationStatus { get; set; }//设备运行状态
        public double totalRunTime { get; set; }//总运行时间
        public double totalStopTime { get; set; }//总停机时间
        public double totalFaultDownTime { get; set; }//总故障停机时间
        public int toalFaultNumber { get; set; }//总故障次数
        public double totalFreeTime { get; set; }//总闲置时间

        public double weibaoTime { get; set; }//总维保时间

        public int roomId { get; set; }//试验间id
        public int isClear { get; set; }//是否清零
    }
}
