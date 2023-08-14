using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.Project
{
    /// <summary>
    /// 项目交付信息
    /// </summary>
    [Table(Name = "pro_delivery_file", OldName = "ProjectDeliveryFile")]

    public class ProjectDeliveryFileEntity : FullEntity
    {
        /// <summary>
        /// 项目交付id
        /// </summary>
        public int DeliveryID { get; set; }
        /// <summary>
        /// 项目id
        /// </summary>
        public int ProjectID { get; set; }
        /// <summary>
        /// 设备id
        /// </summary>
        public int DeviceID { get; set; }
        /// <summary>
        /// 交付id
        /// </summary>
        public int DeliveryFileId { get; set; }
        /// <summary>
        /// 文件类别
        /// </summary>
        public int DeliveryType { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Qty { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public string Remark { get; set; }
    }
}
