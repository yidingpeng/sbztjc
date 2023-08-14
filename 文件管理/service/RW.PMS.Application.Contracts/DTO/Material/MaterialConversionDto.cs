using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Material
{
    public class MaterialConversionDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 物料编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 规格型号
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// 基本单位
        /// </summary>
        public string BasicUnit { get; set; }

        /// <summary>
        /// 换算单位
        /// </summary>
        public string HsUnit { get; set; }

        /// <summary>
        /// 换算数量 N ：1
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
