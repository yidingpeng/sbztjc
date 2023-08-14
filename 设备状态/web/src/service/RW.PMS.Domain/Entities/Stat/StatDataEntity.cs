using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.Stat
{
    /// <summary>
    /// 统计数据表（按天统计）
    /// </summary>
    [Table(Name = "stat_data")]
    public class StatDataEntity : IEDRoomJiaoZhunTimeEntityntity<int>
    {
        /// <summary>
        /// 发生的日期
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// 统计类别
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 统计关键字
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 统计值
        /// </summary>
        public int Value { get; set; }

    }
}
