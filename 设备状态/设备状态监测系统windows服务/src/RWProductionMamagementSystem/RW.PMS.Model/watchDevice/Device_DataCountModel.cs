using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Model.watchDevice
{
    public class Device_DataCountModel
    {

        public int id { get; set; }
        public double runTimeCount { get; set; }
        public double stopTimeCount { get; set; }
        public int faultNumberCount { get; set; }
        public string deviceName { get; set; }
        public string roomName { get; set; }
        public string week { get; set; }
        public string month { get; set; }
        public string year { get; set; }

    }
}
