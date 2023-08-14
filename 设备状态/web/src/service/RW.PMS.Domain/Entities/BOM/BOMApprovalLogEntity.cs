using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RW.PMS.Domain.Entities.BOM
{
    /// <summary>
    /// BOM审批记录表
    /// </summary>
    [Table(Name = "`rw.esb`.`bom_approval_log`")]
    public class BOMApprovalLogEntity : IEntity<int>
    {
        /// <summary>
        /// 唯一编号
        /// </summary>
        [Column(IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }
        /// <summary>
        /// BOM表ID
        /// </summary>
        public int BomId { get; set; }
        /// <summary>
        /// 审批节点
        /// </summary>
        public string Node { get; set; }
        /// <summary>
        /// 审批用户
        /// </summary>
        public string User { get; set; }
        /// <summary>
        /// 审批时间
        /// </summary>
        public DateTime ApprovedTime { get; set; }
        /// <summary>
        /// 审批详情
        /// </summary>
        [Column(StringLength = -1)]
        public string Desc { get; set; }
        /// <summary>
        /// 审批结果
        /// </summary>
        public bool Result { get; set; }
    }
}
