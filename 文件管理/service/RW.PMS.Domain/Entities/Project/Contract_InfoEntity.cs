using System;
using System.ComponentModel.DataAnnotations;
using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;

namespace RW.PMS.Domain.Entities.Project
{
    /// <summary>
    /// 合同信息表
    /// </summary>
    [Table(Name = "pro_contract", OldName = "Contract_Info")]

    public class Contract_InfoEntity : FullEntity
    {
        /// <summary>
        /// 项目Id
        /// </summary>
        public int pt_id { get; set; }

        /// <summary>
        /// 项目Id文本
        /// </summary>
        public string pt_idsTxt { get; set; }

        /// <summary>
        /// 内部合同编号
        /// </summary>
        public string ct_code { get; set; }

        /// <summary>
        /// 外部合同编号
        /// </summary>
        public string ExternalCt_code { get; set; }

        /// <summary>
        /// 合同名称
        /// </summary>
        public string contractName { get; set; }

        /// <summary>
        /// 合同金额
        /// </summary>
        public decimal ct_cash { get; set; }

        /// <summary>
        /// 合同税率
        /// </summary>
        public decimal ctTaxRate { get; set; }

        /// <summary>
        /// 合同签订日期
        /// </summary>
        public DateTime ct_signingDate { get; set; }

        /// <summary>
        /// 合同交货日期
        /// </summary>
        public DateTime ct_deliveryDate { get; set; }

        /// <summary>
        /// 回款条款
        /// </summary>
        [MaxLength(500)]
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
        [MaxLength(500)]
        public string Remark { get; set; }


    }
}
