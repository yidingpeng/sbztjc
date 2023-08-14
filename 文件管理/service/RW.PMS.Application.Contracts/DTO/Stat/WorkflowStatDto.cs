using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Stat
{
    public class WorkflowStatDto
    {
        /// <summary>
        /// 工作流总数
        /// </summary>
        public int Total { get; set; }
        /// <summary>
        /// 添加的流程（周期：当日、本周、本月之类）
        /// </summary>
        public int Added { get; set; }
        /// <summary>
        /// 处理的流程（周期：当日、本周、本月之类）
        /// </summary>
        public int Executed { get; set; }

    }
}
