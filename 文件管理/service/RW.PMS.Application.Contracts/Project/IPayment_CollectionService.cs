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
    public interface IPayment_CollectionService : ICrudApplicationService<Payment_CollectionEntity, int>
    {
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        PagedResult<Payment_CollectionOutput> GetPagedList(Payment_CollectionSearchDto input);

        /// <summary>
        /// 根据ID获取回款信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<Payment_CollectionOutput> GetByIdList(int Id);
        /// <summary>
        /// 添加回款
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool Insert(Payment_CollectionDto input);
        /// <summary>
        /// 编辑回款
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool Update(Payment_CollectionDto input);

        /// <summary>
        /// 根据客户名称查询数据
        /// </summary>
        /// <param name="clietId"></param>
        /// <returns></returns>
        List<Payment_CollectionOutput> GetListByClientId(int clietId);

        /// <summary>
        /// 根据ID数组获取回款信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<Payment_CollectionOutput> GetProIdsList(int[] Ids);
    }
}
