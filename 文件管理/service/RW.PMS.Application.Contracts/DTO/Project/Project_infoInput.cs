using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Project
{
    public class Project_infoInput
    {
        public int? Id { get; set; }
        /// <summary>
        ///    项目名称
        /// </summary>
        public string pt_Name { get; set; }

        /// <summary>
        ///    项目号
        /// </summary>
        public string pt_Code { get; set; }

        /// <summary>
        ///    客户公司名称
        /// </summary>
        public string pt_Company { get; set; }


        /// <summary>
        ///    数量
        /// </summary>
        public int pt_Num { get; set; }

        /// <summary>
        ///    项目接收日期
        /// </summary>
        public string pt_XMJSRQ { get; set; }

        /// <summary>
        ///    要求交付日期
        /// </summary>
        public string pt_YQJFRQ { get; set; }

        /// <summary>
        ///    市场负责人
        /// </summary>
        public string pt_SCFZR { get; set; }

        /// <summary>
        ///    项目管理人员
        /// </summary>
        public string pt_XMGLRY { get; set; }


        /// <summary>
        /// 紧急程度等级_来自于数据字典
        /// </summary>
        public string pt_UrgencyID { get; set; }
        /// <summary>
        ///    业务类型
        /// </summary>
        public string Businesstype { get; set; }

        /// <summary>
        /// 项目分类
        /// </summary>
        public string pt_CategoryID { get; set; }

        /// <summary>
        /// 项目状态
        /// </summary>
        public string pt_State { get; set; }
        /// <summary>
        ///    预计使用时间
        /// </summary>
        public string pt_YJSYSJ { get; set; }

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
        public string IsContract { get; set; }

        /// <summary>
        ///    是否已有技术协议
        /// </summary>
        public string IsAgreement { get; set; }

        /// <summary>
        ///    项目管理方式
        /// </summary>
        public string pt_Manage { get; set; }
    }
}
