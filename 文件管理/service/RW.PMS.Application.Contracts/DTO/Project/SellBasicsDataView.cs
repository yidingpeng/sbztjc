﻿using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Project
{
    public class SellBasicsDataView
    {
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
        public string ProjectStatusName { get; set; }
        /// <summary>
        /// 项目分类名称
        /// </summary>
        public string ProjectClassName { get; set; }
        /// <summary>
        /// 业务类型
        /// </summary>
        public int BusinessType { get; set; }
        /// <summary>
        /// 业务类型
        /// </summary>
        public string BusinessTypeName { get; set; }
        /// <summary>
        /// 重要等级
        /// </summary>
        public int UrgentGrade { get; set; }
        /// <summary>
        /// 重要等级
        /// </summary>
        //public string UrgentGradeName { get; set; }
        /// <summary>
        /// 客户公司
        /// </summary>
        public int ClientCompany { get; set; }
        /// <summary>
        /// 客户公司
        /// </summary>
        public string ClientCompanyName { get; set; }
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
        public string ProjectReceiveDates { get { return ProjectReceiveDate.ToString("yyyy-MM-dd"); } }
        /// <summary>
        /// 合同交货日期
        /// </summary>
        public DateTime ContractDeliveryDate { get; set; }
        public string ContractDeliveryDates { get { return ContractDeliveryDate.ToString("yyyy-MM-dd"); } }
        /// <summary>
        /// 预计使用日期
        /// </summary>
        public DateTime ExpectedUseDate { get; set; }
        public string ExpectedUseDates { get { return ExpectedUseDate.ToString("yyyy-MM-dd"); } }
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
        /// 项目管理方式
        /// </summary>
        public string ProManagementStyleName { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int ProState { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string ProStateName { get; set; }
        /// <summary>
        /// 图纸代号
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

        public bool HasChildren { get; set; }
        /// <summary>
        /// 技术协议附件
        /// </summary>
        public string TechnicalAgreementPath { get; set; }
        /// <summary>
        /// 合同信息表ID
        /// </summary>
        //public int ContractID { get; set; }
        /// <summary>
        /// 合同信息名称
        /// </summary>
        //public string ContractName { get; set; }

        /// <summary>
        /// 项目总金额
        /// </summary>
        public decimal TotalProAmount { get; set; }
        /// <summary>
        /// 项目未签合同总金额
        /// </summary>
        public decimal ProUnsignedContractAmount { get; set; }
        /// <summary>
        /// 合同应开票总金额
        /// </summary>
        public decimal ConInvoiceableAmount { get; set; }
        /// <summary>
        /// 项目已开票金额
        /// </summary>
        public decimal ProInvoicedAmount { get; set; }
        /// <summary>
        /// 合同未开票金额
        /// </summary>
        public decimal ConUnbilledAmount { get; set; }
        /// <summary>
        /// 已回款总金额
        /// </summary>
        public decimal ReturnedTotalAmount { get; set; }
        /// <summary>
        /// 项目已回款比
        /// </summary>
        public decimal ProReturnedScale { get; set; }
        /// <summary>
        /// 质保金金额
        /// </summary>
        public decimal WarrantyAmount { get; set; }
        /// <summary>
        /// 已到期质保金金额
        /// </summary>
        public decimal ExpirationWarrantyAmount { get; set; }
        /// <summary>
        /// 质保金比例
        /// </summary>
        public decimal WarrantyScale { get; set; }
        /// <summary>
        /// 市场片区名称
        /// </summary>
        public string MarketAreaTxt { get; set; }
        /// <summary>
        /// 营销经理
        /// </summary>
        public string PersonInChargeName { get; set; }
        /// <summary>
        /// 营销经理
        /// </summary>
        public string MarketingManager { get; set; }
        /// <summary>
        /// 项目经理
        /// </summary>
        public string ProjectManager { get; set; }
        /// <summary>
        /// 产品经理
        /// </summary>
        public string ProductManager { get; set; }
        /// <summary>
        /// 分管领导
        /// </summary>
        public string LeadersManager { get; set; }

        /// <summary>
        /// 合同签订日期（最后一次合同签订时间）
        /// </summary>
        public string ContractSigningDate { get; set; }
        /// <summary>
        /// 近期开票时间（最后一次开票签订时间）
        /// </summary>
        public string RecentlyBilledDate { get; set; }
        /// <summary>
        /// 近期回款时间（最后一次合同签订时间）
        /// </summary>
        public string RecentlyPaymentDate { get; set; }
    }
}
