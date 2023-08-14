using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Purchase;
using RW.PMS.Domain.Entities.Purchase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.Purchase
{
    public interface IinventoryService : ICrudApplicationService<Mat_InventoryEntity, int>
    {
        /// <summary>
        /// 库存信息分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        PagedResult<InventoryDto> InventoryPagedList(InventorySearchDto input);

        /// <summary>
        /// 根据物料编码查询库存
        /// </summary>
        /// <param name="materialCode"></param>
        /// <returns></returns>
        Mat_InventoryEntity GetInventoryInfo(string materialCode);

        /// <summary>
        /// 库存数量变更
        /// </summary>
        /// <param name="type"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        bool InvCountEdit(int count, string materialCode);

        /// <summary>
        /// 库存数量变更
        /// </summary>
        /// <param name="type"></param>
        /// <param name="count"></param>
        /// <param name="materialCode"></param>
        /// <returns></returns>
        bool InvCountEdit(int type, int count, string materialCode);
    }
}
