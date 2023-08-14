using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Project
{
    public class ProjectDeliveryFileDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// 项目交付id
        /// </summary>
        public int DeliveryID { get; set; }

        /// <summary>
        /// 项目交付信息单据号
        /// </summary>
        public string DeliveryNo { get; set; }

        /// <summary>
        /// 项目id
        /// </summary>
        public int ProjectID { get; set; }

        /// <summary>
        /// 项目编号
        /// </summary>
        public string ProjectCode { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 设备id
        /// </summary>
        public int DeviceID { get; set; }

        /// <summary>
        /// 设备编号
        /// </summary>
        public string DeviceCode { get; set; }

        /// <summary>
        /// 交付id
        /// </summary>
        public int DeliveryFileId { get; set; }

        /// <summary>
        /// 文件类别
        /// </summary>
        public int DeliveryType { get; set; }

        /// <summary>
        /// 文件类别名称
        /// </summary>
        public string DeliveryTypeText { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Qty { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
