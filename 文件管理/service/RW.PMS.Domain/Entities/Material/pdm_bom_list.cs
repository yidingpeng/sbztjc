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
    [Table(Name = "v_pdm_bom_list", DisableSyncStructure = true)]
    public class pdm_bom_list
    {
        /// <summary>
        /// ID
        /// </summary>
        
        public int Id { get; set; }
        /// <summary>
        /// BOMID
        /// </summary>
        public int BomId { get; set; }
        /// <summary>
        /// BOM码
        /// </summary>
        public string BOMCode { get; set; }
        /// <summary>
        /// BOM名
        /// </summary>
        public string BOMName { get; set; }
        /// <summary>
        /// 项目编码
        /// </summary>
        public string ProjectCode { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public string OrderIndex { get; set; }
        /// <summary>
        /// 物料码
        /// </summary>
        public string MaterialCode { get; set; }
        /// <summary>
        /// 物料名
        /// </summary>
        public string MaterialName { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }
        /// <summary>
        /// 父级
        /// </summary>
        public string ParentCode { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public string OrderFormState { get; set; }

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
        public string Cailiao { get; set; }
        /// <summary>
        /// 基本单位
        /// </summary>
        public string BasicUnit { get; set; }
        /// <summary>
        /// 重量
        /// </summary>
        public decimal Weight { get; set; }
        /// <summary>
        /// 重量单位
        /// </summary>
        public string WeightUnit { get; set; }
    }
}
