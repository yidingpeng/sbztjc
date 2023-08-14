using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.Material
{
    /// <summary>
    /// 物料换算
    /// </summary>
    [Table(Name = "mat_conversion")]
    public class MaterialConversionEntity: FullEntity
    {
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
        /// 基本单位
        /// </summary>
        public string BasicUnit { get; set; }

        /// <summary>
        /// 换算单位
        /// </summary>
        public string HsUnit { get; set; }

        /// <summary>
        /// 换算数量 N ：1
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
