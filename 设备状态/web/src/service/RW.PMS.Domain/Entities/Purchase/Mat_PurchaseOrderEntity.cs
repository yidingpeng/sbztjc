using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;

namespace RW.PMS.Domain.Entities.Purchase
{
    /// <summary>
    /// 采购订单
    /// </summary>
    [Table(Name = "mat_purchase_order")]
    public class Mat_PurchaseOrderEntity:FullEntity
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 申请单号
        /// </summary>
        public string ApplicationNo { get; set; }

        /// <summary>
        /// 申请理由
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
