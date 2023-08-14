using System;

namespace RW.PMS.Application.Contracts.Output.System
{
    public class UserView
    {
        public int Id { get; set; }
        /// <summary>
        /// 用户账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 用户真实姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 机构ID
        /// </summary>
        public int OrgId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int LastModifiedBy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime LaseModifiedAt { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public int IsDeleted { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string OrgName { get; set; }

    }
}
