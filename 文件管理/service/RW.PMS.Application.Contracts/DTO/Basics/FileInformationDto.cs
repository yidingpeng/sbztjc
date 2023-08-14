using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Basics
{
    /// <summary>
    ///     设备文件表
    /// </summary>
    public class FileInformationDto
    {
        /// <summary>
        /// 文件绝对路径
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// 文件相对路径
        /// </summary>
        public string FileRelativePath { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 上传时间
        /// </summary>
        public DateTime DateCreateTime { get; set; }
        /// <summary>
        /// 文件变更时间
        /// </summary>
        public DateTime LastModifiedAt { get; set; }
    }
}
