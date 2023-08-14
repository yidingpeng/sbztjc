using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.Project
{
    /// <summary>
    /// 项目联系人信息
    /// </summary>
    [Table(Name = "Project_ContactsInfo")]
    public class Project_ContactsInfo :FullEntity
    {
        /// <summary>
        /// 项目ID
        /// </summary>
        public int PID { get; set; }
        /// <summary>
        /// 联系人类别
        /// </summary>
        //public int ContactsType { get; set; }
        /// <summary>
        /// 负责板块ID
        /// </summary>
        public int FZBKId { get; set; }
        /// <summary>
        /// 联系人ID
        /// </summary>
        public int ContactsID { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public string Remark { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public int Dept { get; set; }


    }
}
