using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;

namespace RW.PMS.Domain.Entities.Workflow
{
    /// <summary>
    /// 工作流统计信息（按日统计）
    /// </summary>
    [Table(Name = "wf_stat")]
    public class WorkflowStatEntity : Entity<int>
    {
        /// <summary>
        /// 工作流ID
        /// </summary>
        public int WorkflowId { get; set; }
        /// <summary>
        /// 统计的日期（天）
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// 提交的数量
        /// </summary>
        public int Count { get; set; }
    }
}
