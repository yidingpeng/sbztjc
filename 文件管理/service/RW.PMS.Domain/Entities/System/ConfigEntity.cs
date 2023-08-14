using System.ComponentModel.DataAnnotations;
using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;

namespace RW.PMS.Domain.Entities.System
{
    /// <summary>
    ///     系统参数
    /// </summary>
    [Table(Name = "sys_conf", OldName = "Config")]
    public class ConfigEntity : FullEntity
    {
        /// <summary>
        /// 配置分类
        /// </summary>
        public string ConfigType { get; set; }
        /// <summary>
        ///     参数编码
        /// </summary>
        [MaxLength(50)]
        public string ConfigCode { get; set; }

        /// <summary>
        ///     参数值
        /// </summary>
        [MaxLength(100)]
        public string ConfigValue { get; set; }

        /// <summary>
        ///     说明
        /// </summary>
        [MaxLength(500)]
        public string Description { get; set; }
    }
}