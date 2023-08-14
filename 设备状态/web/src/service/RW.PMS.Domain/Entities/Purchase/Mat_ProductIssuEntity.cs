using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;

namespace RW.PMS.Domain.Entities.Purchase
{
    /// <summary>
    /// 领料确认表
    /// </summary>
    [Table(Name = "mat_productIssu")]
    public class Mat_ProductIssuEntity: FullEntity
    {
        /// <summary>
        /// 二维码号
        /// </summary>
        public string QrCode { get; set; }

        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; }

        /// <summary>
        /// 物料名
        /// </summary>
        public string MaterialName { get; set; }

        /// <summary>
        /// 项目号
        /// </summary>
        public string ProjectCode { get; set; }
        /// <summary>
        /// 项目名
        /// </summary>
        public string ProjectName { get; set; }
        
        /// <summary>
        /// 领料人
        /// </summary>
        public string LLMan { get; set; }

        /// <summary>
        /// 领料数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int State { get; set; }
    }
}
