using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.Common
{
    /// <summary>
    /// page
    /// </summary>
    public class PagedQuery
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int PageNo { get; set; }

        /// <summary>
        /// 条数
        /// </summary>
        public int PageSize { get; set; }
    }
}
