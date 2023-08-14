using Microsoft.AspNetCore.Http;

namespace RW.PMS.Application.Contracts.DTO.Project
{
    public class ContractInfoFilesListDto
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
        ///     文件根目录
        /// </summary>
        public string RootPath { get; set; }

        /// <summary>
        ///     文件相对路径
        /// </summary>
        public string RelativePath { get; set; }
    }
}
