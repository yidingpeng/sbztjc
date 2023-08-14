using log4net;
using log4net.Config;
using System;
using System.IO;

namespace RW_Monitor.Log4net
{
    public class LogHelper
    {
        public static readonly LogHelper Instance = new LogHelper();
        private static readonly log4net.ILog loginfo = log4net.LogManager.GetLogger("loginfo");
        //public static log4net.ILog loginfo = LogManager.GetLogger("loginfo");

        public LogHelper()
        {
            string text = "";
            text = System.AppDomain.CurrentDomain.BaseDirectory + "config\\Log4Net.config";
            XmlConfigurator.ConfigureAndWatch(new FileInfo(text));
        }

        private static ActionLoggerInfo _message = null;
        private static log4net.ILog _log;

        public static log4net.ILog Log
        {
            get
            {
                if (_log == null)
                {
                    _log = LogManager.GetLogger("OperateLogger");
                }
                return _log;
            }
        }

        public static void Debug()
        {
            if (loginfo.IsDebugEnabled)
            {
                loginfo.Debug(_message);
            }
        }
        public static void Error()
        {
            if (loginfo.IsErrorEnabled)
            {
                loginfo.Error(_message);
            }
        }
        public static void Fatal()
        {
            if (loginfo.IsFatalEnabled)
            {
                loginfo.Fatal(_message);
            }
        }
        public static void Info()
        {
            if (loginfo.IsInfoEnabled)
            {
                loginfo.Info(_message);
            }
        }
        public static void Warn()
        {
            try
            {
                if (loginfo.IsWarnEnabled)
                {
                    loginfo.Warn(_message);
                }
            }
            catch (Exception e)
            {
                var t = e;
            }
        }

        /// <summary>
        /// log4net日志保存到数据库
        /// </summary>
        /// <param name="requesttype">请求类型（自己定义）</param>
        /// <param name="path">文件链接（路径）</param>
        /// <param name="message">文件名称或报错信息</param>
        /// <param name="level">日志等级1:通常信息 2:警告 3:错误 4:致命的错误</param>
        public static void SaveMessage(string requesttype, string path, string message, int level)
        {
            _message = new ActionLoggerInfo(requesttype, path, message);
            switch (level)
            {
                case 1: Info(); break;
                case 2: Warn(); break;
                case 3: Error(); break;
                case 4: Fatal(); break;
                default: break;
            }
        }
    }
}
