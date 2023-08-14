using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Project
{
    public class TaskPlanSearchDto: PagedQuery
    {
        /// <summary>
        /// 计划单据号
        /// </summary>
        public string planReceiptNo { get; set; }
        /// <summary>
        /// 项目ID
        /// </summary>
        public int projectID { get; set; }
        /// <summary>
        /// 任务模式
        /// </summary>
        public int taskMode { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        public int roleID { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>
        public int reviewer { get; set; }
        /// <summary>
        /// 责任人 
        /// </summary>
        public int responsible { get; set; }
    }
}
