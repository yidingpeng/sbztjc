using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.Output.Project
{
    public class Contract_InfoOutput
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

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
        /// 回款条款
        /// </summary>
        public string payment_collection { get; set; }

        /// <summary>
        /// 合同附件
        /// </summary>
        public dynamic ct_attachment { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
