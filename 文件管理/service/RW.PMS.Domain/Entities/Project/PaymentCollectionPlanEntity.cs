using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.Project
{
    /// <summary>
    /// 客户公司信息表
    /// </summary>
    [Table(Name = "pro_payment_collection_plan", OldName = "PaymentCollectionPlan")]

    public class PaymentCollectionPlanEntity : FullEntity
    {
        /// <summary>
        ///     合同ID
        /// </summary>
        public int Ct_ID { get; set; }

        /// <summary>
        ///     回款类型
        /// </summary>
        public string PaymentCTypeID { get; set; }

        /// <summary>
        ///     比例
        /// </summary>
        public decimal PaymentCProportion { get; set; }

        /// <summary>
        ///     合同金额回款计划
        /// </summary>
        public decimal ConAmountCPlan { get; set; }

        /// <summary>
        /// 是否编辑状态
        /// </summary>
        public bool Edit { get; set; }
    }
}
