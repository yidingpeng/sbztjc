using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Project
{
    public class ContactListDto
    {
        //主键ID
        public int? Id { get; set; }

        /// <summary>
        /// 单位编号id
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// 单位编号
        /// </summary>
        public string ComCode { get; set; }

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
        public int? ContactsCategory { get; set; }

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
        public string ContactsBackground { get; set; }

        /// <summary>
        /// 联系人类别文本
        /// </summary>
        public string ContactsCategorytext { get; set; }

        /// <summary>
        /// 性别名称
        /// </summary>
        public string SexName { get; set; }

        /// <summary>
        /// 部门文本
        /// </summary>
        public string Departmenttext { get; set; }

        /// <summary>
        /// 客户公司名称
        /// </summary>
        public string CurCompany { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// 营销负责人名字
        /// </summary>
        public string PersonInChargeName { get; set; }

        /// <summary>
        /// 营销负责人id
        /// </summary>
        public int? PersonInCharge { get; set; }

        /// <summary>
        /// 市场片区名称
        /// </summary>
        public string MarketAreaTxt { get; set; }

        /// <summary>
        /// 客户级别名称
        /// </summary>
        public string ClientRankTxt { get; set; }
    }
}
