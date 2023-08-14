using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.DeviceStatus
{
    [Table(Name = "device_DevicestatusName")]
    public class DeviceStatusTagNameEntity : IEDRoomJiaoZhunTimeEntityntity<int>
    {

        public string DevicestatusTagName { get; set; }
      
    }
}
