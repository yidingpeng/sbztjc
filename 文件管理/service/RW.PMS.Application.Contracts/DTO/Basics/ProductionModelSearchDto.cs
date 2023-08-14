using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Basics
{
    public class ProductionModelSearchDto : PagedQuery
    {
        public int? Id { get; set; }
        public int? Pid { get; set; }
        public string Pname { get; set; }
        /// <summary>
        ///     产品型号编码
        /// </summary>
        public string PmodelCode { get; set; }
    }
}

