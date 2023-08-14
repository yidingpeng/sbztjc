using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Workflow
{
    public class IssueDto : IWorkflowDetailDto
    {
        public int Id { get; set; }
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
        public string Desc { get; set; }
        /// <summary>
        /// 工艺员
        /// </summary>
        public string Technician { get; set; }

        public string Type { get; set; }

    }
}
