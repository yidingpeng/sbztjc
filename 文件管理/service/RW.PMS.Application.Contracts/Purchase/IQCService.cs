using RW.PMS.Application.Contracts.DTO.Purchase;
using RW.PMS.Domain.Entities.Common;
using RW.PMS.Domain.Entities.Purchase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.Purchase
{
    public interface IQCService : ICrudApplicationService<MatQCEntity, int>
    {
        /// <summary>
        /// 质检分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        PagedResult<QCDto> QCPagedList(QCSearchDto input);

        /// <summary>
        /// 根据二维码查询质检信息
        /// </summary>
        /// <param name="QrCode"></param>
        /// <returns></returns>
        QCDto QcInfo(string QrCode);

        /// <summary>
        /// 是否存在重复质检信息
        /// </summary>
        /// <param name="QrCode"></param>
        /// <returns></returns>
        bool Exists(string QrCode);
    }
}
