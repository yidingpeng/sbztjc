using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.DeviceFile
{
    [Table(Name = "device_TestSheetTimeCount")]
    public class DeviceTestSheetTimeCountEntity : Entity<int>
    {
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
