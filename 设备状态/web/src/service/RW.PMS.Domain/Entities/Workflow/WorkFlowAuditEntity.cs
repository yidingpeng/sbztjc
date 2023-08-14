using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.Workflow
{
    /// <summary>
    /// 流程操作日志
    /// </summary>
    [Table(Name = "wf_user_audit")]
    public class WorkFlowAuditEntity : FullEntity
    {
        /// <summary>
        /// 用户流程ID
        /// </summary>
        public int UserWorkflowId { get; set; }

        /// <summary>
        /// 评论意见
        /// </summary>
        [Column(StringLength = -1)]
        public string Comments { get; set; }

        /// <summary>
        /// 审批结果，true为通过，false驳回
        /// </summary>
        public bool Result { get; set; }

    }
}
