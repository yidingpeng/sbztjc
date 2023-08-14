using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.Input.Message
{
    public class ESBSendMessageInput
    {
        /// <summary>
        /// 消息标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 消息类别 1通知 2待办 
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 消息详情（通常不做显示，主要以标题为准）
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// 消息来源，如“PLM”、“ERP”使用系统名称
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// 目标平台，默认为OA 可选的有：OA、APP、PLM、ERP
        /// </summary>
        public string TargetPlantform { get; set; }

        /// <summary>
        /// 目标用户，可以是部门，岗位，员工
        /// </summary>
        public Array Targets { get; set; }

        /// <summary>
        /// 发送方式，0立即发送；1定时发送；
        /// </summary>
        public int SendMode { get; set; }

        /// <summary>
        /// 如果Mode=0，该值为空；Mode=1,该值为时间，格式为“2022-07-02 16:00:00”
        /// </summary>
        public string SendModeValue { get; set; }
    }
}
