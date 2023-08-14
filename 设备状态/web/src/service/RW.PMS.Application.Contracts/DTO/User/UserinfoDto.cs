using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.User
{
    public class UserinfoDto
    {
        public string Avatar { get; set; }
        public string[] Permissions { get; set; }
        public string[] Roles { get; set; }
        public string Username { get; set; }
    }
}
