using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.User
{
    public class ChangePwdDto
    {
        public string Old { get; set; }
        public string New { get; set; }
        public string Confirm { get; set; }
    }
}
