using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.Purchase
{
    /// <summary>
    /// 二维码信息表
    /// </summary>
    [Table(Name = "qr_code")]
    public class Qr_CodeEntity: FullEntity
    {
        /// <summary>
        /// 二维码
        /// </summary>
        public string QrCode { get; set; }
        /// <summary>
        /// 供应商
        /// </summary>
        public string Supplier { get; set; }
        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        public string MaterialName { get; set; }
        /// <summary>
        /// 项目编码
        /// </summary>
        public string ProjectCode { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// BOM编码
        /// </summary>
        public int BomId { get; set; }

        /// <summary>
        /// bom编码
        /// </summary>
        public string BomCode { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 是否合格
        /// </summary>
        public int qualified { get; set; }
    }
}
