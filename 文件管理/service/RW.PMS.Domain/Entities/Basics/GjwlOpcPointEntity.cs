using System;
using System.ComponentModel.DataAnnotations;
using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;


namespace RW.PMS.Domain.Entities.Basics
{
    /// <summary>
    ///     工位工具/物料管控OPC点位类型信息表
    /// </summary>
    [Table(Name = "base_gjwlOpcPoint")]
    public class GjwlOpcPointEntity : FullEntity
    {
        /// <summary>
        ///     工作中心ID
        /// </summary>
        public int gwID { get; set; }

        /// <summary>
        ///     工作中心名称
        /// </summary>
        [MaxLength(150)]
        public string gwname { get; set; }

        /// <summary>
        ///     工具ID
        /// </summary>
        public int gjID { get; set; }

        /// <summary>
        ///     物料ID
        /// </summary>
        public int wlID { get; set; }

        /// <summary>
        ///     Opc点位类型ID
        /// </summary>
        public int opcTypeID { get; set; }

        /// <summary>
        ///     Opc点位类型编码
        /// </summary>
        [MaxLength(150)]
        public string opcTypeCode { get; set; }

        /// <summary>
        ///     设备名称
        /// </summary>
        [MaxLength(150)]
        public string opcDeviceName { get; set; }

        /// <summary>
        ///     Opc点位信息
        /// </summary>
        [MaxLength(150)]
        public string opcValue { get; set; }

        /// <summary>
        ///     备注
        /// </summary>
        [MaxLength(255)]
        public string remark { get; set; }
    }
}
