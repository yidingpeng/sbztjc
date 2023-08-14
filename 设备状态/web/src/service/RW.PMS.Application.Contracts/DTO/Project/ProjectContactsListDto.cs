using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Project
{
    public class projectcontactsListDto
    {
        public int? Id { get; set; }
        /// <summary>
        /// 项目ID
        /// </summary>
        public int PID { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string PName { get; set; }
        /// <summary>
        /// 项目编号
        /// </summary>
        public string projectCode { get; set; }
        
        /// <summary>
        /// 联系人类别
        /// </summary>
        //public int ContactsType { get; set; }
        /// <summary>
        /// 联系人类别名
        /// </summary>
        //public string ContactsTypeName { get; set; }
        /// <summary>
        /// 负责板块ID
        /// </summary>
        public int FZBKId { get; set; }
        /// <summary>
        /// 负责板块名称
        /// </summary>
        public string FZBKName { get; set; }
        /// <summary>
        /// 联系人ID
        /// </summary>
        public int ContactsID { get; set; }
        /// <summary>
        /// 联系人账号
        /// </summary>
        public string ContactsName { get; set; }
        /// <summary>
        /// 联系人客户公司名称
        /// </summary>
        public string CurCompany { get; set; }
        /// <summary>
        /// 电话1
        /// </summary>
        public string Telephone1 { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public int Dept { get; set; }

        /// <summary>
        /// 部门文本
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string sex { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public string? Roles { get; set; }
    }
}
