using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Purchase
{
    public class Mat_PurchaseOrderDto
    {
        /// <summary>
        /// 采购明细数据
        /// </summary>
        public List<Mat_PurchaseDetailDto> purDetaileData { get; set; }
        /// <summary>
        /// ID
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 申请单号
        /// </summary>
        public string ApplicationNo { get; set; }

        /// <summary>
        /// 申请理由
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 申请人
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// 创建人名称
        /// </summary>
        public string CreatedByName { get; set; }
    }
}
