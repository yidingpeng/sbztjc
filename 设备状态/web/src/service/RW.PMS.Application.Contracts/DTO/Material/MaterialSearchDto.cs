using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Material
{
    public class MaterialSearchDto : PagedQuery
    {
        /// <summary>
        ///     物料编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        ///     物料名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     产品编码
        /// </summary>
        public string ModelCode { get; set; }

        /// <summary>
        ///     项目编码
        /// </summary>
        public string ProjectCode { get; set; }

    }
}
