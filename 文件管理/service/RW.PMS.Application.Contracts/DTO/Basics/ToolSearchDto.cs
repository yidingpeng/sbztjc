using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Basics
{
    public class ToolSearchDto : PagedQuery
    {
        public int? Id { get; set; }

        /// <summary>
        ///     工具编码
        /// </summary>
        public string toolCode { get; set; }
        public string toolName { get; set; }
    }
}
