using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public class FileInformation
    {
        public int ID { get; set; }
        public string OldFullPath { get; set; }
        /// <summary>
        /// 绝对路径
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// 文件相对路径
        /// </summary>
        public string FileRelativePath { get; set; }
        public string OldFiledName { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string FileName { get; set; }
        public DateTime DateCreateTime { get; set; }
        public DateTime DateUpdateTime { get; set; }
    }
}
