using RW_Monitor.Log4net;
using Core.Model;
using Core.MySqlSelect;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Serilog;
using RW_Monitor.log;

namespace RW_Monitor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                //LogHelper.SaveMessage("程序启动", "push", "文件监听程序启动!", 1);

                #region 启动时最小化到右下角托盘
                this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
                notifyIcon1.Icon = new Icon("image/RW.ico");//指定一个图标　　　
                notifyIcon1.Visible = false;
                notifyIcon1.Click += new System.EventHandler(this.notifyIcon1_Click);
                // this.SizeChanged += new System.EventHandler(this.MainWnd_SizeChanged);
                //自动最小化，因为添加了notifyIcon，最小化时不会最小化到任务栏，而是到托盘　　　　
                this.WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;//任务栏里不显示任务
                this.notifyIcon1.Visible = true;//这个必须为true
                #endregion

                Sql sql = new Sql();
                List<MonitorConfig> ModelList = sql.GetMonitorPush();//获取配置的文件夹监听路径参数
                List<string> FilePush = new List<string>();

                if (ModelList != null && ModelList.Count > 0)
                {
                    foreach (var item in ModelList)
                    {
                        FilePush.Add(item.Path);
                    }
                }
                FileMonitorHelp fileMonitor = new FileMonitorHelp(FilePush);
                fileMonitor.Start();
            }
            catch (Exception ex)
            {
              //  LogHelper.SaveMessage("程序测试", "", "致命错误日志测试"+ex.Message, 4);
                insertlog("程序测试", "", "致命错误日志测试" + ex.Message, 4);
            }
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
                this.WindowState = FormWindowState.Normal;
            this.Activate();
            this.notifyIcon1.Visible = false;
            this.ShowInTaskbar = true;
        }

        public void insertlog(string requesttype, string path, string message, int level)
        {
            var logger = Log.ForContext<CustomLogEvent>();

            var customLog = new CustomLogEvent
            {
                LogDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                LogThread = "11",
                LogLevel = "INFO",
                LogLogger = "loginfo",
                LogMessage = message,
                LogActionClick = "",
                RequestType = requesttype,
                Path = path,
                CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                LastModifiedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };

            logger.Information("This is a custom log message {@CustomLogEvent}", customLog);
        }

    }
}
