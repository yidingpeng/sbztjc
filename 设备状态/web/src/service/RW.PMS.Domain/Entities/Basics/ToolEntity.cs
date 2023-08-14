using System;
using System.ComponentModel.DataAnnotations;
using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;

namespace RW.PMS.Domain.Entities.Basics
{
    /// <summary>
    ///     工器具基础信息表
    /// </summary>
    [Table(Name = "base_Tool")]
    public class ToolEntity : FullEntity
    {
        /// <summary>
        ///     工具编码
        /// </summary>
        [MaxLength(150)]
        public string toolCode { get; set; }
        /// <summary>
        ///     工具名称
        /// </summary>
        [MaxLength(150)]
        public string toolName { get; set; }
        /// <summary>
        ///     工具类别
        /// </summary>
        public int toolClassID { get; set; }
        /// <summary>
        ///     工具类型
        /// </summary>
        public int toolTypeID { get; set; }
        /// <summary>
        ///     规格型号
        /// </summary>
        public int toolModel { get; set; }
        /// <summary>
        ///     品牌
        /// </summary>
        [MaxLength(150)]
        public string toolBrand { get; set; }
        /// <summary>
        ///     证书编号
        /// </summary>
        [MaxLength(150)]
        public string toolCertificate { get; set; }
        /// <summary>
        ///     购买日期
        /// </summary>
        [MaxLength(150)]
        public DateTime purchaseTime { get; set; }
        /// <summary>
        ///     生效时间
        /// </summary>
        [MaxLength(150)]
        public DateTime effectTime { get; set; }
        /// <summary>
        ///     失效时间
        /// </summary>
        [MaxLength(150)]
        public DateTime failuretime { get; set; }
        /// <summary>
        ///     装配时是否要录工具编码
        /// </summary>
        public int toolIsHasCode { get; set; }
        /// <summary>
        ///     状态
        /// </summary>
        public int usingFlag { get; set; }
        /// <summary>
        ///     备注
        /// </summary>
        [MaxLength(255)]
        public string remark { get; set; }

    }
}
