using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Purchase;
using RW.PMS.Domain.Entities.Purchase;

namespace RW.PMS.Application.Contracts.Purchase
{
    public interface IindentPrimaryService : ICrudApplicationService<IndentPrimaryEntity, int>
    {
        /// <summary>
        /// BOM订单主表分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        PagedResult<IndentPrimaryDto> PagedList(IndentPrimarySearchDto input);

        /// <summary>
        /// 根据BomId查询订单主表信息
        /// </summary>
        /// <param name="BomId"></param>
        /// <returns></returns>
        IndentPrimaryDto ByBomId(int BomId);
    }
}
