using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Project
{
    public class Payment_CollectionOutput
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 项目编号
        /// </summary>
        public int Pt_Id { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string Pt_Name { get; set; }

        /// <summary>
        /// 项目编号
        /// </summary>
        public string Pt_Code { get; set; }
        /// <summary>
        /// 合同未开票金额
        /// </summary>
        public decimal Non_Payment { get; set; }
        /// <summary>
        /// 已回款总额
        /// </summary>
        public decimal Payment_Cash { get; set; }
        /// <summary>
        /// 合同回款比
        /// </summary>
        public decimal Payment_Precent { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 回款类型
        /// </summary>
        public int ReturnType { get; set; }
        /// <summary>
        /// 回款类型文本
        /// </summary>
        public string ReturnTypeText { get; set; }
        /// <summary>
        /// 回款方式
        /// </summary>
        public int ReturnWay { get; set; }
        /// <summary>
        /// 回款方式文本
        /// </summary>
        public string ReturnWayText { get; set; }
        /// <summary>
        /// 回款日期
        /// </summary>
        public string ReturnDate { get; set; }
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
        /// 项目已签合同金额
        /// </summary>
        public decimal pt_SignedCash { get; set; }

        /// <summary>
        /// 项目未签合同金额
        /// </summary>
        public decimal pt_UnSignedCash { get; set; }

        /// <summary>
        /// 合同已开票金额
        /// </summary>
        public decimal ctInvoiceAmount { get; set; }

        /// <summary>
        /// 合同未开票金额
        /// </summary>
        public decimal ctUnInvoiceAmount { get; set; }
            
    }
}
