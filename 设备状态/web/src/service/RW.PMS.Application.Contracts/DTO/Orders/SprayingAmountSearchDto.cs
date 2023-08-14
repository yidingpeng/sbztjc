using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Orders
{
    public class SprayingAmountSearchDto : PagedQuery
    {
        /// <summary>
        /// 车轴型号
        /// </summary>
        public string oAxleModel { get; set; }
    }
}
