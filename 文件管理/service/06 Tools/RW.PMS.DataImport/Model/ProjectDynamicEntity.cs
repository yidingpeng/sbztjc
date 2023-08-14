using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.Project
{
    /// <summary>
    /// 项目动态
    /// </summary>
    [Table(Name = "ProjectDynamic")]
    public class ProjectDynamicEntity : FullEntity
    {
        /// <summary>
        /// 动态类型
        /// </summary>
        public int dyType { get; set; }
        /// <summary>
        /// 项目ID
        /// </summary>
        public int projectID { get; set; }
        /// <summary>
        /// 操作内容
        /// </summary>
        [MaxLength(1000)]
        public string operationContent{get; set; }
        /// <summary>
        /// attr1
        /// </summary>
        public string attr1 { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(1000)]
        public string remark { get; set; }

    }
}
