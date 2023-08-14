using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Basics
{
    public class ProductionModelDto
    {
        public int Id { get; set; }

        /// <summary>
        ///     父级ID
        /// </summary>
        public int PID { get; set; }

        /// <summary>
        ///     产品型号编码
        /// </summary>
        public string PmodelCode { get; set; }

        /// <summary>
        ///     产品型号名称
        /// </summary>
        public string Pname { get; set; }

        /// <summary>
        ///     产品型号简称
        /// </summary>
        public string PnameShort { get; set; }

        /// <summary>
        ///     图号
        /// </summary>
        public string DrawNo { get; set; }

        /// <summary>
        ///     物料号
        /// </summary>
        public string MaterialNo { get; set; }

        /// <summary>
        ///     产品型号类型
        /// </summary>
        public int ProductionModelType { get; set; }
        public string ProductionModelTypeTxt { get; set; }
        /// <summary>
        ///     产品类型
        /// </summary>
        public int ProductionType { get; set; }
        public string ProductionTypeTxt { get; set; }

        /// <summary>
        ///     地铁/高铁/火车线路号
        /// </summary>
        public string TrainLine { get; set; }

        /// <summary>
        ///     地铁/高铁/火车型号
        /// </summary>
        public string TraiNModel { get; set; }

        /// <summary>
        ///     同产品型号排序号
        /// </summary>
        public int OrderIndex { get; set; }

        /// <summary>
        ///     状态
        /// </summary>
        public int Pstatus { get; set; }

        /// <summary>
        ///     备注
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        ///     额外字段
        /// </summary>
        public List<ProductExtendDto> extendList { get; set; }
    }
}
