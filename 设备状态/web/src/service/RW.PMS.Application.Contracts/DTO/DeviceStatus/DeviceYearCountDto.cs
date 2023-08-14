using MiniExcelLibs.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.DeviceStatus
{
    public class DeviceYearCountDto
    {
        public int id { get; set; }
        [ExcelColumnName("运行时间")]
        public double totalRunTime { get; set; }//总运行时间
        [ExcelColumnName("停机时间")]
        public double totalStopTime { get; set; }//总停机时间
        [ExcelColumnName("故障时间")]
        public double totalFaultDownTime { get; set; }//总故障停机时间
        [ExcelColumnName("故障次数")]
        public int toalFaultNumber { get; set; }//总故障次数
        [ExcelColumnName("闲置时间")]
        public double totalFreeTime { get; set; }//总闲置时间
        [ExcelColumnName("维保时间")]

        public double weibaoTime { get; set; }//总维保时间
        [ExcelColumnName("年")]

        public int years { get; set; }
        [ExcelIgnore]
        public double holidayTime { get; set; }
        [ExcelIgnore]
        public double trainningTime { get; set; }
        [ExcelColumnName("试验台架")]
        public string roomName { get; set; }
        [ExcelColumnName("设备名称")]
        public string deviceName { get; set; }
    }
}
