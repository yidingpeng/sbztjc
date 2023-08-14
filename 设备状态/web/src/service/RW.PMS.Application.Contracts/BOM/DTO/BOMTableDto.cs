using RW.PMS.Domain.Entities.BOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.BOM.DTO
{
    public class BOMTableDto
    {
        public int Id { get; set; }
        public string BOMName { get; set; }
        public string BOMCode { get; set; }
        /// <summary>
        /// 所属项目编号
        /// </summary>
        public string ProjectCode { get; set; }
        /// <summary>
        /// 所属项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 总体要求时间
        /// </summary>
        public DateTime? RequiredTime { get; set; }

        /// <summary>
        /// BOM状态
        /// designing/Approving/purchasing/completed
        /// 设计中..审批中...采购中...已完成
        /// </summary>
        public string Status { get; set; } = BOMStatusDesc.Designing;

        public string StatusText { get => BOMStatusDesc.GetDesc(Status).Text; }

        /// <summary>
        /// 版本号，通常以A.0、B.0、C.0、D.0 ...来命名版本
        /// O、I字母不可用，超过Z.0时，可继续使用A.1、B.1以此类推.
        /// </summary>
        public string Version { get; set; }
        public int VersionIndex { get; set; } = 1;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        public string CreateBy { get; set; }
        /// <summary>
        /// 提交时间
        /// </summary>
        public DateTime? ComitTime { get; set; }
        public string ComitBy { get; set; }
        /// <summary>
        /// 审批时间（产品经理）
        /// </summary>
        public DateTime? AuditTime { get; set; }
        public string AuditBy { get; set; }
        /// <summary>
        /// 采购
        /// </summary>
        public string PurchaseBy { get; set; }
        /// <summary>
        /// 审核时间（部长）
        /// </summary>
        public DateTime? ApprovalTime { get; set; }
        public string ApprovalBy { get; set; }

        public bool IsOld { get; set; }
        public string Remark { get; set; }

        public bool CanApprove
        {
            get =>
    isManager && (Status == BOMStatusDesc.Auditing || Status == BOMStatusDesc.Approving || Status == BOMStatusDesc.Purchasing)
    || (Status == BOMStatusDesc.Auditing && user == AuditBy)
    || (Status == BOMStatusDesc.Purchasing && user == PurchaseBy)
    || (Status == BOMStatusDesc.Approving && user == ApprovalBy)
    ;
        }

        public bool CanUndo { get => CreateBy == user || ComitBy == user; }

        private bool isManager = false;
        public BOMTableDto SetManager(bool value)
        {
            this.isManager = value;
            return this;
        }

        private string user = "";
        public BOMTableDto SetUser(string user)
        {
            this.user = user;
            return this;
        }
    }
}
