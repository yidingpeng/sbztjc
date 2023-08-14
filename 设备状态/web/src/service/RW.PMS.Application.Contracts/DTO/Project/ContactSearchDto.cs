using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Project
{
    public class ContractSearchDto : PagedQuery
    {
        /// <summary>
        /// 联系人姓名
        /// </summary>
        public string ContactsName { get; set; }

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
        /// 客户级别
        /// </summary>
        public int ClientRank { get; set; }
    }
}
