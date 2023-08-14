using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.DeviceFile
{
    public class DeviceTestSheetTimeCountDto
    {
        public int id { get; set; }
        public string testName { get; set; }
        public string testNumber { get; set; }
        public string testType { get; set; }
        public double orderTypeTime { get; set; }
        public double operationTestTime { get; set; }
        public double abnormalTime { get; set; }
        public double waitMaterialTime { get; set; }
        public double waitManageTime { get; set; }
        public DateTime alarmTime { get; set; }
    }
}
