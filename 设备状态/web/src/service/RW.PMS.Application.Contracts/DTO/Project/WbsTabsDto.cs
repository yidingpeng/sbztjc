using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Project
{
    public class WbsTabsDto
    {
        /// <summary>
        /// 编码ID
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 项目模板ID
        /// </summary>
        public int TemplateId { get; set; }
        /// <summary>
        /// 计划名称
        /// </summary>
        public string PlanName { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 项目计划信息
        /// </summary>
        public List<WbsPlanDto> WBSPlan { get; set; }
    }
}
