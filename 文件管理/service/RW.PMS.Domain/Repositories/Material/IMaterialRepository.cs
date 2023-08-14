using FreeSql;
using RW.PMS.Domain.Entities.Common;
using RW.PMS.Domain.Entities.Material;
using System.Collections.Generic;

namespace RW.PMS.Domain.Repositories.Material
{
    /// <summary>
    /// 物料
    /// </summary>
    public interface IMaterialRepository : IBaseRepository<pdm_material>
    {
        /// <summary>
        /// 物料分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        PagedResult<pdm_material> GetPagedList(MaterialSearchDto input);

        /// <summary>
        /// 根据cid获取物料数据
        /// </summary>
        /// <param name="Cid"></param>
        /// <returns></returns>
        pdm_material getDataByCid(int Cid);

        /// <summary>
        /// 获取物料所有信息
        /// </summary>
        /// <returns></returns>
        List<pdm_material> Getpdm_material();

        /// <summary>
        /// BOM清单分页
        /// </summary>
        /// <param name="BomId"></param>
        /// <returns></returns>
        List<pdm_bom_list> ByBomIdList(int BomId);

        /// <summary>
        /// BOMSet
        /// </summary>
        /// <returns></returns>
        List<pdm_bom> PDM_BOMList();

        /// <summary>
        /// 根据bom编码获取bom信息
        /// </summary>
        /// <param name="bomCode"></param>
        /// <returns></returns>
        pdm_bom PDM_BOMListByCode(string bomCode);

        /// <summary>
        /// PDM_BOMPageList
        /// </summary>
        /// <returns></returns>
        PagedResult<pdm_bom> PDM_BOMPageList(PdmBomSearchDto input);
        /// <summary>
        /// 获取BOm树形数据
        /// </summary>
        /// <param name="BomId"></param>
        /// <returns></returns>
        List<pdm_bom_list> ByBomToTreeList();
        /// <summary>
        /// 编码查物料信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        pdm_material getDataByCode(string code);
    }
}
