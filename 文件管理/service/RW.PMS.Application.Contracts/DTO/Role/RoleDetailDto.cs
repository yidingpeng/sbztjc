namespace RW.PMS.Application.Contracts.DTO.Role;

public class RoleDetailDto : RoleListDto
{
    /// <summary>
    ///     权限树列表
    /// </summary>
    public string[] TreeArray { get; set; }
}