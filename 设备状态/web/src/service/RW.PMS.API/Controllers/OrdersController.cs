using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.Orders;

namespace RW.PMS.API.Controllers
{
    public class OrdersController : RWBaseController
    {
        private readonly IOdersService _ordersService;
        public OrdersController( IOdersService ordersService)
        {
        
            _ordersService = ordersService;
        }
        [HttpGet("GetAllList")]
        public IResponseDto GetAllList([FromQuery] PagedQuery input)
        {
            var result = _ordersService.GetAllList(input);
            return ResponseDto.Success(result);
        }
    }
}
