using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Material
{
    public class BOMListSearchDto : PagedQuery
    {
        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; }
        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialName { get; set; }
        /// <summary>
        /// 物料编码
        /// </summary>
        public string ProjectCode { get; set; }

    }
}
