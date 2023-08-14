using FreeSql.DataAnnotations;

namespace RW.PMS.Domain.Entities.System
{
    /// <summary>
    ///     用户角色表
    /// </summary>
    [Table(Name = "sys_user_role", OldName = "UserRole")]
    public class UserRoleEntity
    {
        /// <summary>
        ///     用户Id
        /// </summary>
        [Column(IsPrimary = true)]
        public int UserId { get; set; }

        /// <summary>
        ///     角色Id
        /// </summary>
        [Column(IsPrimary = true)]
        public int RoleId { get; set; }
    }
}