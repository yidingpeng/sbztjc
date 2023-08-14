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
    public interface IProjectDynamicService : ICrudApplicationService<ProjectDynamicEntity, int>
    {
        /// <summary>
        /// 项目动态分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        PagedResult<ProjectDynamicView> GetPagedList(ProjectDynamicSearchDto input);
        /// <summary>
        /// 项目动态List
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<ProjectDynamicView> GetList(int Id);

        /// <summary>
        /// 项目动态新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        bool Insert(ProjectDynamicView input);

        /// <summary>
        /// 项目动态修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        bool Update(ProjectDynamicView input);
    }
}
