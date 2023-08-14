using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RW.PMS.Application.Contracts.Input.System
{
    public class ConfigInput
    {
        public int? Id { get; set; }

        /// <summary>
        ///     参数编码
        /// </summary>
        public string ConfigCode { get; set; }

        /// <summary>
        ///     参数值
        /// </summary>
        public string ConfigValue { get; set; }

        /// <summary>
        ///     说明
        /// </summary>
        public string Description { get; set; }
    }
}