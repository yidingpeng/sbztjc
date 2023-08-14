using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Project;
using RW.PMS.Domain.Entities.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.Project
{
    public interface IProjectDeliveryService: ICrudApplicationService<Project_DeliveryEntity, int>
    {
        PagedResult<Project_DeliveryDto> GetPagedList(Project_DeliverySearchDto input);
        /// <summary>
        /// 根据ID获取交付信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<Project_DeliveryDto> GetByIdList(int Id);

        /// <summary>
        /// 判断项目交付编号是否存在
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        bool IsExist(Project_DeliveryDto input);

    }
}
