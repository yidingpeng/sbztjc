using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Workflow
{
    public class WorkflowListDto
    {
        public int Id { get; set; }
        /// <summary>
        /// 工作流名称
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 工作流类型
        /// </summary>
        public string Type { get; set; }

        public string TypeKey { get; set; }

        /// <summary>
        /// 是否启用状态
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 工作流JSON数据
        /// </summary>
        public BaseFlowModel WorkFlowNode { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 流程数
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 允许用户抄送
        /// </summary>
        public bool AllowUserSend { get; set; }
    }
}
