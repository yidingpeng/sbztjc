using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Purchase
{
    public class SupplierSearchDto: PagedQuery
    {
        /// <summary>
        /// 供应商编码
        /// </summary>
        public string SupCode { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string SupName { get; set; }
        /// <summary>
        /// 供应商负责人
        /// </summary>
        public string SupPrincipal { get; set; }
    }
}
