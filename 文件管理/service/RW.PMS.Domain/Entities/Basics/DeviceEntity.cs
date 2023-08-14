using System;
using System.ComponentModel.DataAnnotations;
using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;

namespace RW.PMS.Domain.Entities.Basics
{
    /// <summary>
    ///     设备基础信息表
    /// </summary>
    [Table(Name = "base_Device")]
    public class DeviceEntity : FullEntity
    {
        ///     设备名称
        /// </summary>
        [MaxLength(150)]
        public string devName { get; set; }
        /// <summary>
        ///     设备编码
        /// </summary>
        [MaxLength(150)]
        public string devNo { get; set; }
        /// <summary>
        ///     设备IP
        /// </summary>
        [MaxLength(50)]
        public string devIP { get; set; }
        /// <summary>
        ///     创建时间
        /// </summary>
        [MaxLength(150)]
        public DateTime createDate { get; set; }
        /// <summary>
        ///     状态
        /// </summary>
        public int devStatus { get; set; }
        /// <summary>
        ///     IC卡号
        /// </summary>
        [MaxLength(50)]
        public string devCardno { get; set; }
        /// <summary>
        ///     备注
        /// </summary>
        [MaxLength(255)]
        public string remark { get; set; }
    }
}
