using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.DeviceFile
{
    [Table(Name = "device_TestSheet")]
    public class TestSheetEntity:Entity<int>
    {
        public string testNumber { get; set; }
        public string roomName { get; set; }
        public string orderType { get; set; }
        public DateTime orderTypeStartTime { get; set; }
        public DateTime orderTypeEndTime { get; set; }
        public DateTime operationTestStartTime { get; set; }
        public DateTime operationTestEndTime { get; set; }
        public DateTime abnormalStartTime { get; set; }
        public DateTime abnormalEndTime { get; set; }
        public DateTime waitMaterialStartTime { get; set; }
        public DateTime waitMaterialEndTime { get; set; }
        public DateTime waitManageStartTime { get; set; }
        public DateTime waitManageEndTime { get; set; }
        public string remark { get; set; }
    }
}
