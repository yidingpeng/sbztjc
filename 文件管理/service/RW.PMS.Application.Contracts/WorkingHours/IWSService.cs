using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.WorkingHours;
using RW.PMS.Domain.Entities.WorkingHours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.WorkingHours
{
    public interface IWSService : ICrudApplicationService<WHDetailEntity, int>
    {
        /// <summary>
        /// 工时信息分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        PagedResult<WHDetailDto> GetPageList(WHDetailSearchDto input);
    }
}
