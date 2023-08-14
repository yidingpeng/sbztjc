using RW.PMS.Application.Contracts.DTO.Purchase;
using RW.PMS.Domain.Entities.Purchase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.Purchase
{
    public interface ICdService : ICrudApplicationService<MatCDEntity, int>
    {
        /// <summary>
        /// 根据二维码查询报检信息
        /// </summary>
        /// <param name="QrCode"></param>
        /// <returns></returns>
        CDDto CDInfo(string QrCode);
    }
}
