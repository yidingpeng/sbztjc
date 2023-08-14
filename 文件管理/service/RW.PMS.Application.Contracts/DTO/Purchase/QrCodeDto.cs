using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Purchase
{
    public class QrCodeDto
    {
        public int? Id { get; set; }
        /// <summary>
        /// 二维码
        /// </summary>
        public string QrCode { get; set; }
        /// <summary>
        /// 供应商
        /// </summary>
        public string Supplier { get; set; }

        /// <summary>
        /// BomId
        /// </summary>
        public int BomID { get; set; }

        public string SupName { get; set; }
        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; }

        public string MaterialName { get; set; }
        /// <summary>
        /// 项目编码
        /// </summary>
        public string ProjectCode { get; set; }

        public string ProjectName { get; set; }
        /// <summary>
        /// BOM编码
        /// </summary>
        public string BomCode { get; set; }
        /// <summary>
        /// BOM编码
        /// </summary>
        public string BomName { get; set; }
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
        /// <summary>
        /// 出入库类型，在扫码时赋值使用
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 图片Id集合
        /// </summary>
        public string imgFilesId { get; set; }
    }
}
