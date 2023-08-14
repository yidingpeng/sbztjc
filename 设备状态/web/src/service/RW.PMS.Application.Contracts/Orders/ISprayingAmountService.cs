using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Orders;
using RW.PMS.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.Orders
{
    public interface ISprayingAmountService:ICrudApplicationService<SprayingAmountEntity, int>
    {
        PagedResult<SprayingAmountDto> GetAllList(SprayingAmountSearchDto input);
        bool Update(SprayingAmountDto input);
        SprayingAmountEntity Insert(SprayingAmountDto input);
    }
}
