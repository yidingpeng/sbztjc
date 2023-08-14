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
    /// 配置阶段表
    /// </summary>
    [Table(Name = "pro_template_phase", OldName = "projectTemplatePhase")]

    public class projectTemplatePhaseEntity : FullEntity
    {
        /// <summary>
        /// 模板ID
        /// </summary>
        public int templateID { get; set; }
        /// <summary>
        /// 阶段名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        ///attr1
        /// </summary>
        public string Attr1 { get; set; }
        /// <summary>
        ///attr2
        /// </summary>
        public string Attr2 { get; set; }
    }
}
