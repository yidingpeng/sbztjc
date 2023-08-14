using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;

namespace RW.PMS.Domain.Entities.Purchase
{
    /// <summary>
    /// BOM订单主表
    /// </summary>
    [Table(Name = "mat_indentprimary")]
    public class IndentPrimaryEntity : FullEntity
    {
        /// <summary>
        /// BOM编码
        /// </summary>
        public string BomCode { get; set; }
        /// <summary>
        /// 项目号
        /// </summary>
        public string ProjectCode { get; set; }
        /// <summary>
        /// 项目名
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// 当前版本号
        /// </summary>
        public string CurrentVersion { get; set; }
        /// <summary>
        /// 申请人
        /// </summary>
        public string Applicant { get; set; }
        /// <summary>
        /// 申请日期
        /// </summary>
        public string ApplicationDate { get; set; }
        /// <summary>
        /// 齐套率
        /// </summary>
        public string CompleteSetRate { get; set; }
        /// <summary>
        /// 图纸代号
        /// </summary>
        public string DrawingCode { get; set; }
        /// <summary>
        /// BOMID
        /// </summary>
        public int BomId { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
