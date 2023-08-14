using RW.PMS.Application.Contracts.Project;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Project
{
    public class Contract_InfoInput 
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// 项目编号
        /// </summary>
        public string pt_codeTxt { get; set; }

        /// <summary>
        /// 项目编号id
        /// </summary>
        public int pt_id { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string pt_Name { get; set; }

        /// <summary>
        /// 合同编号
        /// </summary>
        public string ct_code { get; set; }

        /// <summary>
        /// 合同名称
        /// </summary>
        public string contractName { get; set; }

        /// <summary>
        /// 合同金额
        /// </summary>
        public decimal ct_cash { get; set; }

        /// <summary>
        /// 合同签订日期
        /// </summary>
        public DateTime ct_signingDate { get; set; }

        /// <summary>
        /// 合同交货日期
        /// </summary>
        public DateTime ct_deliveryDate { get; set; }

        /// <summary>
        /// 付款方式
        /// </summary>
        public int pay_mode { get; set; }

        /// <summary>
        /// 付款方式
        /// </summary>
        public string pay_modeTxt { get; set; }

        /// <summary>
        /// 回款条款
        /// </summary>
        public string payment_collection { get; set; }

        /// <summary>
        /// 合同扫描件
        /// </summary>
        public int ct_attachmentPdf { get; set; }

        /// <summary>
        /// 合同可编辑文件
        /// </summary>
        public int ct_attachmentWord { get; set; }

        /// <summary>
        /// 合同扫描件附地址
        /// </summary>
        public string ct_attachmentPdfUrl { get; set; }

        /// <summary>
        /// 合同可编辑文件地址
        /// </summary>
        public string ct_attachmentWordUrl { get; set; }

        /// <summary>
        /// 项目数量
        /// </summary>
        public int ProCount { get; set; }

        /// <summary>
        /// 项目单价
        /// </summary>
        public decimal ProUnitPrice { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 合同扫描件名称
        /// </summary>
        public string filePdfName { get; set; }

        /// <summary>
        /// 合同可编辑文件名称
        /// </summary>
        public string fileWordName { get; set; }
    }
}
