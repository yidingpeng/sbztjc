using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Project
{
    public class bd_SalesAreaInfoDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// 市场片区编码
        /// </summary>
        public string AreaCode { get; set; }

        /// <summary>
        /// 市场片区名称
        /// </summary>
        public string AreaName { get; set; }

        /// <summary>
        /// 板块名称id
        /// </summary>
        public int PlaceName { get; set; }

        /// <summary>
        /// 板块名称
        /// </summary>
        public string PlaceNameText { get; set; }

        /// <summary>
        /// 片区营销经理id
        /// </summary>
        public int AreaGM { get; set; }

        /// <summary>
        /// 片区营销经理
        /// </summary>
        public string AreaGMText { get; set; }

        /// <summary>
        /// 片区负责人id
        /// </summary>
        public int AreaCharger { get; set; }

        /// <summary>
        /// 片区负责人
        /// </summary>
        public string AreaChargerText { get; set; }

        /// <summary>
        /// 片区营销人员id
        /// </summary>
        public int AreaSalesman { get; set; }

        /// <summary>
        /// 片区营销人员
        /// </summary>
        public string AreaSalesmanText { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public string Remark { get; set; }
    }
}
