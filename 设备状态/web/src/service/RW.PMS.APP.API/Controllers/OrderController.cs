using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Orders;
using RW.PMS.Application.Contracts.Orders;
using RW.PMS.CrossCutting.Exceptions;

namespace RW.PMS.API.Controllers;

public class OrderController : RWBaseController
{
    private readonly IOdersService _ordersService;
    public OrderController(IOdersService ordersService)
    {

        _ordersService = ordersService;
    }
    [HttpPost("DoAdd")]
 
    public IResponseDto DoAdd([FromBody] OrdersDto input)
    {
        input.CreationTime = DateTime.Now;
        input.CurrentState = "0";
        input.IsDeleted = "0";

        
        var  entity = _ordersService.Insert(input);

        return entity.Id > 0 ? ResponseDto.Success("添加成功") : ResponseDto.Error(0, ExceptionCode.InsertFailed.ToString());
    }
}

