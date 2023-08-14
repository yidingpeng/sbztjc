using System;
using System.ComponentModel.DataAnnotations;
using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;

namespace RW.PMS.Domain.Entities.Basics
{
    /// <summary>
    ///     工器具基础信息图片实体
    /// </summary>
    [Table(Name = "base_Tool_detail")]
    public class ToolDetailEntity : FullEntity
    {
        /// <summary>
        ///     父级ID
        /// </summary>
        public int PID { get; set; }

        /// <summary>
        ///     文件类型
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        ///     文件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        ///     文件大小
        /// </summary>
        public long FileSize { get; set; }

        /// <summary>
        ///     路径
        /// </summary>
        public string RootPath { get; set; }

        /// <summary>
        ///     相对路径
        /// </summary>
        public string RelativePath { get; set; }
    }
}
