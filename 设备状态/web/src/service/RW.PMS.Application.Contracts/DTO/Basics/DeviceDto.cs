using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Basics
{
    /// <summary>
    ///     设备基础信息表
    /// </summary>
    public class DeviceDto
    {
        public int Id { get; set; }

        ///     设备名称
        /// </summary>
        public string devName { get; set; }
        /// <summary>
        ///     设备编码
        /// </summary>
        public string devNo { get; set; }
        /// <summary>
        ///     设备IP
        /// </summary>
        public string devIP { get; set; }
        /// <summary>
        ///     创建时间
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        ///     状态
        /// </summary>
        public int devStatus { get; set; }
        /// <summary>
        ///     IC卡号
        /// </summary>
        public string devCardno { get; set; }
        /// <summary>
        ///     备注
        /// </summary>
        public string remark { get; set; }
    }
}
