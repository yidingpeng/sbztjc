using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.Project
{
    /// <summary>
    /// 合同信息子表
    /// </summary>
    [Table(Name = "contractDetail")]

    public class ContractDetailEntity : FullEntity
    {
        /// <summary>
        /// 父级ID
        /// </summary>
        public int PID { get; set; }
        /// <summary>
        /// 项目编号
        /// </summary>
        public string ProjectId { get; set; }

        /// <summary>
        /// 设备数量
        /// </summary>
        public int EquipmentCount { get; set; }

        /// <summary>
        /// 设备合同单价
        /// </summary>
        public decimal EquipmentUnitPrice { get; set; }

        /// <summary>
        /// 设备合同总价
        /// </summary>
        public decimal EquipmentTotalPrice { get; set; }

        /// <summary>
        /// 要求交付日期
        /// </summary>
        public string DeliverDate { get; set; }

        /// <summary>
        /// 预付款
        /// </summary>
        public decimal AdvanceCharge { get; set; }

        /// <summary>
        /// 进度款
        /// </summary>
        public decimal ProgressPayment { get; set; }

        /// <summary>
        /// 验收款
        /// </summary>
        public decimal AcceptancePayment { get; set; }

        /// <summary>
        /// 质保金
        /// </summary>
        public decimal RetentionMoney { get; set; }

        /// <summary>
        /// 结算款
        /// </summary>
        public decimal SettlementFunds { get; set; }

        /// <summary>
        /// 尾款
        /// </summary>
        public decimal BalancePayment { get; set; }
    }
}
