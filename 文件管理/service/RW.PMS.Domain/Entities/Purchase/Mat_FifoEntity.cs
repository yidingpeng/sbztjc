using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using System;

namespace RW.PMS.Domain.Entities.Purchase
{
    /// <summary>
    /// 采购出入库表
    /// </summary>
    [Table(Name = "mat_fifo")]
    public class Mat_FifoEntity: FullEntity
    {
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
        public DateTime  FifoDateTime { get; set; }
        /// <summary>
        /// 领料人员
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
    }
}
