using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.File
{
    public class FileQueryDto : PagedQuery
    {
        public string Name { get; set; }
        public string Type { get; set; }

        public bool IsMine { get; set; }
        /// <summary>
        /// 文件大小筛选，MB 0表示不限制
        /// </summary>
        public double MaxSize { get; set; }
    }
}
