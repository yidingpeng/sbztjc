using RW.PMS.Application.Contracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Project
{
    public class Project_DeliverySearchDto : PagedQuery
    {
        /// <summary>
        /// 项目编号
        /// </summary>
        public string projectName { get; set; }
        /// <summary>
        /// 交付信息单据号
        /// </summary>
        public string DeliveryCode { get; set; }
    }
}
