using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    /// <summary>
    /// 监听配置
    /// </summary>
    public class MonitorConfig
    {
        public int ID { get; set; }
        /// <summary>
        /// 监听路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName { get; set; }
        /// <summary>
        /// 房间号
        /// </summary>
        public string RoomNumber { get; set; }

        public string IP { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
