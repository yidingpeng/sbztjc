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

    [HttpGet("GetUserId")]
    public IActionResult GetUserId()
    {
        var userId = _currentUser?.Id ?? 0;
        return new JsonResult(new {userId});
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
    /// 获取token
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetToken")]
    public IResponseDto GetToken()
    {
        var token = _userService.GetToken();
        return ResponseDto.Success(token);
    }
    /// <summary>
    /// 账号获取用户信息
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetAccountInfo")]
    public IResponseDto GetAccountInfo(string Account)
    {
        var info = _userService.GetUserTelByAccount(Account);
        return ResponseDto.Success(info);
    }

}