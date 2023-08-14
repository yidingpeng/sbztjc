using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Log
{
    public class LogInputDto
    {
        public LogTypeEnum Type { get; set; }
        public bool Result { get; set; }
        public string Message { get; set; }
        public string Desc { get; set; }

    }

    public enum LogTypeEnum
    {
        None = 0,
        Login = 1,
        Operation = 2,
        Error = 3,
    }
}
