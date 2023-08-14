namespace RW.PMS.Application.Contracts.DTO.Role;

public class RolePermissionDto
{
    /// <summary>
    ///     角色Id
    /// </summary>
    public int RoleId { get; set; }

    /// <summary>
    ///     模块Id
    /// </summary>
    public int[] Module { get; set; }

    /// <summary>
    ///     操作Id
    /// </summary>
    public int[] Operation { get; set; }
}