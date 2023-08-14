using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Log
{
    public class LogOutputDto 
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public string Type { get; set; }

        public bool Result { get; set; }
        public string ExecuteResult { get; set; }

        public string Desc { get; set; }
        public string Ip { get; set; }
        public DateTime Datetime { get; set; }
    }
}
