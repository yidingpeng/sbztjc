using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Project;
using RW.PMS.Application.Contracts.DTO.Role;
using RW.PMS.Domain.Entities.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.Project
{
    public interface IContactsService: ICrudApplicationService<ContactsEntity, int>
    {
        /// <summary>
        /// 联系人分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        PagedResult<ContactListDto> GetPagedList(ContractSearchDto input);
        /// <summary>
        /// 添加联系人
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool Insert(ContactListDto input);
        /// <summary>
        /// 修改联系人
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool Update(ContactListDto input);

        /// <summary>
        /// 根据角色排列表格
        /// </summary>
        /// <param name="rolId"></param>
        /// <param name="PageNo"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        PagedResult<ContactListDto> GetPagedListByRol(string ContactsName, int rolId, int PageNo, int PageSize, string QueryType);

        /// <summary>
        /// 根据客户名称获取信息
        /// </summary>
        /// <param name="ClientId"></param>
        /// <returns></returns>
        List<ContactListDto> GetListByClientId(int ClientId);

        /// <summary>
        /// 根据id获取信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        List<ContactListDto> GetListById(int Id);
    }
}
