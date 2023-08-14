using System.ComponentModel.DataAnnotations;
using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;

namespace RW.PMS.Domain.Entities.System
{
    /// <summary>
    ///     操作实体（权限）
    /// </summary>
    [Table(Name = "sys_user_operation", OldName = "UserOperation")]
    public class UserOperationEntity
    {
        /// <summary>
        ///     用户Id
        /// </summary>
        [Column(IsPrimary = true)]
        public int UserId { get; set; }

        /// <summary>
        ///     操作编码
        /// </summary>
        [MaxLength(50)]
        [Column(IsPrimary = true)]
        public int OperationId { get; set; }

    }
}