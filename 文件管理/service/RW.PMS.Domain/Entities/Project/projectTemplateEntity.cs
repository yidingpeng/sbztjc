using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.Project
{
    /// <summary>
    /// 项目模板信息表
    /// </summary>
    [Table(Name = "pro_template", OldName = "projectTemplate")]

    public class projectTemplateEntity : FullEntity
    {

        /// <summary>
        /// 模板名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 项目类型
        /// </summary>

        public int ProjectType { get; set; }

        /// <summary>
        /// attr1
        /// </summary>
        public string Attr1 { get; set; }
        /// <summary>
        /// attr2
        /// </summary>
        public string Attr2 { get; set; }
        /// <summary>
        /// 状态 1启用  0停用
        /// </summary>
        public bool State { get; set; }
    }
}
