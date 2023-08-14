using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Project
{
    public class ProjectBasicsListDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 父节点ID
        /// </summary>
        public int ParentId { get; set; }
        /// <summary>
        /// 项目编号
        /// </summary>
        public string ProjectCode { get; set; }
        /// <summary>
        /// 初始项目编号
        /// </summary>
        public string InitialProjectCode { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// 项目分类
        /// </summary>
        public int ProjectClass { get; set; }
        /// <summary>
        /// 项目状态
        /// </summary>
        public int ProjectStatus { get; set; }
        /// <summary>
        /// 业务类型
        /// </summary>
        public int BusinessType { get; set; }
        /// <summary>
        /// 重要等级
        /// </summary>
        public int UrgentGrade { get; set; }
        /// <summary>
        /// 客户公司
        /// </summary>
        public int ClientCompany { get; set; }
        /// <summary>
        /// 业主单位
        /// </summary>
        public int OwnerUnitID { get; set; }
        /// <summary>
        /// 业主单位
        /// </summary>
        public string OwnerUnit { get; set; }

        /// <summary>
        /// 业主单位名称
        /// </summary>
        public string OwnerUnitName { get; set; }
        /// <summary>
        /// 项目接收日期
        /// </summary>
        public DateTime ProjectReceiveDate { get; set; }
        /// <summary>
        /// 合同交货日期
        /// </summary>
        public DateTime? ContractDeliveryDate { get; set; }
        /// <summary>
        /// 预计使用日期
        /// </summary>
        public DateTime ExpectedUseDate { get; set; }
        /// <summary>
        /// 项目背景
        /// </summary>
        public string ProjectBackground { get; set; }
        /// <summary>
        /// 项目概述
        /// </summary>
        public string ProjectSummary { get; set; }
        /// <summary>
        /// 是否已有合同
        /// </summary>
        public string IsContract { get; set; }
        /// <summary>
        /// 是否已有技术协议
        /// </summary>
        public string IsTechnicalAgreement { get; set; }
        /// <summary>
        /// 招标编号
        /// </summary>
        public string BiddingNo { get; set; }
        /// <summary>
        /// 项目管理方式
        /// </summary>
        public int ProManagementStyle { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int ProState { get; set; }
        /// <summary>
        /// Attr1
        /// </summary>
        public string Attr1 { get; set; }
        /// <summary>
        /// Attr2
        /// </summary>
        public string Attr2 { get; set; }
        /// <summary>
        /// Attr3
        /// </summary>
        public string Attr3 { get; set; }
        /// <summary>
        /// Attr4
        /// </summary>
        public string Attr4 { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 技术协议附件
        /// </summary>
        public string TechnicalAgreementPath { get; set; }
        /// <summary>
        /// 总价
        /// </summary>
        public decimal SalesPrice { get; set; }
        /// <summary>
        /// 合同信息表ID
        /// </summary>
        public int ContractID { get; set; }

        /// <summary>
        /// 产线类型
        /// </summary>
        public string ProLine { get; set; }
    }
}
