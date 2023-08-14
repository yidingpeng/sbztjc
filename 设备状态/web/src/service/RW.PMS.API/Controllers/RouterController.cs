using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.System;

namespace RW.PMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RouterController : RWBaseController
{
    private readonly IModuleService _moduleService;

    public RouterController(IModuleService moduleService)
    {
        _moduleService = moduleService;
    }

    [HttpGet]
    public IResponseDto GetList()
    {
        var router = _moduleService.GetRouter();
        return ResponseDto.Success(new { List = router });
    }
}