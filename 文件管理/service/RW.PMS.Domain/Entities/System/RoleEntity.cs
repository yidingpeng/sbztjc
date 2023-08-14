using System.ComponentModel.DataAnnotations;
using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;

namespace RW.PMS.Domain.Entities.System
{
    /// <summary>
    /// 角色实体
    /// </summary>
    [Table(Name = "sys_role", OldName = "Role")]
    public class RoleEntity : FullEntity
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        [MaxLength(50)]
        public string RoleName { get; set; }

        /// <summary>
        /// 角色说明
        /// </summary>
        [MaxLength(500)]
        public string Description { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 按钮权限
        /// </summary>
        public string BtnRolesCheckedList { get; set; }
    }
}
