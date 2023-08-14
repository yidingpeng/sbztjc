using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Project;
using RW.PMS.Application.Contracts.Input.BaseInfo;
using RW.PMS.Application.Contracts.Output.Project;
using RW.PMS.Domain.Entities.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.Project
{
    public interface IProjectAcceptanceService : ICrudApplicationService<ProjectAcceptanceEntity, int>
    {
        /// <summary>
        /// 获取项目验收分页数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        PagedResult<ProjectAcceptanceDto> GetPagedList(ProjectAcceptanceSearchDto input);

        /// <summary>
        /// 根据id项目获取数据
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        List<ProjectAcceptanceDto> GetByIdList(int Id);

        /// <summary>
        /// 判断该项目验收数据是否已存在
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        bool IsExist(ProjectAcceptanceDto input);
    }

}
