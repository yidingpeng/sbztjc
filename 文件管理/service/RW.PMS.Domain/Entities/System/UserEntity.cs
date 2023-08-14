using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;

namespace RW.PMS.Domain.Entities.System
{
    //用户实体，freesqlbug：如果改名时，会先修改sys_users的comment,但sys_users还是User表，未修改前
    /// <summary>
    /// 用户基本信息
    /// </summary>
    [Table(Name = "sys_users", OldName = "User")]
    public class UserEntity : FullOrderedEntity
    {
        /// <summary>
        /// 用户唯一的编号，原则上不允许为空，主要用于与各个系统数据同步
        /// 通常为10000+序号
        /// 也可以是 组织+部门+员工号 组合
        /// </summary>
        public string UserNo { get; set; }

        /// <summary>
        ///     账号，账号建议为手机号
        /// </summary>
        [MaxLength(50)]
        public string Account { get; set; }

        /// <summary>
        ///     密码
        /// </summary>
        [MaxLength(255)]
        //[Column(CanUpdate = false)]
        public string Password { get; set; }

        /// <summary>
        ///     真实姓名
        /// </summary>
        [MaxLength(20)]
        public string RealName { get; set; }

        /// <summary>
        /// 部门Id（主部门）
        /// </summary>
        public int OrgId { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        [Column(IsIgnore = true)]
        public string OrgName { get; set; }
        /// <summary>
        /// 岗位ID（主岗位）
        /// </summary>
        public int PostId { get; set; }
        /// <summary>
        /// 岗位名称
        /// </summary>
        [Column(IsIgnore = true)]
        public string PostName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public dynamic Parent { get; set; }

        /// <summary>
        /// 上次登录时间
        /// </summary>
        public DateTime? LastLogin { get; set; }
        /// <summary>
        /// 本次登录时间
        /// </summary>
        public DateTime? CurrentLoginTime { get; set; }

        /// <summary>
        /// 登录次数
        /// </summary>
        [DefaultValue(0)]
        public int LoginCount { get; set; }
    }
}