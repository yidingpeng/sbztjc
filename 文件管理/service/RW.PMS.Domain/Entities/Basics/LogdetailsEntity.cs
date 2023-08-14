using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.Basics
{
    /// <summary>
    ///     日志信息表
    /// </summary>
    [Table(Name = "Logdetails")]
    public class LogdetailsEntity : FullEntity
    {
        /// <summary>
        /// 日志时间
        /// </summary>
        [MaxLength(150)]
        public DateTime LogDate { get; set; }
        /// <summary>
        /// 日志线程
        /// </summary>
        [MaxLength(100)]
        public string LogThread { get; set; }
        /// <summary>
        /// 日志等级
        /// </summary>
        [MaxLength(200)]
        public string LogLevel { get; set; }
        /// <summary>
        /// 日志记录
        /// </summary>
        [MaxLength(500)]
        public string LogLogger { get; set; }
        /// <summary>
        /// 日志信息
        /// </summary>
        [MaxLength(3000)]
        public string LogMessage { get; set; }
        /// <summary>
        /// 日志操作
        /// </summary>
        [MaxLength(4000)]
        public string LogActionClick { get; set; }
        /// <summary>
        /// 日志类别
        /// </summary>
        [MaxLength(30)]
        public string RequestType { get; set; }
        /// <summary>
        /// 日志文件路径
        /// </summary>
        [MaxLength(3000)]
        public string Path { get; set; }
    }
}
