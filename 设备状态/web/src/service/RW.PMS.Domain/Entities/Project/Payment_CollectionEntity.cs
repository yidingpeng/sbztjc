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
    /// 项目回款
    /// </summary>
    [Table(Name = "pro_payment_collection", OldName = "Payment_Collection")]

    public class Payment_CollectionEntity : FullEntity
    {
        /// <summary>
        /// 项目编号
        /// </summary>
        public int Pt_Id { get; set; }
        /// <summary>
        /// 开票日期
        /// </summary>
        public DateTime Payment_Time { get; set; }
        /// <summary>
        /// 已回款总额
        /// </summary>
        public decimal Payment_Cash { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public string Remark { get; set; }
        /// <summary>
        /// 回款类型
        /// </summary>
        public int ReturnType { get; set; }
        /// <summary>
        /// 回款方式
        /// </summary>
        public int ReturnWay { get; set; }
        /// <summary>
        /// 回款日期
        /// </summary>
        public DateTime ReturnDate { get; set; }
    }
}
