using System;
using System.ComponentModel.DataAnnotations;
using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;

namespace RW.PMS.Domain.Entities.Project
{
    /// <summary>
    /// 合同信息表
    /// </summary>
    [Table(Name = "pro_invoicing", OldName = "ProjectInvoicing")]

    public class ProjectInvoicingEntity : FullEntity
    {
        /// <summary>
        /// 开票编号
        /// </summary>
        public string InvoiceNo { get; set; }
        
        /// <summary>
        /// 项目id
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 合同id
        /// </summary>
        public int ContactId { get; set; }

        /// <summary>
        /// 开票日期
        /// </summary>
        public DateTime InvoiceDate { get; set; }

        /// <summary>
        /// 开票金额
        /// </summary>
        public decimal InvoiceAmount { get; set; }

        /// <summary>
        /// 不含税金额
        /// </summary>
        public decimal AmounExcludingTax { get; set; }

        /// <summary>
        /// 开票税率
        /// </summary>
        public decimal InvoiceTaxRate { get; set; }

        /// <summary>
        /// 开票申请人
        /// </summary>
        public int ApplyMan { get; set; }

        /// <summary>
        /// 开票是否挂账
        /// </summary>
        public bool IsCredit { get; set; }

        /// <summary>
        /// 挂账日期
        /// </summary>
        public DateTime CreditDate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public string Remark { get; set; }


    }
}
