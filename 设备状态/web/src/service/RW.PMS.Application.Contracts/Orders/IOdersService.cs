using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Orders;
using RW.PMS.Domain.Entities.Orders;
using RW.PMS.Domain.Entities.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.Orders
{
    public interface IOdersService : ICrudApplicationService<OrdersEntity, int>
    {
        PagedResult<OrdersDto> GetAllList(PagedQuery input);
    }
}
