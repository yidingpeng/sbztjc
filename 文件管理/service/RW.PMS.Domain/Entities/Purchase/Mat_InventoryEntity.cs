using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;

namespace RW.PMS.Domain.Entities.Purchase
{
    /// <summary>
    /// 采购库存表
    /// </summary>
    [Table(Name = "mat_inventory")]
    public class Mat_InventoryEntity : FullEntity
    {
        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        public string MaterialName { get; set; }
        /// <summary>
        /// 型号
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// 参数
        /// </summary>
        public string Parameter { get; set; }
        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// BOM数量
        /// </summary>
        public int BomCount { get; set; }
        /// <summary>
        /// 库存数量
        /// </summary>
        public int WarehousCount { get; set; }

        /// <summary>
        /// 项目号
        /// </summary>
        public string ProjectCode { get; set; }
        /// <summary>
        /// 项目名
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// 供应商
        /// </summary>
        public string Supplier { get; set; }
        /// <summary>
        /// 存放位置
        /// </summary>
        public string StorageLocation { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
