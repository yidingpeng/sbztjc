using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.Material
{
    /// <summary>
    /// BOM信息
    /// </summary>
    [Table(Name = "v_pdm_bom", DisableSyncStructure = true)]
    public class pdm_bom
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// BOM编码
        /// </summary>
        public string BOMCode { get; set; }

        /// <summary>
        /// BOM名
        /// </summary>
        public string BOMName { get; set; }
        /// <summary>
        /// 项目号
        /// </summary>
        public string ProjectId { get; set; }
        /// <summary>
        /// 项目名
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }
    }
}
