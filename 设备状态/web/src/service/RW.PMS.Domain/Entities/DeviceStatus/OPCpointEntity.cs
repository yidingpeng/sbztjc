using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.DeviceStatus
{
    [Table(Name = "sys_pointinfo")]
    public class OPCpointEntity : IEDRoomJiaoZhunTimeEntityntity<int>
    {
        public string TagName { get; set; }
        public string Address { get; set; }
        public string ExplainInfo { get; set; }
       
        
    }
}
