using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.User;
using RW.PMS.Application.Contracts.Input.System;
using RW.PMS.Application.Contracts.System;

namespace RW.PMS.API.Controllers;

public class LoginController : RWBaseController
{
    private readonly IUserService _userService;

    public LoginController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("Userinfo")]
    public IResponseDto UserInfo()
    {
        var result = _userService.GetUserInfo();
        return ResponseDto.Success(result);
    }

    [HttpPost]
    [AllowAnonymous]
    public IResponseDto Login(LoginDto input)
    {
        var token = _userService.Login(input);
        return ResponseDto.Success(token);
    }

    [HttpGet("logout")]
    [AllowAnonymous]
    public IResponseDto Logout()
    {
        _userService.Logout();
        return ResponseDto.Success();
    }
}