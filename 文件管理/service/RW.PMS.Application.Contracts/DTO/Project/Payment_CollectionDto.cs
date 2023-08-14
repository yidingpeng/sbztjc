using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Project
{
    public class Payment_CollectionDto
    {
        public int Id { get; set; }
        /// <summary>
        /// 项目编号
        /// </summary>
        public int Pt_Id { get; set; }
        ///// <summary>
        ///// 开票日期
        ///// </summary>
        //public DateTime Payment_Time { get; set; }
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
        /// 回款方式
        /// </summary>
        public int ReturnWay { get; set; }
        /// <summary>
        /// 回款日期
        /// </summary>
        public DateTime ReturnDate { get; set; }

        /// <summary>
        /// 项目已签合同金额
        /// </summary>
        public decimal pt_SignedCash { get; set; }

        /// <summary>
        /// 项目未签合同金额
        /// </summary>
        public decimal pt_UnSignedCash { get; set; }

    }
}
