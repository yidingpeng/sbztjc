using RW.PMS.Application.Contracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Project
{
    public class Payment_CollectionSearchDto : PagedQuery
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public string Pt_Name { get; set; }
        /// <summary>
        /// 项目编号
        /// </summary>
        public string Pt_Code { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// 营销负责人名字
        /// </summary>
        public string PersonInChargeName { get; set; }

        /// <summary>
        /// 市场片区
        /// </summary>
        public int MarketArea { get; set; }

        /// <summary>
		/// 项目回款日期期限字符串
		/// </summary>
		public string PaymentDateRangT { get; set; }

    }
}
