using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.User
{
    public class UserListDto
    {
        public int Id { get; set; }
        /// <summary>
        /// 用户账号
        /// </summary>
        public string Account { get; set; }

        public string Fullname { get; set; }
        /// <summary>
        /// 机构ID
        /// </summary>
        public int OrgId { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public int LastModified { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public int IsDeleted { get; set; }
        /// <summary>
        /// 用户头像
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string Department { get; set; }
        /// <summary>
        /// 岗位ID
        /// </summary>
        public int PostId { get; set; }
        /// <summary>
        /// 岗位
        /// </summary>
        public string Post { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string[] Roles { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Telnum { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 是否在职
        /// </summary>
        public bool OnWork { get; set; }
        /// <summary>
        /// 员工编号
        /// </summary>
        public string EmployeeId { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }
    }

}
