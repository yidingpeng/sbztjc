using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Project
{
    public class SalesAreaInfoSelectDto 
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// 市场片区名称+板块名称
        /// </summary>
        public string AreaNameAndPlaceName { get; set; }
    }
}
