using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.DeviceFile
{
    [Table(Name = "droom_TestRoom")]
    public class TestRoomEntity : Entity<int>
    {
        public string roomName { get; set; }//台架名称
    }
}
