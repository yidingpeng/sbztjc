using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.Input.BaseInfo
{
    public class Project_DeliveryInput
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 交付信息单据号
        /// </summary>
        public string DeliveryCode { get; set; }
        /// <summary>
        /// 项目编号
        /// </summary>
        public string ProjectID { get; set; }
        /// <summary>
        /// 计划交货日期
        /// </summary>
        public string JHJHDate { get; set; }
        /// <summary>
        /// 实际交货日期
        /// </summary>
        public string SJJHDate { get; set; }
        /// <summary>
        /// 计划验收日期
        /// </summary>
        public string JHYSDate { get; set; }
        /// <summary>
        /// 实际验收日期
        /// </summary>
        public string SJYSDate { get; set; }
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
        public string AcceptanceCertificate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
