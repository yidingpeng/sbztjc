using FreeSql.DataAnnotations;
using RW.PMS.CrossCutting.Exceptions;
using RW.PMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.BOM
{
    /// <summary>
    /// BOM清单表
    /// </summary>
    [Table(Name = "`rw.esb`.`pdm_bom`")]
    public class BomTableEntity : IEntity<int>, ISoftDelete
    {
        [Column(IsPrimary = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// BOM编号，唯一编号，一般为自动生成
        /// </summary>
        public string BOMCode { get; set; }
        /// <summary>
        /// BOM名称
        /// </summary>
        public string BOMName { get; set; }
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
        public List<BomItemEntity> Items { get; set; }
        /// <summary>
        /// 删除时间
        /// </summary>
        [Column(Position = -2)]
        public DateTime? DeletedTime { get; set; }
        /// <summary>
        /// 逻辑删除
        /// </summary>
        [Column(Position = -1)]
        public bool IsDeleted { get; set; }

        public void SetVersion(BOMVersion version)
        {
            this.Version = version.VersionText;
            this.VersionIndex = version.VersionIndex;
        }

        public BOMVersion GetVersion()
        {
            var version = new BOMVersion(this.VersionIndex);
            return version;
        }

        public void NextVersion()
        {
            SetVersion(GetVersion().Next());
        }

        public BomTableEntity CreateNext()
        {
            var newTable = new BomTableEntity
            {
                ProjectCode = this.ProjectCode,
                ProjectName = this.ProjectName,
                ApprovalBy = this.ApprovalBy,
                AuditBy = this.AuditBy,
                BOMCode = this.BOMCode,
                BOMName = this.BOMName,
                ComitBy = this.ComitBy,
                CreateBy = this.CreateBy,
                IsOld = this.IsOld,
                PurchaseBy = this.PurchaseBy,
                Remark = this.Remark,
                RequiredTime = this.RequiredTime,
                Status = BOMStatusDesc.Designing,
            };
            newTable.SetVersion(this.GetVersion());
            newTable.NextVersion();
            return newTable;
        }

        /// <summary>
        /// 审批BOM表
        /// </summary>
        public void Audit(bool result)
        {
            if (result)
            {
                if (Status == BOMStatusDesc.Auditing)
                    Status = BOMStatusDesc.Purchasing;
                else if (Status == BOMStatusDesc.Purchasing)
                    Status = BOMStatusDesc.Approving;
                else if (Status == BOMStatusDesc.Approving)
                    Status = BOMStatusDesc.Completed;
            }
            else Status = BOMStatusDesc.Rejected;
        }

        /// <summary>
        /// 是否可以编辑
        /// </summary>
        public bool CanEdit()
        {
            //设计中、创建中、已驳回 允许继续编辑和发布
            return Status == BOMStatusDesc.Creating || Status == BOMStatusDesc.Designing || Status == BOMStatusDesc.Rejected;
        }

        /// <summary>
        /// 撤回BOM
        /// </summary>
        public void Undo()
        {
            Status = BOMStatusDesc.Designing;
        }

        /// <summary>
        /// 是否正在审批中
        /// </summary>
        public bool IsApproving()
        {
            return Status == BOMStatusDesc.Approving || Status == BOMStatusDesc.Auditing || Status == BOMStatusDesc.Purchasing;
        }

        /// <summary>
        /// 是否完成
        /// </summary>
        public bool IsCompleted()
        {
            return Status == BOMStatusDesc.Completed;
        }

        internal bool CanRemove()
        {
            return Status != BOMStatusDesc.Approving && Status != BOMStatusDesc.Auditing && Status != BOMStatusDesc.Purchasing && Status != BOMStatusDesc.Completed;
        }
    }
}
