using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.DeviceFile
{
    [Table(Name = "device_Device")]
    public class DeviceEntity:Entity<int>
    {
        public string deviceName { get; set; }//设备名称

      

        public int roomId { get; set; }//试验间id
       
    }
}
