using System.ComponentModel.DataAnnotations;
using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;

namespace RW.PMS.Domain.Entities.System
{
    /// <summary>
    ///     操作实体（权限）
    /// </summary>
    [Table(Name = "sys_operation", OldName = "Operation")]
    public class OperationEntity : Entity<int>
    {
        /// <summary>
        ///     模块Id
        /// </summary>
        public int ModuleId { get; set; }

        /// <summary>
        ///     操作编码
        /// </summary>
        [MaxLength(50)]
        public string OperationCode { get; set; }

        /// <summary>
        ///     标题
        /// </summary>
        [MaxLength(255)]
        public string Title { get; set; }
    }
}