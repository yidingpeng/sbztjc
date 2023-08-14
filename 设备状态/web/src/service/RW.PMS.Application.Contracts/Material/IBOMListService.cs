using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Material;
using RW.PMS.Domain.Entities.Material;
using System.Collections.Generic;

namespace RW.PMS.Application.Contracts.Material
{
    public interface IBOMListService : ICrudApplicationService<BOMListEntity, int>
    {
        /// <summary>
        /// BOM清单分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        PagedResult<BOMListEntity> GetPageList(BOMListSearchDto input);

        /// <summary>
        /// 批量增加
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        bool BatchDoAdd(List<BOMListEntity> datas);
        /// <summary>
        /// 逻辑删除BOM
        /// </summary>
        /// <param name="MaterialCode"></param>
        /// <returns></returns>
        bool DeleteBOM(string MaterialCode);
    }
}
