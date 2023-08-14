using RW.PMS.Application.Contracts.DTO.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Workflow
{
    public class UserFlowModelDto : EditUserFlowDto
    {
        public string SN { get; set; }
        public string Requested { get; set; }
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 结束时间可为空，如果未结束
        /// </summary>
        public DateTime? OverTime { get; set; }
        /// <summary>
        /// 详情（目前只使用问题详情）
        /// </summary>
        public IssueDto Detail { get; set; }
        public List<AuditListDto> Records { get; set; }

        public JsonNode FlowData { get; set; }
        /// <summary>
        /// 当前流程
        /// </summary>
        public string CurrentFlow { get; set; }
        public List<WorkflowNodeDto> Nodes { get; set; }
        /// <summary>
        /// 是否可以审批
        /// </summary>
        public bool CanApproval { get; set; }
        /// <summary>
        /// 是否允许用户抄送
        /// </summary>
        public bool AllowUserSend { get; set; }

    }
}
