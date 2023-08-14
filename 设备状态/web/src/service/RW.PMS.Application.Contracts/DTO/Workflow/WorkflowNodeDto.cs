using RW.PMS.Domain.Entities.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Workflow
{
    public class WorkflowNodeDto
    {
        public int Id { get; set; }
        /// <summary>
        /// 节点标识，用于组成链表结构，有系统自动生成
        /// </summary>
        public string NodeId { get; set; }

        public NodeActionEnums NodeType { get; set; }

        /// <summary>
        /// 用户流程ID
        /// </summary>
        public int UserFlowId { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        public string NodeName { get; set; }

        /// <summary>
        /// 处理人
        /// </summary>
        public string Handler { get; set; }

        /// <summary>
        /// 相关人员ID，使用逗号分隔的用户ID
        /// </summary>
        public string UserIds { get; set; }

        /// <summary>
        /// 已审核的用户ID
        /// </summary>
        public string AuditUserIds { get; set; }

        /// <summary>
        /// 下一个节点ID，该ID==0时表示最后的节点，流程完成
        /// </summary>
        public string NextNodeId { get; set; }

        /// <summary>
        /// 执行下一个节点条件
        /// auto 自动跳转下一节点
        /// and userID全部处理后
        /// or[1]  userID部分处理后[数量]
        /// timeout[1] 超时自动审批[分钟]
        /// </summary>
        public string NextType { get; set; }

        /// <summary>
        /// 已执行的
        /// </summary>
        public WorkflowNodeStatus Status { get; set; }
    }
}
