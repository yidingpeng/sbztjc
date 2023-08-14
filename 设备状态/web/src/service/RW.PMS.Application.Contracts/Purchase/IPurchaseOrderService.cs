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
    public interface IPurchaseOrderService : ICrudApplicationService<Mat_PurchaseOrderEntity, int>
    {

        /// <summary>
        /// 申请列表分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        PagedResult<Mat_PurchaseOrderDto> GetPagedList(PurchaseOrderSearchDto input);

        /// <summary>
        /// 判断该订单单号是否已经存在
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        bool IsExist(Mat_PurchaseOrderDto input);

    }
}
