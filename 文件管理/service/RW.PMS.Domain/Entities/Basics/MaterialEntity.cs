using System;
using System.ComponentModel.DataAnnotations;
using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;

namespace RW.PMS.Domain.Entities.Basics
{
    /// <summary>
    ///     物料基础信息表
    /// </summary>
    [Table(Name = "base_Material")]
    public class MaterialEntity : FullEntity
    {
        /// <summary>
        ///     物料编码
        /// </summary>
        [MaxLength(50)]
        public string MtlCode { get; set; }
        /// <summary>
        ///     物料/零件名称
        /// </summary>
        [MaxLength(150)]
        public string MtlName { get; set; }
        /// <summary>
        ///     物料/零件规格型号
        /// </summary>
        [MaxLength(150)]
        public string MtlModel { get; set; }
        /// <summary>
        ///     图号
        /// </summary>
        [MaxLength(50)]
        public string wlFigureNo { get; set; }
        /// <summary>
        ///     物料分类
        /// </summary>
        public int MtlClassID { get; set; }
        /// <summary>
        ///     物料类型
        /// </summary>
        public int MtlTypeID { get; set; }
        /// <summary>
        ///     基本单位
        /// </summary>
        public int baseUnitID { get; set; }
        /// <summary>
        ///     重要度
        /// </summary>
        public int importance { get; set; }
        /// <summary>
        ///     装配时是否要录物料编码
        /// </summary>
        public int MtlIsHasCode { get; set; }
        /// <summary>
        ///     购买日期
        /// </summary>
        public DateTime purchaseTime { get; set; }
        /// <summary>
        ///     生效时间
        /// </summary>
        public DateTime effectTime { get; set; }
        /// <summary>
        ///     失效时间
        /// </summary>
        public DateTime failuretime { get; set; }
        /// <summary>
        ///     材质
        /// </summary>
        public int texture { get; set; }
        /// <summary>
        ///     重量
        /// </summary>
        public int weight { get; set; }
        /// <summary>
        ///     尺寸
        /// </summary>
        [MaxLength(150)]
        public string size { get; set; }
        /// <summary>
        ///     状态
        /// </summary>
        public int MtlStatus { get; set; }
        /// <summary>
        ///     备注
        /// </summary>
        [MaxLength(255)]
        public string remark { get; set; }
    }
}
