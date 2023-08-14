using RW.PMS.Application.Contracts.DTO.Project;
using RW.PMS.Domain.Entities.Project;
using System.Collections.Generic;

namespace RW.PMS.Application.Contracts.Project
{
    public interface IContractDetailService : ICrudApplicationService<ContractDetailEntity, int>
    {
        /// <summary>
        /// 根据父级ID获取子集项目数据
        /// </summary>
        /// <param name="PID"></param>
        /// <returns></returns>
        List<ContractDetailDto> GetByPIDList(int? PID);
    }
}
