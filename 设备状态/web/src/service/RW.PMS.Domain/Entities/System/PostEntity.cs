using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.System
{
    /// <summary>
    /// 岗位
    /// </summary>
    [Table(Name = "sys_post")]
    public class PostEntity : FullOrderedEntity
    {
        /// <summary>
        /// 岗位编号，自定义的岗位编号，可用于显示，也可用于数据同步
        /// 允许为空，但不建议为空
        /// </summary>
        public string PostNo { get; set; }
        /// <summary>
        /// 职位名称
        /// </summary>
        public string PostName { get; set; }
        /// <summary>
        /// 所属部门ID
        /// </summary>
        public int OrgId { get; set; }
        /// <summary>
        /// 部门名称冗余
        /// </summary>
        [Column(IsIgnore = true)]
        public string OrgName { get; set; }

        /// <summary>
        /// 岗位备注
        /// </summary>
        [Column(StringLength = -1)]
        public string Desc { get; set; }
    }
}
