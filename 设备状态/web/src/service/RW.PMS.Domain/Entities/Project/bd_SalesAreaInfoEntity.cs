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
    /// 市场片区信息
    /// </summary>
    [Table(Name = "bd_salesarea_info", OldName = "bd_SalesAreaInfo")]

    public class bd_SalesAreaInfoEntity : FullEntity
    {
        /// <summary>
        /// 市场片区编码
        /// </summary>
        public string AreaCode { get; set; }

        /// <summary>
        /// 市场片区名称
        /// </summary>
        public string AreaName { get; set; }

        /// <summary>
        /// 板块名称
        /// </summary>
        public int PlaceName { get; set; }

        /// <summary>
        /// 片区营销经理
        /// </summary>
        public int AreaGM { get; set; }

        /// <summary>
        /// 片区负责人
        /// </summary>
        public int AreaCharger { get; set; }

        /// <summary>
        /// 片区营销人员
        /// </summary>
        public int AreaSalesman { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public string Remark { get; set; }
    }
}
