using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.Purchase
{
    /// <summary>
    ///     附件实体
    /// </summary>
    [Table(Name = "qr_code_pic")]
    public class Qr_codePicEntity : FullEntity
    {
        /// <summary>
        ///     父级ID
        /// </summary>
        public int PID { get; set; }

        /// <summary>
        ///     文件类型
        /// </summary>
        [MaxLength(255)]
        public string ContentType { get; set; }

        /// <summary>
        ///     文件名称
        /// </summary>
        [MaxLength(100)]
        public string FileName { get; set; }

        /// <summary>
        ///     文件大小
        /// </summary>
        public long FileSize { get; set; }

        /// <summary>
        ///     文件根目录
        /// </summary>
        [MaxLength(200)]
        public string RootPath { get; set; }

        /// <summary>
        ///     文件相对路径
        /// </summary>
        public string RelativePath { get; set; }
    }
}

