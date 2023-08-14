using AutoMapper;
using RW.PMS.Application.Contracts.DTO.Orders;
using RW.PMS.Domain.Entities.Orders;
using RW.PMS.Domain.Entities.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.Mapper
{
    public class OrdersProfile : Profile
    {
        public OrdersProfile()
        {
            CreateMap<OrdersEntity, OrdersDto>();
            CreateMap<OrdersDto, OrdersEntity>();
            CreateMap<SprayingAmountEntity, SprayingAmountDto>();
            CreateMap<SprayingAmountDto, SprayingAmountEntity>();
        }
    }
}
