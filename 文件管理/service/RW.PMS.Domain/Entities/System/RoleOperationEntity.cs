using FreeSql.DataAnnotations;

namespace RW.PMS.Domain.Entities.System
{
    /// <summary>
    ///     角色操作表
    /// </summary>
    [Table(Name = "sys_role_operation", OldName = "RoleOperation")]
    public class RoleOperationEntity
    {
        /// <summary>
        ///     角色Id
        /// </summary>
        [Column(IsPrimary = true)]
        public int RoleId { get; set; }

        /// <summary>
        ///     操作Id
        /// </summary>
        [Column(IsPrimary = true)]
        public int OperationId { get; set; }
    }
}