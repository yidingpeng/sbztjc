using System.Collections.Generic;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Module;
using RW.PMS.Application.Contracts.Output.System;
using RW.PMS.Domain.Entities.System;

namespace RW.PMS.Application.Contracts.System;

public interface IModuleService : ICrudApplicationService<ModuleEntity, int>
{
    /// <summary>
    ///     获取路由列表
    /// </summary>
    /// <returns></returns>
    IList<RouterOutput> GetRouter();

    /// <summary>
    ///     获取树形列表
    /// </summary>
    /// <returns></returns>
    IList<ModuleListDto> GetTreeList();

    /// <summary>
    ///     获取角色权限树形列表
    /// </summary>
    /// <param name="roleId">角色Id</param>
    /// <returns></returns>
    IList<ModuleWithOperationDto> GetTreeListForRole(int roleId);

    /// <summary>
    ///     获取用户权限树列表
    /// </summary>
    /// <param name="userId">用户Id</param>
    /// <returns></returns>
    IList<ModuleWithOperationDto> GetTreeListForUser(int userId);

    /// <summary>
    ///     获取操作列表
    /// </summary>
    /// <param name="moduleId">模块Id</param>
    /// <returns></returns>
    IList<OperationListDto> GetOperation(int moduleId);

    /// <summary>
    /// 更新批量数据，并使得指定数据后移
    /// </summary>
    int Sort(SortDto dto);

    /// <summary>
    /// 更新单个位置数据
    /// </summary>
    bool UpdateSort(SortDto dto);
    /// <summary>
    /// 指定用户是否拥有权限
    /// </summary>
    bool HasModuleOperation(int userId, string operation);
    bool HasModuleOperation(string operation);
}