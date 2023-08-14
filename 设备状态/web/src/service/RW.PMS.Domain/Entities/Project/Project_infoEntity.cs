using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using RW.PMS.Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.Project
{
    /// <summary>
    ///     项目信息管理主表
    /// </summary>
    [Table(Name = "project_info")]
    public class Project_infoEntity : FullEntity
    {
        /// <summary>
        ///    项目名称
        /// </summary>
        public string pt_Name { get; set; }

        /// <summary>
        ///    项目号
        /// </summary>
        public string pt_Code { get; set; }


 
        /// <summary>
        ///    类别_来自于数据字典
        /// </summary>

        public int pt_CategoryID { get; set; }

        /// <summary>
        ///    紧急程度_来自于数据字典
        /// </summary>
        public int pt_UrgencyID { get; set; }




        /// <summary>
        ///    备注
        /// </summary>
        public string pt_Remark { get; set; }



        /// <summary>
        ///    客户公司名称
        /// </summary>
        public int pt_Company { get; set; }

     

        /// <summary>
        ///    数量
        /// </summary>
        public int pt_Num { get; set; }


        /// <summary>
        ///    项目接收日期
        /// </summary>
        public DateTime pt_XMJSRQ { get; set; }

        /// <summary>
        ///    要求交付日期
        /// </summary>
        public DateTime pt_YQJFRQ { get; set; }



        /// <summary>
        ///    当前状态
        /// </summary>
        public int pt_State { get; set; }



        /// <summary>
        ///    项目管理人员
        /// </summary>
        public string pt_XMGLRY { get; set; }

        /// <summary>
        ///    产品经理
        /// </summary>
        public string pt_CPJL { get; set; }


        /// <summary>
        ///    业务类型
        /// </summary>
        public int Businesstype { get; set; }



        /// <summary>
        ///    预计使用时间
        /// </summary>
        public DateTime pt_YJSYSJ { get; set; }

        /// <summary>
        ///    项目背景
        /// </summary>
        public string pt_background { get; set; }

        /// <summary>
        ///    项目概述
        /// </summary>
        public string pt_summary { get; set; }

        /// <summary>
        ///    是否已有合同
        /// </summary>
        public int IsContract { get; set; }

        /// <summary>
        ///    是否已有技术协议
        /// </summary>
        public int IsAgreement { get; set; }

        /// <summary>
        ///    项目管理方式
        /// </summary>

        public int pt_Manage { get; set; }
    }
}
