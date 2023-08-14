using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.User;
using RW.PMS.Application.Contracts.Input.System;
using RW.PMS.Application.Contracts.System;
using RW.PMS.CrossCutting.Security.Encrypt;

namespace RW.PMS.API.Controllers;

public class LoginController : RWBaseController
{
    private readonly IUserService _userService;
    IConfiguration _configuration;

    public LoginController(IUserService userService, IConfiguration configuration)
    {
        _userService = userService;
        _configuration = configuration;
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
    /// <summary>
    /// 单点登录
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost("SSO")]
    [AllowAnonymous]
    public IResponseDto SsoLogin(SSOLoginDto input)
    {
        var sKey = _configuration["EncryptionKey:key"];
        string decryptStr = EncryptProvider.Decrypt(input.Username, sKey);
        input.Username = decryptStr;
        var token = _userService.SsoLogin(input);
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