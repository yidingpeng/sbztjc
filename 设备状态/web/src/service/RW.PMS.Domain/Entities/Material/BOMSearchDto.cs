using RW.PMS.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.Material
{
    /// <summary>
    /// BOM清单查询
    /// </summary>
    public class BOMSearchDto:PagedQuery
    {
        /// <summary>
        /// BOMID
        /// </summary>
        public int BOMID { get; set; }
        /// <summary>
        /// 物料码
        /// </summary>
        public string MaterialCode { get; set; }
        /// <summary>
        /// 物料名
        /// </summary>
        public string MaterialName { get; set; }
        /// <summary>
        /// 项目编码
        /// </summary>
        public string ProjectCode { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }
    }
}
