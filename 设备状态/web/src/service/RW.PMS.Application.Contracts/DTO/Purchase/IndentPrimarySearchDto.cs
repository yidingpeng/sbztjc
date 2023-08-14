using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Purchase
{
    public class IndentPrimarySearchDto : PagedQuery
    {
        /// <summary>
        /// 项目号
        /// </summary>
        public string ProjectCode { get; set; }

        /// <summary>
        /// 申请人
        /// </summary>
        public string Applicant { get; set; }

        /// <summary>
        /// 申请日期
        /// </summary>
        public string ApplicationDate { get; set; }
    }
}
