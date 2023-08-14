using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Basics
{
    /// <summary>
    /// 日志信息表
    /// </summary>
    public class LogdetailsDto
    {
        //public int LogID { get; set; }
        /// <summary>
        /// 日志时间
        /// </summary>
        public DateTime LogDate { get; set; }
        /// <summary>
        /// 日志线程
        /// </summary>
        public string LogThread { get; set; }
        /// <summary>
        /// 日志等级
        /// </summary>
        public string LogLevel { get; set; }
        /// <summary>
        /// 日志记录
        /// </summary>
        public string LogLogger { get; set; }
        /// <summary>
        /// 日志信息
        /// </summary>
        public string LogMessage { get; set; }
        /// <summary>
        /// 日志操作
        /// </summary>
        public string LogActionClick { get; set; }
        /// <summary>
        /// 日志类别
        /// </summary>
        public string RequestType { get; set; }
        /// <summary>
        /// 日志文件路径
        /// </summary>
        public string Path { get; set; }
    }
}
