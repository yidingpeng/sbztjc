using MiniExcelLibs.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Log
{
    public class LogOutputDto 
    {
        [ExcelColumnName("id编号")]
        public int Id { get; set; }
        [ExcelIgnore]
        public string Account { get; set; }

        [ExcelColumnName("日志类型")]
        public string Type { get; set; }
        [ExcelIgnore]
        public bool Result { get; set; }

        [ExcelColumnName("执行结果")]
        public string ExecuteResult { get; set; }
        [ExcelColumnName("日志详情")]
        public string Desc { get; set; }
        [ExcelColumnName("IP地址")]
        public string Ip { get; set; }
        [ExcelColumnName("时间")]
        public DateTime Datetime { get; set; }
    }
}
