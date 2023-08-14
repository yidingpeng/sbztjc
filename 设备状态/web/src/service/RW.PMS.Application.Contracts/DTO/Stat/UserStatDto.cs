using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Stat
{
    public class UserStatDto
    {
        /// <summary>
        /// 用户总数
        /// </summary>
        public int Total { get; set; }
        /// <summary>
        /// 日活跃（登录就算）
        /// </summary>
        public int ActiveToday { get; set; }
        /// <summary>
        /// 周活跃（登录就算）
        /// </summary>
        public int ActiveWeek { get; set; }
    }
}
