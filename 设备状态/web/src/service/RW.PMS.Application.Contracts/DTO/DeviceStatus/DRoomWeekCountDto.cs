using MiniExcelLibs.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.DeviceStatus
{
    public class DRoomWeekCountDto
    {
        public int id { get; set; }
        [ExcelColumnName("试验台架")]
        public string roomName { get; set; }
        [ExcelColumnName("有效运行时间")]
        public double totalEffectiveRunningTime { get; set; }//总有效运行时间
        [ExcelColumnName("试验暂停时间")]
        public double totalTestStopTime { get; set; }//总试验暂停时间
        [ExcelColumnName("故障时间")]
        public double totalFaultTime { get; set; }//总故障时间
        [ExcelColumnName("待机时间")]
        public double totalStandbyTime { get; set; }//总待机时间
        [ExcelColumnName("闲置时间")]
        public double totalFreeTime { get; set; }//总闲置时间
        [ExcelColumnName("维保时间")]
        public double totalweibaoTime { get; set; }//总维保时间
        [ExcelColumnName("稼动率")]
        public double totalUtilizationRate { get; set; }//总稼动率
        [ExcelColumnName("调试运行时间")]
        public double deviceDebugRun { get; set; }
        [ExcelColumnName("周")]
        public int weeks { get; set; }
        [ExcelColumnName("月")]
        public int months { get; set; }
        [ExcelColumnName("年")]
        public int years { get; set; }
        [ExcelIgnore]
        public double holidayTime { get; set; }
        [ExcelIgnore]
        public double jiaozhunTime { get; set; }
        [ExcelIgnore]
        public double zhiduTime { get; set; }
    }
}
