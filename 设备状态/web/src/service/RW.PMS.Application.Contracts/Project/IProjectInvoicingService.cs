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
    public interface IProjectInvoicingService : ICrudApplicationService<ProjectInvoicingEntity, int>
    {
        /// <summary>
        /// 获取项目开票分页信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        PagedResult<ProjectInvoicingDto> GetPagedList(ProjectInvoicingSearchDto input);

        /// <summary>
        /// 防止重复项
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        bool IsExist(ProjectInvoicingDto input);

        /// <summary>
        /// 根据id获取数据
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        List<ProjectInvoicingDto> GetByIdList(int Id);

        /// <summary>
        /// 根据客户名称获取数据
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        List<ProjectInvoicingDto> GetListByClientId(int clientId);
    }
}
