using RW.PMS.Application.Contracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Project
{
    public class ProjectInvoicingSearchDto : PagedQuery
    {
        /// <summary>
        /// 项目编号
        /// </summary>
        public string ProjectCode { get; set; }

        /// <summary>
        /// 合同编号
        /// </summary>
        public string ct_code { get; set; }
    }
}
