using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Workflow
{
    public class AddWorkflowDto
    {
        /// <summary>
        /// 工作流名称
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 工作流类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 是否启用状态
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 工作流JSON数据
        /// </summary>
        public string WorkFlowData { get; set; }

        public bool AllowUserSend { get; set; }
    }
}
