using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Notice
{
    public class NoticeOutputDto
    {
        public int Id { get; set; }
        /// <summary>
        /// 通知标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 通知类型，比如
        /// </summary>
        public string Type { get; set; } = "默认";

        /// <summary>
        /// 通知内容，HTML格式的内容
        /// 由富文本编辑器创建
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 阅读人数
        /// </summary>
        public int ReadCount { get; set; }

        /// <summary>
        /// 通知状态，0待发布，1已发布，-1已撤销
        /// </summary>
        public int Status { get; set; }

        public string StatusText { get; set; }

        public DateTime Time { get; set; }

        public DateTime UpdateTime { get; set; }

        public int[] Users { get; set; }
    }
}
