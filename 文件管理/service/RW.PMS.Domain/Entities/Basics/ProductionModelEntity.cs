using System;
using System.ComponentModel.DataAnnotations;
using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;

namespace RW.PMS.Domain.Entities.Basics
{
    /// <summary>
    ///     产品型号表
    /// </summary>
    [Table(Name = "base_productionModel")]
    public class ProductionModelEntity : FullEntity
    {
        /// <summary>
        ///     父级ID
        /// </summary>
        public int PID { get; set; }

        /// <summary>
        ///     产品型号编码
        /// </summary>
        [MaxLength(150)]
        public string PmodelCode { get; set; }
         
        /// <summary>
        ///     产品型号名称
        /// </summary>
        [MaxLength(150)]
        public string Pname { get; set; }

        /// <summary>
        ///     产品型号简称
        /// </summary>
        [MaxLength(150)]
        public string PnameShort { get; set; }

        /// <summary>
        ///     图号
        /// </summary>
        [MaxLength(50)]
        public string DrawNo { get; set; }

        /// <summary>
        ///     物料号
        /// </summary>
        [MaxLength(50)]
        public string MaterialNo { get; set; }

        /// <summary>
        ///     产品型号类型
        /// </summary>
        public int ProductionModelType { get; set; }

        /// <summary>
        ///     产品类型
        /// </summary>
        public int ProductionType { get; set; }

        /// <summary>
        ///     地铁/高铁/火车线路号
        /// </summary>
        [MaxLength(50)]
        public string TrainLine { get; set; }

        /// <summary>
        ///     地铁/高铁/火车型号
        /// </summary>
        [MaxLength(50)]
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
        [MaxLength(255)]
        public string remark { get; set; }

    }
}
