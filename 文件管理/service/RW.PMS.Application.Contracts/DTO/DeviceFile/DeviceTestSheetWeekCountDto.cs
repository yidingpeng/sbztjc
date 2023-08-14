using MiniExcelLibs.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.DeviceFile
{
    public class DeviceTestSheetWeekCountDto
    {
        public int id { get; set; }
        [ExcelColumnName("试验台架")]
        public string testName { get; set; }
        [ExcelColumnName("实验单次数")]
        public int testNumber { get; set; }
        [ExcelColumnName("任务单时间")]
        public double orderTypeTime { get; set; }
        [ExcelColumnName("运行试验时间")]
        public double operationTestTime { get; set; }
        [ExcelColumnName("异常时间")]
        public double abnormalTime { get; set; }
        [ExcelColumnName("等待物料时间")]
        public double waitMaterialTime { get; set; }
        [ExcelColumnName("等待试验经理时间")]
        public double waitManageTime { get; set; }
        [ExcelColumnName("周")]
        public int weeks { get; set; }
        [ExcelColumnName("月")]
        public int months { get; set; }
        [ExcelColumnName("年")]
        public int years { get; set; }
    }
}
