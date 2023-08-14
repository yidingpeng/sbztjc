using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Project
{
    public class ProjectDeliveryFileSearchDto : PagedQuery
    {
        /// <summary>
        /// 项目编号
        /// </summary>
        public string ProjectCode { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 项目交付信息单据号
        /// </summary>
        public string DeliveryNo { get; set; }

        /// <summary>
        /// 设备编号
        /// </summary>
        public string DeviceCode { get; set; }

        /// <summary>
        /// 文件类别
        /// </summary>
        public int DeliveryType { get; set; }
    }
}
