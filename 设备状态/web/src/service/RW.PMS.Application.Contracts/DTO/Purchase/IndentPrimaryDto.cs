using RW.PMS.Domain.Entities.BOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Purchase
{
    public class IndentPrimaryDto
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// BOM编码
        /// </summary>
        public string BomCode { get; set; }
        /// <summary>
        /// 项目号
        /// </summary>
        public string ProjectCode { get; set; }
        /// <summary>
        /// 项目名
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// 当前版本号
        /// </summary>
        public string CurrentVersion { get; set; }
        /// <summary>
        /// 申请人
        /// </summary>
        public string Applicant { get; set; }
        /// <summary>
        /// 申请日期
        /// </summary>
        public string ApplicationDate { get; set; }
        /// <summary>
        /// 齐套率
        /// </summary>
        public string CompleteSetRate { get; set; }
        /// <summary>
        /// 图纸代号
        /// </summary>
        public string DrawingCode { get; set; }
        /// <summary>
        /// BOMID
        /// </summary>
        public string BomId { get; set; }

        /// <summary>
        /// BOM状态
        /// designing/Approving/purchasing/completed
        /// 设计中..审批中...采购中...已完成
        /// </summary>
        public string Status { get; set; } = BOMStatusDesc.Designing;

        public string StatusText { get => BOMStatusDesc.GetDesc(Status).Text; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
