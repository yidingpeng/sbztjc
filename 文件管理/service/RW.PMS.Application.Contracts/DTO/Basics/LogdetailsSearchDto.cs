using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Basics
{
    public class LogdetailsSearchDto : PagedQuery
    {
        /// <summary>
        /// 日志等级
        /// </summary>
        public string LogLevel { get; set; }
        /// <summary>
        /// 日志类别
        /// </summary>
        public string RequestType { get; set; }
        /// <summary>
        /// 日志时间
        /// </summary>
        public DateTime LogDate { get; set; }

        public string startTime { get; set; }
        public string endTime { get; set; }
    }
}
