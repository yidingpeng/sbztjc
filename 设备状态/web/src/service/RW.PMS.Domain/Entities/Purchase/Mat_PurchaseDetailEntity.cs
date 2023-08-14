using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;

namespace RW.PMS.Domain.Entities.Purchase
{
    /// <summary>
    /// 采购明细
    /// </summary>
    [Table(Name = "mat_purchase_detail")]
    public class Mat_PurchaseDetailEntity: FullEntity
    {
        /// <summary>
        /// 订单主表申请单号
        /// </summary>
        public string ApplicationNo { get; set; }

        /// <summary>
        /// 物料编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 规格型号
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// 基本单位
        /// </summary>
        public string BasicUnit { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }
    }
}
