using System;
using System.ComponentModel.DataAnnotations;
using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;

namespace RW.PMS.Domain.Entities.Basics
{ 
    /// <summary>
    ///     产品信息
    /// </summary>
    [Table(Name = "base_production")]
    public class ProductionEntity : FullEntity
    {
        /// <summary>
        ///     产品名称
        /// </summary>
        [MaxLength(150)]
        public string Pname { get; set; }
        /// <summary>
        ///     产品编码
        /// </summary>
        [MaxLength(150)]
        public string PCode { get; set; }
        /// <summary>
        ///     规格
        /// </summary>
        [MaxLength(150)]
        public string Size { get; set; }
        /// <summary>
        ///     产品种类
        /// </summary>
        [MaxLength(150)]
        public string Category { get; set; }
        /// <summary>
        ///     状态，0:启用，1：禁用	
        /// </summary>
        public int Pstatus { get; set; }
        /// <summary>
        ///     备注
        /// </summary>
        [MaxLength(255)]
        public string remark { get; set; }
    }
}
