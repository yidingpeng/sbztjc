using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Project
{
    public class ProjectInvoicingDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// 开票编号
        /// </summary>
        public string InvoiceNo { get; set; }

        /// <summary>
        /// 项目id
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 项目编号
        /// </summary>
        public string ProjectCode { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
		/// 项目接收日期
		/// </summary>
		public DateTime ProjectReceiveDate { get; set; }

        /// <summary>
        /// 合同id
        /// </summary>
        public int ContactId { get; set; }

        /// <summary>
        /// 合同编号
        /// </summary>
        public string ct_code { get; set; }

        /// <summary>
        /// 合同金额
        /// </summary>
        public decimal? ct_cash { get; set; }

        /// <summary>
        /// 不含税金额
        /// </summary>
        public decimal AmounExcludingTax { get; set; }

        /// <summary>
        /// 合同未开票金额
        /// </summary>
        public decimal ct_UninvoicedAmount { get; set; }

        /// <summary>
        /// 合同应开票总金额
        /// </summary>
        public decimal ct_InvoicedAmount { get; set; }

        /// <summary>
        /// 开票日期
        /// </summary>
        public DateTime InvoiceDate { get; set; }

        /// <summary>
        /// 开票金额
        /// </summary>
        public decimal InvoiceAmount { get; set; }

        /// <summary>
        /// 开票税率
        /// </summary>
        public decimal InvoiceTaxRate { get; set; }

        /// <summary>
        /// 开票申请人id
        /// </summary>
        public int ApplyMan { get; set; }

        /// <summary>
        /// 开票申请人
        /// </summary>
        public string ApplyManTxt { get; set; }

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
        public string Remark { get; set; }
    }
}
