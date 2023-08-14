using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.WorkerService
{
    public class WriteLogFile
    {
        public void WriteLog(string str)
        {
            using System.IO.StreamWriter streamWriter = new("D:\\log.txt", true);

            streamWriter.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss  ffff") + "   ====   " + str);
        }
    }
}
