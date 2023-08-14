using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Workflow
{
    public class UserFlowListDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        /// <summary>
        /// 流程类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 流程号
        /// </summary>
        public string SN { get; set; }
        /// <summary>
        /// 发起人
        /// </summary>
        public string Requested { get; set; }
        /// <summary>
        /// 发起时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? OverTime { get; set; }
        /// <summary>
        /// 当前状态，起草、审批中、结束、驳回
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 当前节点 具体审批人
        /// </summary>
        public string CurrentNode { get; set; }

        public UserFlowListDto SetRequested(string realName)
        {
            this.Requested = realName;
            return this;
        }
    }
}
