using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Purchase
{
    public class Mat_PurchaseDetailDto
    {
        /// <summary>
        /// 订单主表申请单号
        /// </summary>
        public string ApplicationNo { get; set; }

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
        /// 品牌
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// 基本单位
        /// </summary>
        public string BasicUnit { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }
    }
}
