using RW.PMS.CrossCutting.EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Event
{
    public class WorkflowEventData : EventData
    {
        /// <summary>
        /// 流程标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 流程ID
        /// </summary>
        public int DataId { get; set; }
        /// <summary>
        /// 通知用户ID
        /// </summary>
        public int[] UserIds { get; set; }
        /// <summary>
        /// 流程动作
        /// </summary>
        public WorkflowActionEnums Action { get; set; }

        public string Content { get; set; }

        public string GetActionText()
        {
            switch (Action)
            {
                case WorkflowActionEnums.Pass: return "通过";
                case WorkflowActionEnums.Reject: return "驳回";
                case WorkflowActionEnums.Urging: return "催办";
                case WorkflowActionEnums.Send: return "抄送";
                case WorkflowActionEnums.Completed: return "完成";
                case WorkflowActionEnums.Publish: return "发起";
                default: return "未知";
            }
        }

        public bool NoMessageAction()
        {
            return Action == WorkflowActionEnums.Publish || UserIds == null || UserIds.Length == 0;
        }
    }

    /// <summary>
    /// 工作流动作枚举
    /// </summary>
    public enum WorkflowActionEnums
    {
        /// <summary>
        /// 通过
        /// </summary>
        Pass = 1,
        /// <summary>
        /// 驳回
        /// </summary>
        Reject = 2,
        /// <summary>
        /// 催办
        /// </summary>
        Urging = 3,
        /// <summary>
        /// 抄送
        /// </summary>
        Send = 4,
        /// <summary>
        /// 完成
        /// </summary>
        Completed = 5,
        /// <summary>
        /// 发起
        /// </summary>
        Publish = 6,
    }
}
