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
    public interface IProTemplateService : ICrudApplicationService<projectTemplateEntity, int>
    {
        /// <summary>
        /// 项目模板分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        PagedResult<projectTemplateView> ProTemplatePagedList(projectTemplateSearchDto input);

        ///// <summary>
        ///// 模板信息新增
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //bool Insert(projectTemplateView input);

        ///// <summary>
        ///// 模板信息修改
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //bool Update(projectTemplateView input);

        /// <summary>
        /// 模板名唯一
        /// </summary>
        /// <param name="ProjectCode"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        bool ProOnly(string Name, int Id);
        /// <summary>
        /// 项目模板修改状态
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        bool updateState(int Id);
    }
}
