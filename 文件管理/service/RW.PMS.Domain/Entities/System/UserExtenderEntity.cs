using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.System
{
    // 用户扩展信息
    [Table(Name = "sys_user_extender", OldName = "UserExtender")]
    public class UserExtenderEntity : Base.Entity<int>
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        [Column(IsNullable = false)]
        public int UserId { get; set; }
        /// <summary>
        /// 性别，男，女
        /// </summary>
        [Column(StringLength = 2)]
        public string Sex { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public string Department { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        [Column(StringLength = 20)]
        public string Telnum { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        [Column(OldName = "avator")]
        public string Avatar { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? Born { get; set; }

        /// <summary>
        /// 个人简介
        /// </summary>
        public string Description { get; set; }
    }
}
