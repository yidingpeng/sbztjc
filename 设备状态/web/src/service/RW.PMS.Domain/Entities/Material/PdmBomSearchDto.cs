using RW.PMS.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.Material
{
    /// <summary>
    /// BOM清单
    /// </summary>
    public class PdmBomSearchDto: PagedQuery
    {
        /// <summary>
        /// BOM编码
        /// </summary>
        public string BOMCode { get; set; }
        /// <summary>
        /// BOM名称
        /// </summary>
        public string BOMName { get; set; }
        /// <summary>
        /// 项目编码
        /// </summary>
        public string ProjectId { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }
    } 
}
