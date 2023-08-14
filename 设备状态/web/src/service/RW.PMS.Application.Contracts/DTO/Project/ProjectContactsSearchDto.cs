using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Project
{
    public class projectcontactsSearchDto:PagedQuery
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public string PName { get; set; }
        /// <summary>
        /// 项目编号
        /// </summary>
        public string projectCode { get; set; }
        /// <summary>
        /// 项目编号
        /// </summary>
        public string ContactsName { get; set; }

    }
}
