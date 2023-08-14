using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.User
{
    public class UserSearchDto : PagedQuery
    {
        public string Username { get; set; }

        public int Organization { get; set; }
    }
}
