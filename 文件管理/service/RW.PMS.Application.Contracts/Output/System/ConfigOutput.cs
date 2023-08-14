using System;

namespace RW.PMS.Application.Contracts.Output.System
{
    public class ConfigOutput
    {
        /// <summary>
        ///     主键
        /// </summary>
        public int Id { get; set; }

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

        /// <summary>
        ///     创建时间
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        ///     创建人
        /// </summary>
        public string CreatedUser { get; set; }
    }
}
