using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Material;
using RW.PMS.Domain.Entities.Material;

namespace RW.PMS.Application.Contracts.Material
{
    public interface IMatConversionService : ICrudApplicationService<MaterialConversionEntity, int>
    {
        /// <summary>
        /// 物料换算基础信息查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        PagedResult<MaterialConversionDto> GetHsPageList(MaterialConversionSearchDto input);

        /// <summary>
        /// 验重
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        bool IsExist(MaterialConversionDto input);

        /// <summary>
        /// 编码查信息
        /// </summary>
        /// <returns></returns>
        MaterialConversionDto GetByCode(string code);
    }
}
