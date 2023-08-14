using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.Base
{
    /// <summary>
    /// 支持排序的接口
    /// </summary>
    public interface IOrderable
    {
        /// <summary>
        /// 排序号
        /// </summary>
        int OrderIndex { get; set; }
    }
}
