using AutoMapper;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Orders;
using RW.PMS.Application.Contracts.Orders;
using RW.PMS.Application.Contracts.System;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.Extensions;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.Orders;
using RW.PMS.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Orders
{
    public class SprayingAmountService : CrudApplicationService<SprayingAmountEntity, int>, ISprayingAmountService
    {
        private readonly ILogService _log;
        public SprayingAmountService(IDataValidatorProvider dataValidator, IRepository<SprayingAmountEntity, int> repository, ILogService log, IMapper mapper, Lazy<ICurrentUser> currentUser) : base(dataValidator, repository, mapper, currentUser)
        {
            _log = log;
        }

        public PagedResult<SprayingAmountDto> GetAllList(SprayingAmountSearchDto input)
        {
            var list = Repository. WhereIf(input.oAxleModel.NotNullOrWhiteSpace(), t => t.oAxleModel.Contains(input.oAxleModel)).Count(out var total).Page(input.PageNo, input.PageSize).OrderByDescending(t => t.Id).ToList()
        .Select(t => Mapper.Map<SprayingAmountDto>(t)).ToList();
            return new PagedResult<SprayingAmountDto>(list, total);
        }

        public bool Update(SprayingAmountDto input)
        {
            var result = base.Update(input.id, input) > 0;
            
            _log.AddOperationLog(true, "更新成功", $"更新了喷漆量[{input.sprayingParameter}]");
            return result;
        }
        public SprayingAmountEntity Insert(SprayingAmountDto input)
        {
            var result = base.Insert(input);           
            _log.AddOperationLog(true, "添加成功", $"添加了车轴型号及喷涂量参数[{input.oAxleModel}]及喷涂量参数[{input.sprayingParameter}]");
            return result;
        }
       
    }
}
