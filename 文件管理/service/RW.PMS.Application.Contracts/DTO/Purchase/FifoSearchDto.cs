using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Purchase
{
    public class FifoSearchDto: PagedQuery
    {
        /// <summary>
        /// 出入库分类 0 入库 1 出库
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// BOM编码
        /// </summary>
        public string BomCode { get; set; }
        /// <summary>
        /// BOM名
        /// </summary>
        public string BomName { get; set; }
        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; }

        /// <summary>
        /// 物料名
        /// </summary>
        public string MaterialName { get; set; }
        /// <summary>
        /// 项目号
        /// </summary>
        public string ProjectCode { get; set; }
        /// <summary>
        /// 项目名
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// 出入库人员
        /// </summary>
        public string FifoPersonnel { get; set; }
    }
}
