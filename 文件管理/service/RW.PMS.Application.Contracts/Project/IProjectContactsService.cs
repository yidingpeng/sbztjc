using RW.PMS.Application.Contracts;
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
    public interface IProjectContactsService: ICrudApplicationService<Project_ContactsInfo, int>
    {
        /// <summary>
        /// 项目联系人分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        PagedResult<projectcontactsListDto> GetPagedList(projectcontactsSearchDto input);

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        List<projectcontactsListDto> GetAllList();
        /// <summary>
        /// 添加项目联系人
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool Insert(projectcontactsListDto input);
        /// <summary>
        /// 修改项目联系人
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool Update(projectcontactsListDto input);

        /// <summary>
        /// 根据项目ID查询项目联系人
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<projectcontactsListDto> GetByIdList(int Id);

        /// <summary>
        /// 根据id查询项目联系人信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        List<projectcontactsListDto> GetConListById(int Id);

        /// <summary>
        /// 判断该项目下是否存在该负责板块的该联系人
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        bool IsExist(projectcontactsListDto input);
    }
}
