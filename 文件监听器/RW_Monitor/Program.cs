using RW_Monitor.log;
using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RW_Monitor
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["SerilogRWPMS_MySQLServer"].ConnectionString;
            var tableName = "logdetails";

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Sink(new MySqlSink(connectionString, tableName))
                .CreateLogger();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
          //  log4net.Config.XmlConfigurator.Configure();
            Application.Run(new Form1());
        }
    }
}
