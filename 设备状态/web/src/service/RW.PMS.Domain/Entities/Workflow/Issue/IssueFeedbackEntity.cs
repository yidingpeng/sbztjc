using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.Workflow.Issue
{
    /// <summary>
    /// 问题反馈流程
    /// </summary>
    [Table(Name = "wf_user_issue")]
    public class IssueFeedbackEntity : FullEntity
    {
        /// <summary>
        /// 用户流程ID
        /// </summary>
        public int UserFlowId { get; set; }
        /// <summary>
        /// 问题类型[字典Key]
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 项目号
        /// </summary>
        public string ProjectNo { get; set; }
        /// <summary>
        /// 项目明后才能
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// 问题描述,长文本，使用富文本编辑器
        /// </summary>
        [Column(StringLength = -1)]
        public string Desc { get; set; }
        /// <summary>
        /// 工艺员
        /// </summary>
        public string Technician { get; set; }

        /// <summary>
        /// 原因分析
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// 处理结果
        /// </summary>
        public string ExecuteResult { get; set; }
    }
}
