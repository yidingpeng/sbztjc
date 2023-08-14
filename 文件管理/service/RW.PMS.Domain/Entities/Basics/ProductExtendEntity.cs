using System;
using System.ComponentModel.DataAnnotations;
using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;

namespace RW.PMS.Domain.Entities.Basics
{
    /// <summary>
    ///     产品信息扩展字段表
    /// </summary>
    [Table(Name = "base_productExtend")]
    public class ProductExtendEntity : FullEntity
    {
        /// <summary>
        ///     产品型号ID
        /// </summary>
        public int PModelID { get; set; }

        /// <summary>
        ///     字段名称
        /// </summary>
        [MaxLength(150)]
        public string colName { get; set; }
        /// <summary>
        ///     界面列名称
        /// </summary>
        [MaxLength(150)]
        public string textName { get; set; }
        /// <summary>
        ///     列类型
        /// </summary>
        public int textType { get; set; }
        /// <summary>
        ///     界面列说明
        /// </summary>
        [MaxLength(150)]
        public string textMemo { get; set; }
        /// <summary>
        ///     字段值
        /// </summary>
        [MaxLength(150)]
        public string extendValue { get; set; }
    }
}
