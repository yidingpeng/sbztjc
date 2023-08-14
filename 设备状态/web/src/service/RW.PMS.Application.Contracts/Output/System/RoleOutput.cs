using System;

namespace RW.PMS.Application.Contracts.Output.System
{
    [Obsolete]
    public class RoleOutput
    {
        public int? Id { get; set; }

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
