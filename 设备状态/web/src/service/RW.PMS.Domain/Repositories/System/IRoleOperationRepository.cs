using System.Collections.Generic;
using FreeSql;
using RW.PMS.Domain.Entities.System;

namespace RW.PMS.Domain.Repositories.System;

/// <summary>
///     角色操作仓储基类
/// </summary>
public interface IRoleOperationRepository : IBaseRepository<RoleOperationEntity>
{
    /// <summary>
    ///     插入角色操作信息
    /// </summary>
    /// <param name="roleId">角色Id</param>
    /// <param name="operation">操作Id</param>
    /// <returns></returns>
    void Insert(int roleId, int[] operation);

    /// <summary>
    ///     根据角色获取操作编码
    /// </summary>
    /// <param name="roleId"></param>
    /// <returns></returns>
    List<string> GetOperationCodeForRole(int[] roleId);
}