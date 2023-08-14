using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.DeviceFile
{
    [Table(Name = "device_testsheetyearcount")]
    public class DeviceTestSheetYearCountEntity : Entity<int>
    {
        public string testName { get; set; }
        public int testNumber { get; set; }

        public double orderTypeTime { get; set; }
        public double operationTestTime { get; set; }
        public double abnormalTime { get; set; }
        public double waitMaterialTime { get; set; }
        public double waitManageTime { get; set; }


        public int years { get; set; }
    }
}
