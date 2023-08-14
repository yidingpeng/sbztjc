using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.WorkingHours
{
    public class WHDetailSearchDto: PagedQuery
    {
        /// <summary>
        /// 填报开始日期
        /// </summary>
        public DateOnly? WHStartDate { get; set; }

        /// <summary>
        /// 填报结束日期
        /// </summary>
        public DateOnly? WHEndDate { get; set; }

        /// <summary>
        /// 项目编号
        /// </summary>
        public string ProjectCode { get; set; }
    }
}
