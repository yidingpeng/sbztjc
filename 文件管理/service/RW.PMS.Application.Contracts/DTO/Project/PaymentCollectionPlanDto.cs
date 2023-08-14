using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Project
{
    public class PaymentCollectionPlanDto
    {
        /// <summary>
        /// id
        /// </summary>
        public int? Id { get; set; }

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
