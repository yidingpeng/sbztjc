using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Role;
using RW.PMS.Application.Contracts.Input.System;
using RW.PMS.Application.Contracts.Output.System;
using RW.PMS.CrossCutting.Security.Authorization;
using RW.PMS.Domain.Entities.System;
using System.Collections.Generic;

namespace RW.PMS.Application.Contracts.System
{
    public interface IRoleService : ICrudApplicationService<RoleEntity, int>
    {
        /// <summary>
        ///     查询角色分页数据
        /// </summary>
        /// <param name="input">查询参数</param>
        /// <returns></returns>
        PagedResult<RoleListDto> GetPagedList(RoleSearchDto input);

        /// <summary>
        /// 查询所有角色信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<RoleListDto> GetAllList();

        /// <summary>
        ///     变更启用状态
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="isActive">启用状态</param>
        /// <returns></returns>
        int ChangeActive(int id, bool isActive);

        /// <summary>
        ///     角色授权
        /// </summary>
        bool RoleAuth(int roleId, string[] operation);

        /// <summary>
        /// 添加角色并授权
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        RoleEntity Insert(RoleDetailDto input);
        /// <summary>
        /// 修改角色，并授权
        /// </summary>
        bool Update(RoleDetailDto input);

        /// <summary>
        ///     检查角色是否拥有模块权限
        /// </summary>
        /// <param name="roles">角色Id数组</param>
        /// <param name="module">模块编码</param>
        /// <param name="operation">权限类型</param>
        /// <returns></returns>
        bool Check(int[] roles, string module, OperationType operation);

        /// <summary>
        ///     判断角色名是否存在
        /// </summary>
        bool NameExist(string name);

        /// <summary>
        /// 获取指定权限下的所有菜单
        /// </summary>
        PagedResult<string> GetRoleAuth(int roleId);

        /// <summary>
        ///     更新角色权限
        /// </summary>
        /// <param name="dto">输入参数<see cref="RolePermissionDto"/></param>
        void UpdateRolePermission(RolePermissionDto dto);
    }
}