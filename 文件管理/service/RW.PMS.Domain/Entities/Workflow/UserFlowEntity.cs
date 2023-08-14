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
    /// 用户提交的流程
    /// </summary>
    [Table(Name = "wf_user_data")]
    public class UserFlowEntity : FullEntity
    {
        /// <summary>
        /// 流程标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 流程类型
        /// 与系统字典关联
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 应用的流程ID
        /// </summary>
        public int WorkflowId { get; set; }

        /// <summary>
        /// 流程图数据 冗余数据（避免因流程图修改，导致已提交的流程出现问题）
        /// </summary>
        [Column(StringLength = -1)]
        public string FlowData { get; set; }

        /// <summary>
        /// 流水号，根据类型+日期 自动生成
        /// </summary>
        public string SN { get; set; }

        /// <summary>
        /// 流程状态
        /// approving 审批中；closed 已关闭；completed 已完成;Countersigning 会签中
        /// </summary>
        [Column(StringLength = 30)]
        public UserFlowStatus Status { get; set; }

        /// <summary>
        /// 当前审核人ID
        /// </summary>
        public int AuditUserId { get; set; }
        /// <summary>
        /// 当前审核人姓名（添加时冗余）
        /// </summary>
        public string AuditUsername { get; set; }

        /// <summary>
        /// 结束时间可为空，如果未结束
        /// </summary>
        public DateTime? OverTime { get; set; }

        /// <summary>
        /// 当前流程
        /// </summary>
        public string CurrentNode { get; set; }

        /// <summary>
        /// 是否允许用户抄送
        /// </summary>
        public bool AllowUserSend { get; set; }

        /// <summary>
        /// 关闭流程
        /// </summary>
        public void Close()
        {
            Status = UserFlowStatus.Closed;
        }

        /// <summary>
        /// 开始进入审批流程
        /// </summary>
        public void StartApproving()
        {
            Status = UserFlowStatus.Approving;
        }

        /// <summary>
        /// 开始进入会签环节
        /// </summary>
        public void StartCountersigning()
        {
            Status = UserFlowStatus.Countersigning;
        }

        /// <summary>
        /// 已完成
        /// </summary>
        public void Completed()
        {
            Status = UserFlowStatus.Completed;
        }

        /// <summary>
        /// 设置当前节点
        /// </summary>
        public void SetCurrentNode(WorkFlowNodeEntity node)
        {
            CurrentNode = $"[{node.NodeName}]" + node.Handler?.Substring(0, Math.Min(node.Handler.Length, 100));
        }

        /// <summary>
        /// 重置流程状态
        /// </summary>
        public void ResetStatus()
        {
            Status = UserFlowStatus.Saved;
        }
    }

    /// <summary>
    /// 用户流程状态枚举
    /// </summary>
    public enum UserFlowStatus
    {
        /// <summary>
        /// 暂时保存
        /// </summary>
        Saved = 0,
        /// <summary>
        /// 提交后，进入审核中
        /// </summary>
        Approving = 1,
        /// <summary>
        /// 已关闭
        /// </summary>
        Closed = -1,
        /// <summary>
        /// 会签中
        /// </summary>
        Countersigning = 2,
        /// <summary>
        /// 已完成
        /// </summary>
        Completed = 100,
    }
}
