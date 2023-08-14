using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.Purchase
{
    /// <summary>
    /// 报检表
    /// </summary>
    [Table(Name = "mat_cd")]
    public class MatCDEntity : FullEntity
    {
        /// <summary>
        /// 二维码
        /// </summary>
        public string QrCode { get; set; }


        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
