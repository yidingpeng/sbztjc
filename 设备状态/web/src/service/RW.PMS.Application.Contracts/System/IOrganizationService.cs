using System.Collections.Generic;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Organization;
using RW.PMS.Application.Contracts.Input.System;
using RW.PMS.Application.Contracts.Output.System;
using RW.PMS.Domain.Entities.System;

namespace RW.PMS.Application.Contracts.System
{
    public interface IOrganizationService : ICrudApplicationService<OrganizationEntity, int>
    {
        /// <summary>
        ///  根据条件查询机构实体集合 分页
        /// </summary>
        /// <returns></returns>
        List<OrganizationListDto> GetTreeList(OrganizationSearchDto search);

        int addNewOrgnization(string OrgName);

        /// <summary>
        /// 判断该部门编码是否已存在
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        bool OrganizationCodeExist(OrganizationListDto input);

        /// <summary>
        /// 根据父项查询子项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<OrganizationListDto> GetByParentIdList(int id);
    }
}