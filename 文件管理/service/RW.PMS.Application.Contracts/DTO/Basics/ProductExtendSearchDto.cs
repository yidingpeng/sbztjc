using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Basics
{
    public class ProductExtendSearchDto : PagedQuery
    {
        public int? Id { get; set; }

        /// <summary>
        ///     产品型号ID
        /// </summary>
        public int? PModelID { get; set; }

        /// <summary>
        ///     字段名称
        /// </summary>
        public string colName { get; set; }
    }
}
