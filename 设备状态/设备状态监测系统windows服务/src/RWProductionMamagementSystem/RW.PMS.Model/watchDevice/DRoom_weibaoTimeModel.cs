using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Model.watchDevice
{
   public class DRoom_weibaoTimeModel
    {
        public int id { get; set; }
        public string deviceName { get; set; }
        public string roomName { get; set; }
        public DateTime createTime { get; set; }
        public double alarmTime { get; set; }
    }
}
