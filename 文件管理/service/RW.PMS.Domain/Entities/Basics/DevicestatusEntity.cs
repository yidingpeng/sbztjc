using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.Basics
{
    /// <summary>
    ///     设备文件路径信息表
    /// </summary>
    [Table(Name = "MonitorConfig")]
    public class DevicestatusEntity : FullEntity
    {
        /// <summary>
        /// 监听路径
        /// </summary>
        [MaxLength(200)]
        public string Path { get; set; }
        ///// <summary>
        ///// 设备名称
        ///// </summary>
        //[MaxLength(200)]
        //public string DeviceName { get; set; }
        ///// <summary>
        ///// 房间号
        ///// </summary>
        //[MaxLength(50)]
        //public string RoomNumber { get; set; }
        ///// <summary>
        ///// 设备IP
        ///// </summary>
        //[MaxLength(50)]
        //public string IP { get; set; }
    }
}
