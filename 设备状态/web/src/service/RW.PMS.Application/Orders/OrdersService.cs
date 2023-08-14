using AutoMapper;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Orders;
using RW.PMS.Application.Contracts.Orders;
using RW.PMS.Application.Contracts.System;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.EventBus;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.Orders;
using RW.PMS.Domain.Entities.System;
using RW.PMS.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Orders
{
    internal class OrdersService : CrudApplicationService<OrdersEntity, int>, IOdersService
    {
        private readonly IEventBus _eventBus;
        private readonly ILogService _log;
        private readonly IRepository<ModuleEntity, int> _moduleRepository;
        
        public OrdersService(IDataValidatorProvider dataValidator,
       IRepository<OrdersEntity, int> roleRepository,
       IMapper mapper,
       Lazy<ICurrentUser> currentUser,
       IRepository<ModuleEntity, int> moduleRepository,
       IEventBus eventBus,
       ILogService log) : base(dataValidator,
       roleRepository, mapper, currentUser)
        {
         
            _moduleRepository = moduleRepository;
            _eventBus = eventBus;
            _log = log;
           
        }
        public PagedResult<OrdersDto> GetAllList(PagedQuery input)
        {
            var list = Repository.Select.Count(out var total).Page(input.PageNo, input.PageSize).OrderByDescending(t=>t.Id).ToList()
           .Select(t => Mapper.Map<OrdersDto>(t)).ToList();
            return new PagedResult<OrdersDto>(list, total);
            

        }
    }
}
