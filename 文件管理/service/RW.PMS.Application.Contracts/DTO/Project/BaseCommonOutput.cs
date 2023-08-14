using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Project
{
    public class BaseCommonOutput
    {
        /// <summary>
        /// 下拉框Value
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 下拉框Label
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// val值
        /// </summary>
        public int value { get; set; }
        /// <summary>
        /// 文本显示值
        /// </summary>
        public string label { get; set; }


    }
}
