using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Role
{
    public class RoleSearchDto : PagedQuery
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Role { get; set; }
    }
}
