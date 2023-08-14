using RW.PMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.System
{
    // 用户产生的附件
    [Table("sys_user_attach")]
    public class UserAttachmentEntity : FullEntity
    {
        /// <summary>
        /// 附件类型
        /// 用户流程：userflow
        /// 审核附件：approve
        /// 新闻资讯：inform
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 流程ID
        /// </summary>
        public int DataId { get; set; }

        /// <summary>
        /// 文件ID
        /// </summary>
        public int FileId { get; set; }

        /// <summary>
        /// 附件名称
        /// </summary>
        public string Name { get; set; }
    }
}
