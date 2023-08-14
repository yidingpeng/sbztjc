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
    [Table(Name = "pro_stage")]
    public class ConfigureStageEntity : FullEntity
    {
        /// <summary>
        /// 项目模板ID
        /// </summary>
        public int TemplateId { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        ///阶段名
        /// </summary>
        public string StageName { get; set; }
    }
}
