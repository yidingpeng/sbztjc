using RW.PMS.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.Material
{
    /// <summary>
    /// material
    /// </summary>
    public class MaterialSearchDto:PagedQuery
    {
        /// <summary>
        /// 物料编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// 当前状态
        /// </summary>
        public string CodingStatus { get; set; }
        /// <summary>
        /// 重要度
        /// </summary>
        public string Important { get; set; }
        /// <summary>
        /// 别称
        /// </summary>
        public string Alias { get; set; }
    }
}
