using RW.PMS.Application.Contracts.DTO;
using System;

namespace RW.PMS.Application.Contracts.Input.System
{
    [Obsolete]
    public class RoleSearchInput : PagedQuery
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Role { get; set; }
    }
}