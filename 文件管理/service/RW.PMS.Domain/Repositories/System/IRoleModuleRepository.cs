using FreeSql;
using RW.PMS.Domain.Entities.System;

namespace RW.PMS.Domain.Repositories.System;

/// <summary>
///     角色模块仓储基类
/// </summary>
public interface IRoleModuleRepository: IBaseRepository<RoleModuleEntity>
{
    /// <summary>
    ///     批量插入角色模块数据
    /// </summary>
    /// <param name="roleId">角色Id</param>
    /// <param name="module">模块Id</param>
    void Insert(int roleId, int[] module);
}