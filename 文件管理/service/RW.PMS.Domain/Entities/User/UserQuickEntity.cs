using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.User
{
    /// <summary>
    /// 用户首页快捷方式
    /// </summary>
    [Table(Name = "user_quick")]
    public class UserQuickEntity : FullEntity
    {
        /// <summary>
        /// 显示标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 显示的图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 图标的颜色
        /// </summary>
        public string Color { get; set; }
        /// <summary>
        /// 链接类型 0 站内链接；1 外部链接
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 链接地址
        /// </summary>
        public string Link { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderIndex { get; set; }
    }
}
