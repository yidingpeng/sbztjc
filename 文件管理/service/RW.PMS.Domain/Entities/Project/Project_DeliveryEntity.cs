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
    [Table(Name = "pro_delivery", OldName = "Project_Delivery")]

    public class Project_DeliveryEntity: FullEntity
    {
        /// <summary>
        /// 交付信息单据号
        /// </summary>
        public string DeliveryCode { get; set; }
        /// <summary>
        /// 项目编号
        /// </summary>
        public int ProjectID { get; set; }
        /// <summary>
        /// 计划交货日期
        /// </summary>
        public DateTime JHJHDate { get; set; }
        /// <summary>
        /// 实际交货日期
        /// </summary>
        public DateTime SJJHDate { get; set; }
        /// <summary>
        /// 计划验收日期
        /// </summary>
        public DateTime JHYSDate { get; set; }
        /// <summary>
        /// 实际验收日期
        /// </summary>
        public DateTime SJYSDate { get; set; }
        /// <summary>
        /// 验收阶段
        /// </summary>
        public int AcceptancePhase { get; set; }
        /// <summary>
        /// 是否终验收
        /// </summary>
        public int IsZYS { get; set; }
        /// <summary>
        /// 验收凭证
        /// </summary>
        public int AcceptanceCertificate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public string Remark { get; set; }
    }
}
