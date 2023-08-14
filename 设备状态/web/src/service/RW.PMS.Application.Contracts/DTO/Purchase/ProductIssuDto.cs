using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Purchase
{
    public class ProductIssuDto
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 二维码号
        /// </summary>
        public string QrCode { get; set; }

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
        /// 领料人
        /// </summary>
        public string LLMan { get; set; }

        /// <summary>
        /// 领料数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int State { get; set; }
    }
}
