using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Project
{
    public class bd_SalesAreaInfoSearchDto : PagedQuery
    {
        /// <summary>
        /// 市场片区编码
        /// </summary>
        public string AreaCode { get; set; }

        /// <summary>
        /// 市场片区名称
        /// </summary>
        public string AreaName { get; set; }
    }
}
