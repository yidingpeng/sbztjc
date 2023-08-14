using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Role;
using RW.PMS.Application.Contracts.System;

namespace RW.PMS.API.Controllers;

public class RoleController : RWBaseController
{
    private readonly IRoleService _roleService;
    private readonly IModuleService _moduleService;
    public RoleController(IRoleService roleService, IModuleService moduleService)
    {
        _roleService = roleService;
        _moduleService = moduleService;
    }

    [HttpGet]
    public IResponseDto GetList([FromQuery] RoleSearchDto input)
    {
        var result = _roleService.GetPagedList(input);
        return ResponseDto.Success(result);
    }
    /// <summary>
    /// 查询所有角色信息
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetAllList")]
    public IResponseDto GetAllList()
    {
        var result = _roleService.GetAllList();
        return ResponseDto.Success(result);
    }

    [HttpGet("Modules")]
    public IResponseDto GetModuleList(int roleId)
    {
        var result = _roleService.GetRoleAuth(roleId);
        return ResponseDto.Success(result);
    }

    [HttpPost]
    public IResponseDto DoAdd([FromBody] RoleDetailDto input)
    {
        var list = _roleService.GetList().Where(s => s.RoleName.Equals(input.Role) && s.Id != input.Id && s.IsDeleted == false).Count();
        if (list > 0) return ResponseDto.Error(500, "角色名已存在！");
        var result = _roleService.Insert(input);
        return ResponseDto.Success("添加成功！");
    }

    [HttpPut]
    public IResponseDto DoEdit([FromBody] RoleDetailDto input)
    {
        if (input.Id <= 0) return ResponseDto.Error(500, "ID不能为空！");
        var list = _roleService.GetList().Where(s => s.RoleName.Equals(input.Role) && s.Id != input.Id && s.IsDeleted == false).Count();
        if (list > 0) return ResponseDto.Error(500, "角色名已存在！");
        _roleService.Update(input);
        return ResponseDto.Success("修改成功！");
    }

    [HttpDelete]
    public IResponseDto DoDelete([FromBody] DeleteRequesDto model)
    {
        var ids = model.GetIds();
        if (ids.Length == 0) return ResponseDto.Error(500, "未指定删除的ID。");
        int count = _roleService.Delete(ids);
        return ResponseDto.Success($"成功删除了{count}条数据。");
    }

    [HttpGet("Permission")]
    public IResponseDto GetPermission(int roleId)
    {
        var treeList = _moduleService.GetTreeListForRole(roleId);
        return ResponseDto.Success(treeList);
    }

    [HttpPut("Permission")]
    public IResponseDto UpdatePermission(RolePermissionDto dto)
    {
        _roleService.UpdateRolePermission(dto);
        return ResponseDto.Success("保存成功");
    }
}
