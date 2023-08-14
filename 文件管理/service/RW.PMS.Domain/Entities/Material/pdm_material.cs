using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.Material
{
    /// <summary>
    /// 物料信息
    /// </summary>
    [Table(Name = "v_pdm_material", DisableSyncStructure = true)]
    public class pdm_material
    {
        /// <summary>
        /// 物料的唯一ID
        /// </summary>
        public int Cid { get; set; }
        /// <summary>
        /// 物料编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 别称
        /// </summary>
        public string Alias { get; set; }
        /// <summary>
        /// 型号规格
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// 参数特性
        /// </summary>
        public string Specification { get; set; }
        /// <summary>
        /// 材料
        /// </summary>
        public string Material { get; set; }
        /// <summary>
        /// 基本单位
        /// </summary>
        public string BasicUnit { get; set; }
        /// <summary>
        /// 物料来源
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// 重要度
        /// </summary>
        public string Important { get; set; }
        /// <summary>
        /// 编码状态
        /// </summary>
        public string CodingStatus { get; set; }
        /// <summary>
        /// 重量
        /// </summary>
        public decimal Weight { get; set; }
        /// <summary>
        /// 重量单位
        /// </summary>
        public string WeightUnit { get; set; }
        /// <summary>
        /// 分类路径
        /// </summary>
        public string CategoryPath { get; set; }
        /// <summary>
        /// 主要分类
        /// </summary>
        public string CategoryMaster { get; set; }
        /// <summary>
        /// 最终分类
        /// </summary>
        public string CategoryLast { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CategoryCode { get; set; }
        /// <summary>
        /// 品牌，制造商
        /// </summary>
        public string Manufacturer { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 物料创建时间
        /// </summary>
        public string CreateTime { get; set; }
        /// <summary>
        /// 参考价格
        /// </summary>
        public decimal RefPrice { get; set; }
        /// <summary>
        /// 价格单位
        /// </summary>
        public string PriceUnit { get; set; }
    }
}
