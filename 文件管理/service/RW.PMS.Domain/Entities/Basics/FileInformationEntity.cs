using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.Basics
{
    /// <summary>
    ///     文件保存路径表
    /// </summary>
    [Table(Name = "FileInformation")]
    public class FileInformationEntity : FullEntity
    {
        /// <summary>
        /// 文件绝对路径
        /// </summary>
        [MaxLength(200)]
        public string FilePath { get; set; }
        /// <summary>
        /// 文件相对路径
        /// </summary>
        [MaxLength(200)]
        public string FileRelativePath { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        [MaxLength(200)]
        public string FileName { get; set; }

        public DateTime DateCreateTime { get; set; }
        public DateTime DateUpdateTime { get; set; }
    }
}
