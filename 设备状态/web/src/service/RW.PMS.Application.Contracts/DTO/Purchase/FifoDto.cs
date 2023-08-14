using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Purchase
{
    /// <summary>
    /// 出入库信息
    /// </summary>
    public class FifoDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 出入库分类 1 入库 2 出库
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// BOM编码
        /// </summary>
        public string BomCode { get; set; }
        /// <summary>
        /// BOM名
        /// </summary>
        public string BomName { get; set; }
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
        /// 二维码号
        /// </summary>
        public string QrCode { get; set; }

        /// <summary>
        /// 出入库数量
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 出入库时间
        /// </summary>
        public DateTime FifoDateTime { get; set; }
        /// <summary>
        /// 出入库人员
        /// </summary>
        public string FifoPersonnel { get; set; }

        /// <summary>
        /// 出库签字
        /// </summary>
        public int DeliverySignature { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 库存数量
        /// </summary>
        public int KuCun { get; set; }
        /// <summary>
        /// 已出库数量
        /// </summary>
        public int YchCount { get; set; }
    }
}
