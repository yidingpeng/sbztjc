using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Project
{
    public class TaskProcessInfoSearchDto : PagedQuery
    {
        public int Id { get; set; }
        /// <summary>
        /// 项目类型
        /// </summary>
        public string ProjectClassID { get; set; }
        /// <summary>
        /// 任务编码
        /// </summary>
        public string TaskCode { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName { get; set; }
    }
}
