using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Role
{
    public class RoleListDto
    {
        public int Id { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// 角色说明
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 权限菜单
        /// 'read:system', 'write:system', 'delete:system'
        /// </summary>
        public string[] BtnRolesCheckedList { get; set; }
    }
}
