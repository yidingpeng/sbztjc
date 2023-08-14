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
    /// WBS主表名
    /// </summary>
    [Table(Name = "pro_wbstabs")]
    public class WbsTabsEntity: FullEntity
    {
        /// <summary>
        /// 项目模板ID
        /// </summary>
        public int TemplateId { get; set; }
        /// <summary>
        /// 计划名称
        /// </summary>
        public string PlanName { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int State { get; set; }
    }
}
