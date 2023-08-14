using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.System
{
    /// <summary>
    /// 日志
    /// </summary>
    [Table(Name = "sys_log")]
    public class LogEntity : IEDRoomJiaoZhunTimeEntityntity<int>
    {
        /// <summary>
        /// 日志的账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 日志类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 日志的结果，true表示正常，false表示异常
        /// </summary>
        public bool Result { get; set; }
        /// <summary>
        /// 执行结果信息
        /// </summary>
        [MaxLength(500)]
        public string ExecuteResult { get; set; }
        /// <summary>
        /// 详细信息，可能是错误详情
        /// </summary>
        [MaxLength(-1)]
        public string Desc { get; set; }
        /// <summary>
        /// 发生的IP地址
        /// </summary>
        public string Ip { get; set; }
        /// <summary>
        /// 发生时间
        /// </summary>
        public DateTime Datetime { get; set; }
    }
}
