using System;

namespace RW.PMS.Application.Contracts.DTO.Problem
{
    public class ProblemMsgSendDto
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 问题反馈ID
        /// </summary>
        public int ProblemId { get; set; }
        /// <summary>
        /// 到达处理人员的时间
        /// </summary>
        public DateTime? Toa { get; set; }

        /// <summary>
        /// 发送消息内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 发送消息时间
        /// </summary>
        public DateTime? TxdTime { get; set; }


        /// <summary>
        /// 手机号：消息发送用户ID
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 项目编号
        /// </summary>
        public string ProjectCode { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 是否已发送消息0:否,1是
        /// </summary>
        public int IsToa { get; set; }
    }
}
