using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RW.PMS.Application.Contracts.Input.System
{
    public class UserInput
    {
        public int? Id { get; set; }

        /// <summary>
        /// 用户账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 机构id
        /// </summary>
        public int OrgId { get; set; }
        /// <summary>
        /// 修改密码时的老密码(用于比对）
        /// </summary>
        public string OldPassword { get; set; }
    }
}
