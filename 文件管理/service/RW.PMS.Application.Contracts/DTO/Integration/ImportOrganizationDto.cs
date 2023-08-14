using RW.PMS.Application.Contracts.DTO.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Integration
{
    public class ImportOrganizationDto
    {
        public string Password { get; set; }
        public FileOutputDto File { get; set; }
        /// <summary>
        /// 是否清空组织架构后再添加
        /// </summary>
        public bool TruncateOrganization { get; set; }
        /// <summary>
        /// 是否清空岗位后再添加
        /// </summary>
        public bool TruncatePost { get; set; }
        /// <summary>
        /// 是否清空用户后再添加
        /// </summary>
        public bool TruncateUser { get; set; }
    }
}
