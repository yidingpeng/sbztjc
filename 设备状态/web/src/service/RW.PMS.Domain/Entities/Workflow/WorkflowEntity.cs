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
    /// 工作流实体
    /// </summary>
    [Table(Name = "wf_main")]
    public class WorkflowEntity : FullEntity
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
        [Column(StringLength = -1)]
        public string WorkFlowData { get; set; }
    }
}
