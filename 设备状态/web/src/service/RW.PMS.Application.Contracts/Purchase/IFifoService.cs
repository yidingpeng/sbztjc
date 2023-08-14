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
    public interface IFifoService: ICrudApplicationService<Mat_FifoEntity, int>
    {
        /// <summary>
        /// 出入库信息分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        PagedResult<FifoDto> FifoPagedList(FifoSearchDto input);

        /// <summary>
        /// 判断是否已经出入库
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        bool IsExist(FifoDto input);

        /// <summary>
        /// 根据二维码号查询最新的出入库信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        FifoDto GetByQrCodeFifo(string qrcode, int type);

        /// <summary>
        /// 返回已经入库的数量
        /// </summary>
        /// <param name="qrcode"></param>
        /// <returns></returns>
        int GetCkCount(string qrcode, int type);
    }
}
