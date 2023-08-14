using FreeSql.DataAnnotations;
using RW.PMS.CrossCutting.Extender;
using RW.PMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.Workflow
{
    /// <summary>
    /// 流程节点
    /// 每当用户发布流程时，根据流程图，自动生成节点数据
    /// </summary>
    [Table(Name = "wf_user_node")]
    public class WorkFlowNodeEntity : FullEntity
    {
        /// <summary>
        /// 节点标识，用于组成链表结构，有系统自动生成
        /// </summary>
        public string NodeId { get; set; }
        /// <summary>
        /// 用户流程ID
        /// </summary>
        public int UserFlowId { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        public string NodeName { get; set; }
        /// <summary>
        /// 节点类别，1、发起人；2、审核；3、抄送；4、条件
        /// </summary>
        public NodeActionEnums NodeType { get; set; }

        /// <summary>
        /// 审核方式
        /// </summary>
        public ExamineModeEnums ExamineMode { get; set; }

        /// <summary>
        /// 处理人
        /// </summary>
        [Column(StringLength = 2000)]
        public string Handler { get; set; }

        ///// <summary>
        ///// ID的类型， 0未指定，1用户，2角色，3主管
        ///// </summary>
        //public int IdType { get; set; }

        /// <summary>
        /// 相关人员ID，使用逗号分隔的用户ID
        /// </summary>
        [Column(StringLength = 200)]
        public string UserIds { get; set; }

        /// <summary>
        /// 已审核的用户
        /// </summary>
        [Column(StringLength = 200)]
        public string AuditUserIds { get; set; }

        /// <summary>
        /// 已通知的用户ID，使用‘,’号分隔
        /// </summary>
        [Column(StringLength = -1)]
        public string NotifyUserIds { get; set; }

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

        /// <summary>
        /// 超时时间（小时）
        /// </summary>
        public int Timeout { get; set; }
        /// <summary>
        /// 超时后，处理
        /// true 表示通过
        /// false 表示拒绝
        /// </summary>
        public bool TimeoutHandle { get; set; }

        ///// <summary>
        ///// 添加已审核的用户
        ///// </summary>
        //public void AddAuditUser(int uid)
        //{
        //    var arr = AuditUserIds.StringToArray<int>().ToList();
        //    arr.Add(uid);
        //    AuditUserIds = arr.ArrayToString();
        //}

        /// <summary>
        /// 添加已审核的用户
        /// </summary>
        public void AddAuditUsers(params int[] ids)
        {
            var arr = AuditUserIds.StringToArray<int>().ToList();
            arr.AddRange(ids);
            AuditUserIds = arr.ArrayToString();
        }

        /// <summary>
        /// 设置相关用户ID
        /// </summary>
        public void SetUserIds(int[] ids)
        {
            UserIds = ids.ArrayToString();
        }

        /// <summary>
        /// 设置完成状态
        /// </summary>
        public void Completed()
        {
            Status = WorkflowNodeStatus.Completed;
        }

        ///// <summary>
        ///// 节点是否完成
        ///// </summary>
        //public bool IsCompleted()
        //{
        //    return Status == WorkflowNodeStatus.Completed || Status == WorkflowNodeStatus.Skip;
        //}

        /// <summary>
        /// 审核中
        /// </summary>
        public void Approving()
        {
            Status = WorkflowNodeStatus.Approving;
        }

        /// <summary>
        /// 跳过节点
        /// 通常是由于没有审核的用户或用户在之前的节点已审核
        /// </summary>
        public void SkipFlow()
        {
            Status = WorkflowNodeStatus.Skip;
        }


        /// <summary>
        /// 判断用户和审核的用户是否都已完成
        /// </summary>
        public bool IsCompleted()
        {
            return Status == WorkflowNodeStatus.Completed
                || Status == WorkflowNodeStatus.Skip
                || UserIds.StringToArray<int>().CompareArray(AuditUserIds.StringToArray<int>())
                ;
        }

        /// <summary>
        /// 重置节点状态 驳回时使用
        /// </summary>
        public void Reset()
        {
            Status = WorkflowNodeStatus.Default;
            AuditUserIds = String.Empty;
        }
    }

    /// <summary>
    /// 工作流状态，包含待处理、已执行、已取消
    /// </summary>
    public enum WorkflowNodeStatus
    {
        /// <summary>
        /// 待处理
        /// </summary>
        Default = 0,
        /// <summary>
        /// 已执行的
        /// </summary>
        Completed = 1,
        /// <summary>
        /// 审批中
        /// </summary>
        Approving = 2,
        /// <summary>
        /// 驳回
        /// </summary>
        Reject = 3,
        /// <summary>
        /// 跳过节点
        /// </summary>
        Skip = 4,
        /// <summary>
        /// 已取消
        /// </summary>
        Canceled = -1,
    }

    /// <summary>
    /// 节点类别枚举
    /// </summary>
    public enum NodeActionEnums
    {
        /// <summary>
        /// 未指定
        /// </summary>
        Default = 0,
        /// <summary>
        /// 发起人
        /// </summary>
        Promoter = 1,
        /// <summary>
        /// 审核人
        /// </summary>
        Approver = 2,
        /// <summary>
        /// 抄送
        /// </summary>
        Send = 3,
        /// <summary>
        /// 条件
        /// </summary>
        Condition = 4,
        /// <summary>
        /// 完成节点
        /// </summary>
        Completed = 9,
    }

    /// <summary>
    /// 审批方式
    /// </summary>
    public enum ExamineModeEnums
    {
        /// <summary>
        /// 依次审批
        /// </summary>
        OneByOne = 1,
        /// <summary>
        /// 会签审批
        /// </summary>
        AllOf = 2,
        /// <summary>
        /// 或签
        /// </summary>
        AnyOne = 3,
    }
}
