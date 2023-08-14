using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Basics
{
    public class GjwlOpcPointSearchDto : PagedQuery
    {
        /// <summary>
        ///     工作中心ID
        /// </summary>
        public int gwID { get; set; }

        public string gwname { get; set; }

        /// <summary>
        ///     工具ID
        /// </summary>
        public int? gjID { get; set; }

        /// <summary>
        ///     物料ID
        /// </summary>
        public int? wlID { get; set; }
    }
}
