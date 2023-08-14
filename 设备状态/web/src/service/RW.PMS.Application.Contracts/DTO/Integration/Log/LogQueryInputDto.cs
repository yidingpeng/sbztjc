using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Log
{
    public class LogQueryInputDto : PagedQuery
    {
        public string Account { get; set; }
        public string Type { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? To { get; set; }
    }
}
