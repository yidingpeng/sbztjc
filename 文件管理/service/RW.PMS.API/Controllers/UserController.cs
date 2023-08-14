using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.User;
using RW.PMS.Application.Contracts.System;
using RW.PMS.CrossCutting.Security.User;

namespace RW.PMS.API.Controllers;

public class UserController : RWBaseController
{
    public readonly ICurrentUser _currentUser;
    private readonly IModuleService _moduleService;
    private readonly IUserService _userService;

    public UserController(IUserService userService, ICurrentUser currentUser, IModuleService moduleService)
    {
        _userService = userService;
        _currentUser = currentUser;
        _moduleService = moduleService;
    }

    [HttpGet]
    public IResponseDto GetList([FromQuery] UserSearchDto search)
    {
        var result = _userService.GetPagedList(search);
        return ResponseDto.Success(result);
    }

    [HttpGet("personal")]
    public IResponseDto GetPersonal()
    {
        var user = _userService.GetPersonal();
        return ResponseDto.Success(user);
    }

    [HttpPut("personal")]
    public IResponseDto SavePersonal(PersonalInputDto input)
    {
        var result = _userService.SavePersonal(input);
        return ResponseDto.Success(result ? "保存个人信息成功！" : "保存个人信息失败！");
    }

    [HttpPost]
    public IResponseDto DoAdd([FromBody] UserDetailDto input)
    {
        var IsExist = _userService.AccountExist(input);
        if (IsExist) return ResponseDto.Error(500, "该用户名已存在");
        var result = _userService.Insert(input);
        return ResponseDto.Success("添加成功！");
    }

    [HttpPut]
    public IResponseDto DoEdit([FromBody] UserListDto input)
    {
        var IsExist = _userService.AccountExist(new UserDetailDto {Id = input.Id, Account = input.Account});
        if (IsExist) return ResponseDto.Error(500, "该用户名已存在");
        if (input.Id <= 0) return ResponseDto.Error(500, "ID不能为空！");
        _userService.Update(input);
        return ResponseDto.Success("修改成功！");
    }

    [HttpDelete]
    public IResponseDto DoDelete([FromBody] DeleteRequesDto model)
    {
        var ids = model.GetIds();
        if (ids.Length == 0) return ResponseDto.Error(500, "未指定删除的ID。");
        var count = _userService.Delete(ids);
        return ResponseDto.Success($"成功删除了{count}条数据。");
    }

    [HttpPut("ResetPwd")]
    public IResponseDto DoResetPwd(ResetPwdDto input)
    {
        var result = _userService.ResetPassword(input);
        return ResponseDto.Success("成功重置了密码。");
    }

    [HttpPut("password")]
    public IResponseDto ChangePwd(ChangePwdDto input)
    {
        var result = _userService.ChangePassword(input);
        return ResponseDto.Success("密码修改成功。");
    }

    [HttpGet("GetUserId")]
    public IActionResult GetUserId()
    {
        var userId = _currentUser?.Id ?? 0;
        return new JsonResult(new {userId});
    }

    [HttpGet("GetUserInfoById")]
    public IResponseDto GetUserInfoById(int id)
    {
        var result = _userService.GetUserInfoById(id);
        return ResponseDto.Success(result);
    }

    [HttpGet("Permission")]
    public IResponseDto GetPermission(int userId)
    {
        var treeList = _moduleService.GetTreeListForUser(userId);
        return ResponseDto.Success(treeList);
    }
    /// <summary>
    /// 获取所有用户
    /// </summary>
    /// <returns></returns>
    [HttpGet("UserList")]
    public IResponseDto GetUserList()
    {
        var List = _userService.GetList();
        return ResponseDto.Success(List);
    }

    /// <summary>
    /// 通过账号获取用户
    /// </summary>
    /// <returns></returns>
    [HttpGet("UserListByAccount")]
    public IResponseDto GetUserLByAccount(string account)
    {
        var List = _userService.GetList().Where(o => o.Account == account).FirstOrDefault();
        return ResponseDto.Success(List);
    }
    /// <summary>
    /// 通过当前登录人获取用户
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetUserById")]
    public IResponseDto GetUserById()
    {
        var info = _userService.GetUserById();
        return ResponseDto.Success(info);
    }
}