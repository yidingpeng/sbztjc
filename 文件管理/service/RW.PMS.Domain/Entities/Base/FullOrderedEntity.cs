
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.Base
{
    /// <summary>
    /// 包含排序号的完整实体
    /// </summary>
    public class FullOrderedEntity : FullEntity, IOrderable
    {
        /// <summary>
        /// 排序号
        /// </summary>
        [DefaultValue(0)]
        public int OrderIndex { get; set; }
    }
}
