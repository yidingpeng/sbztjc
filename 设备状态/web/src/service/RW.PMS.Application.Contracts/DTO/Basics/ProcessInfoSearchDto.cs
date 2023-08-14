using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Basics
{
    public class ProcessInfoSearchDto : PagedQuery
    {
        public int? Id { get; set; }

        /// <summary>
        ///     工序编号
        /// </summary>
        public string pcsNo { get; set; }

        /// <summary>
        ///     工序名称
        /// </summary>
        public string pcsName { get; set; }
    }
}
