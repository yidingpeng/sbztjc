using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Basics
{
    public class WorkCenterSearchDto : PagedQuery
    {
        public int? Id { get; set; }

        /// <summary>
        ///     工作中心编码
        /// </summary>
        public string workCode { get; set; }

        public string workName { get; set; }
    }
}
