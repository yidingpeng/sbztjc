using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using RW.PMS.Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.Project
{
    /// <summary>
    ///     联系人表
    /// </summary>
    [Table(Name = "bd_contacts", OldName = "Contacts")]

    public class ContactsEntity: FullEntity
    {
        /// <summary>
        /// 单位编号
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// 联系人姓名
        /// </summary>
        public string ContactsName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int? Sex { get; set; }

        /// <summary>
        /// 电话1
        /// </summary>
        public string Telephone1 { get; set; }

        /// <summary>
        /// 电话2
        /// </summary>
        public string Telephone2 { get; set; }

        /// <summary>
        /// 联系人类别
        /// </summary>
        public int ContactsCategory  { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public int Department { get; set; }

        /// <summary>
        /// 岗位
        /// </summary>
        public string Post { get; set; }

        /// <summary>
        /// 历史任职履历
        /// </summary>
        [MaxLength(500)]
        public string HistoryJob { get; set; }

        /// <summary>
        /// 办公电话
        /// </summary>
        public string Tel { get; set; }

        /// <summary>
        /// 传真
        /// </summary>
        public string Fax { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        public string Nation { get; set; }

        /// <summary>
        /// 负责业务
        /// </summary>
        public string ResponsibleBusiness { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 微信号
        /// </summary>
        public string Wechat { get; set; }

        /// <summary>
        /// QQ
        /// </summary>
        public string QQ { get; set; }

        /// <summary>
        /// 办公地址
        /// </summary>
        public string OfficeAddress { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// 爱好
        /// </summary>
        public string Hobby { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int? Age { get; set; }

        /// <summary>
        /// 本科学校
        /// </summary>
        public string College { get; set; }

        /// <summary>
        /// 本科毕业年份
        /// </summary>
        public DateTime CollegeAt { get; set; }

        /// <summary>
        /// 硕博学校
        /// </summary>
        public string ThurberSchool { get; set; }

        /// <summary>
        /// 硕博毕业年份
        /// </summary>
        public DateTime ThurberSchoolAt { get; set; }

        /// <summary>
        /// 行业人脉背景
        /// </summary>
        [MaxLength(500)]
        public string ContactsBackground { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public string Remark { get; set; }
    }
}
