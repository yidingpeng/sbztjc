using RW.PMS.Application.Contracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Project
{
    public class Contract_InfoSearchInput: PagedQuery
    {
        /// <summary>
        /// 项目编号
        /// </summary>
        public string pt_code { get; set; }

        /// <summary>
        /// 合同编号
        /// </summary>
        public string ct_code { get; set; }

        /// <summary>
        /// 合同签订日期
        /// </summary>
        public string ct_signingDate { get; set; }

        /// <summary>
        /// 付款方式
        /// </summary>
        public int pay_mode { get; set; }
    }
}
