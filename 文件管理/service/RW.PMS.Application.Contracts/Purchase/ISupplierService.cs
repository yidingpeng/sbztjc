using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Purchase;
using RW.PMS.Domain.Entities.Purchase;

namespace RW.PMS.Application.Contracts.Purchase
{
    public interface ISupplierService:ICrudApplicationService<Mat_SupplierEntity, int>
    {
        /// <summary>
        /// 供应商分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        PagedResult<SupplierDto> PagedList(SupplierSearchDto input);

        /// <summary>
        /// 判断是否存在相同供应商
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        bool IsExist(SupplierDto input);

        /// <summary>
        /// 根据物料编码查询信息
        /// </summary>
        /// <param name="materialCode"></param>
        /// <returns></returns>
        SupplierDto GetInfoByMCode(string materialCode);
    }
}
