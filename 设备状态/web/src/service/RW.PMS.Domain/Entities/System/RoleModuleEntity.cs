using FreeSql.DataAnnotations;

namespace RW.PMS.Domain.Entities.System;

/// <summary>
///     角色菜单
/// </summary>
[Table(Name = "sys_role_module", OldName = "RoleModule")]
public class RoleModuleEntity
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
    public int ModuleId { get; set; }
}