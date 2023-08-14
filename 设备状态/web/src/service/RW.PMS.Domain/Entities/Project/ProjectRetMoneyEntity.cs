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
    /// 项目质保金信息
    /// </summary>
    [Table(Name = "pro_ret_money", OldName = "ProjectRetMoney")]

    public class ProjectRetMoneyEntity: FullEntity
    {
        /// <summary>
        /// 项目编号
        /// </summary>
        public int ProjectID { get; set; }
        /// <summary>
        /// 质保金比例
        /// </summary>
        public decimal RetMoneyProportion { get; set; }
        /// <summary>
        /// 质保金金额
        /// </summary>
        public decimal RetMoneyAmount { get; set; }
        /// <summary>
        /// 质保期限
        /// </summary>
        public string WarrantyPeriod { get; set; }
        /// <summary>
        /// 质保到期日
        /// </summary>
        public string ExpirationDate { get; set; }
        /// <summary>
        /// 已到期质保金金额
        /// </summary>
        public decimal AlrExpirationMoney { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public string Remark { get; set; }
    }
}
