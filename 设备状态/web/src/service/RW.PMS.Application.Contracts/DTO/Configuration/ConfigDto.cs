using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Configuration
{
    /// <summary>
    /// 系统配置列表
    /// </summary>
    public class ConfigListDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Code { get; set; }
        public string Value { get; set; }
        public string Desc { get; set; }

        public string LastModifyedUser { get; set; }

        public DateTime LastModifiedAt { get; set; }
    }
}
