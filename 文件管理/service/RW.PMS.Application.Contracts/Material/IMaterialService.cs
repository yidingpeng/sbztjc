using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Material;
using RW.PMS.Domain.Entities.Material;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.Material
{
    public interface IMaterialService : ICrudApplicationService<MaterialEntity, int>
    {
        /// <summary>
        /// 获取物料分页数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        PagedResult<MaterialEntity> GetPageList(DTO.Material.MaterialSearchDto input, string url);

        /// <summary>
        /// 物料换算基础信息查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        PagedResult<MaterialConversionEntity> GetHsPageList(MaterialConversionSearchDto input);

        /// <summary>
        /// 判断添加物料时该编码是否已存在
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        bool IsExist(MaterialDto input);

        /// <summary>
        /// 批量增加
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        bool BatchDoAdd(List<MaterialEntity> datas);

        /// <summary>
        /// 逻辑删除物料
        /// </summary>
        /// <param name="MaterialCode"></param>
        /// <returns></returns>
        bool DeleteMaterial(string MaterialCode);

        /// <summary>
        /// 物料换算新增
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        bool MatHsDoAdd(MaterialConversionDto input);
        /// <summary>
        /// 物料换算编辑
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        bool MatHsDoEdit(MaterialConversionDto input);

        /// <summary>
        /// 物料换算删除
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        bool MatHsDoDelete(int Id);
    }
}
