using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.System
{
    /// <summary>
    ///     附件实体
    /// </summary>
    [Table(Name = "AttachmentUpload")]
    public class AttachmentUploadEntity : FullEntity
    {
        /// <summary>
        ///     文件类型
        /// </summary>
        public string ContentType { get; set; }
        /// <summary>
        ///     文件ID
        /// </summary>
        public string FileID { get; set; }
        /// <summary>
        ///     文件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        ///     文件大小
        /// </summary>
        public long FileSize { get; set; }

        /// <summary>
        ///     文件根目录
        /// </summary>
        public string RootPath { get; set; }

        /// <summary>
        ///     文件相对路径
        /// </summary>
        public string RelativePath { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime ExpireDate { get; set; }
        /// <summary>
        /// 是否查看
        /// </summary>
        public int IsView { get; set; }
    }
}
