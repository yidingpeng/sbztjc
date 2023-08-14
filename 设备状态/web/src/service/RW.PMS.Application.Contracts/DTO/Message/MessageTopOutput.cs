using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Message
{
    public class MessageTopOutput
    {
        public int Id { get; set; }
        /// <summary>
        /// 通知标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 通知类型，比如 inform,...
        /// </summary>
        public string Type { get; set; }
        public DateTime Time { get; set; }

    }
}
