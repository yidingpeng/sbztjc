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
    public interface IQrCodeService : ICrudApplicationService<Qr_CodeEntity, int>
    {
        #region 二维码信息库
        /// <summary>
        /// 二维码信息库分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        PagedResult<QrCodeDto> PagedList(QrCodeSearchDto input);

        /// <summary>
        /// 二维码信息库导出（带条件）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<QrCodeDto> PrintQrCodeList(QrCodeSearchDto input);

        /// <summary>
        /// 根据二维码查询信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        QrCodeDto GetByQrCode(string QrCode);
        /// <summary>
        /// 二维码库是否已经生成
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        bool IsExist(QrCodeDto input);

        /// <summary>
        /// 给该QRCode填写是否合格字段
        /// </summary>
        /// <param name="QrCode"></param>
        /// <param name="qualified"></param>
        /// <returns></returns>
        bool UpdateMQualified(string QrCode, int qualified);
        #endregion
    }
}
