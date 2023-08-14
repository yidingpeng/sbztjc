using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Stat
{
    public class StatDataDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Key { get; set; }
        public int Value { get; set; }
    }
}
