using AutoMapper;
using FreeSql;
using Newtonsoft.Json;
using RW.PMS.CrossCutting.Extensions;
using RW.PMS.Domain.Entities.Common;
using RW.PMS.Domain.Entities.Material;
using RW.PMS.Domain.Repositories.Material;
using System.Collections.Generic;
using System.Linq;

namespace RW.PMS.Infrastructure.Repositories.Material
{
    public class MaterialRepository: BaseRepository<pdm_material>, IMaterialRepository
    {
        public MaterialRepository(UnitOfWorkManager uowManger) : base(uowManger.Orm, null)
        {

        }
        /// <summary>
        /// 物料分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PagedResult<pdm_material> GetPagedList(MaterialSearchDto input)
        {
            var list = Select
                .WhereIf(input.Code.NotNullOrEmpty(), a => a.Code.Contains(input.Code))
                .WhereIf(input.Name.NotNullOrEmpty(), a => a.Name.Contains(input.Name))
                .WhereIf(input.Source.NotNullOrEmpty(), a => a.Source == input.Source)
                .WhereIf(input.CodingStatus.NotNullOrEmpty(), a => a.CodingStatus == input.CodingStatus)
                .WhereIf(input.Alias.NotNullOrEmpty(), a => a.Alias .Contains(input.Alias))
                .WhereIf(input.Important.NotNullOrEmpty(), a => a.Important == input.Important)
                .Count(out var total)
                .Page(input.PageNo, input.PageSize).ToList();
            return new PagedResult<pdm_material>(list, total);
        }

        public pdm_material getDataByCid(int Cid)
        {
            var data = Select.Where(t => t.Cid == Cid)?.ToOne() ?? null;
            return data;
        }
        /// <summary>
        /// 编码查物料信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public pdm_material getDataByCode(string code)
        {
            var data = Select.Where(t => t.Code == code)?.ToOne() ?? null;
            return data;
        }

        public List<pdm_material> Getpdm_material()
        {
            var list = Select.OrderBy(t => t.Cid).ToList();
            return list;
        }

        /// <summary>
        /// BOM清单分页
        /// </summary>
        /// <param name="BomId"></param>
        /// <returns></returns>
        public List<pdm_bom_list> ByBomIdList(int BomId)
        {
            //var t1 = Orm.Select<pdm_bom_list>().ToTreeList();
            var list = Orm.Select<pdm_bom_list>()
                .Where(a => a.BomId == BomId).OrderBy(t=>t.MaterialCode).ToList();
            return list;
        }
        /// <summary>
        /// 获取BOm树形数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<pdm_bom_list> ByBomToTreeList()
        {
            var list = Orm.Select<pdm_bom_list>().OrderBy(t=>t.MaterialCode).ToList();
            return list;
        }
        /// <summary>
        /// BOMSet
        /// </summary>
        /// <returns></returns>
        public List<pdm_bom> PDM_BOMList()
        {
            var list = Orm.Select<pdm_bom>().OrderBy(t=>t.BOMCode).ToList();
            return new List<pdm_bom>(list);
        }

        /// <summary>
        /// 根据编码获取bom信息
        /// </summary>
        /// <param name="bomCode"></param>
        /// <returns></returns>
        public pdm_bom PDM_BOMListByCode(string bomCode)
        {
            var data = Orm.Select<pdm_bom>().Where(t=>t.BOMCode==bomCode).ToOne();
            return data;
        }

        /// <summary>
        /// PDM_BOMPageList
        /// </summary>
        /// <returns></returns>
        public PagedResult<pdm_bom> PDM_BOMPageList(PdmBomSearchDto input)
        {
            var list = Orm.Select<pdm_bom>()
                 .WhereIf(input.BOMCode.NotNullOrEmpty(), a => a.BOMCode.Contains(input.BOMCode) || a.BOMName.Contains(input.BOMCode))
                 .WhereIf(input.ProjectId.NotNullOrEmpty(), a => a.ProjectId.Contains(input.ProjectId) || a.ProjectName.Contains(input.ProjectId))
                 .Count(out var total)
                 .Page(input.PageNo, input.PageSize).ToList();
            return new PagedResult<pdm_bom>(list, total);
        }
    }
}
