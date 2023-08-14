using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Project
{
    public class Contract_InfoDto
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
        /// 项目Id文本
        /// </summary>
        public string pt_idsTxt { get; set; }

        /// <summary>
        /// 项目编号文本
        /// </summary>
        public string pt_codesTxt { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string pt_Name { get; set; }

        /// <summary>
        /// 项目名称文本
        /// </summary>
        public string pt_NamesTxt { get; set; }

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
        public string payment_collection { get; set; }

        ///// <summary>
        ///// 合同扫描件
        ///// </summary>
        //public int ct_attachmentPdf { get; set; }

        ///// <summary>
        ///// 合同可编辑文件
        ///// </summary>
        //public int ct_attachmentWord { get; set; }

        /// <summary>
        /// 合同扫描件附地址
        /// </summary>
        public string[] ct_attachmentPdfUrl { get; set; }

        /// <summary>
        /// 合同可编辑文件地址
        /// </summary>
        public string[] ct_attachmentWordUrl { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 合同扫描件名称
        /// </summary>
        public string[] filePdfName { get; set; }

        /// <summary>
        /// 合同可编辑文件名称
        /// </summary>
        public string[] fileWordName { get; set; }

        /// <summary>
        /// 合同扫描件ids
        /// </summary>
        public int[] filePdfIds { get; set; }

        /// <summary>
        /// 合同可编辑文件ids
        /// </summary>
        public int[] fileWordIds { get; set; }

        /// <summary>
        /// 保存word文件id数组字符串
        /// </summary>
        public string ConWordFilesId { get; set; }

        /// <summary>
        /// 保存pdf文件id数组字符串
        /// </summary>
        public string ConPdfFilesId { get; set; }

        /// <summary>
        /// 市场片区名称
        /// </summary>
        public string MarketAreaTxt { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// 营销负责人名字
        /// </summary>
        public string PersonInChargeName { get; set; }

        /// <summary>
        /// 市场片区
        /// </summary>
        public int MarketArea { get; set; }

        /// <summary>
        /// 市场片区数组
        /// </summary>
        public int[] marketAreaArr { get; set; }

        /// <summary>
        /// 新增合同回款计划表id字符串
        /// </summary>
        public string NewConPayPlanStr { get; set; }

        /// <summary>
        /// 项目数量
        /// </summary>
        public int ProCount { get; set; }

        /// <summary>
        /// 项目单价
        /// </summary>
        public decimal ProUnitPrice { get; set; }

        /// <summary>
        /// 合同项目信息
        /// </summary>
        public List<ContractDetailDto> ContractDetailList { get; set; }
    }
}
