using RW.PMS.Application.Contracts.DTO.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Workflow
{
    public class AuditListDto
    {
        public int Id { get; set; }
        /// <summary>
        /// 用户流程ID
        /// </summary>
        public int UserWorkflowId { get; set; }

        /// <summary>
        /// 评论意见
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// 审批结果，true为通过，false驳回
        /// </summary>
        public bool Result { get; set; }

        /// <summary>
        /// 处理人
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// 审批时间
        /// </summary>
        public DateTime Time { get; set; }

        public FileOutputDto[] Files { get; set; }
    }
}
