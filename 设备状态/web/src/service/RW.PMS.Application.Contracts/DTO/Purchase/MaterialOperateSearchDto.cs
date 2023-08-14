using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Purchase
{
    public class MaterialOperateSearchDto: PagedQuery
    {
        /// <summary>
        /// BOMID
        /// </summary>
        public int BomId { get; set; }
        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        public string MaterialName { get; set; }

        /// <summary>
        /// 供应商
        /// </summary>
        public string Supplier { get; set; }
    }
}
