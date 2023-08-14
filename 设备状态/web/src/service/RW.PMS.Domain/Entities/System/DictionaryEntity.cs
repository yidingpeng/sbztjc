using System.ComponentModel.DataAnnotations;
using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;

namespace RW.PMS.Domain.Entities.System
{
    /// <summary>
    ///     数据字典实体
    /// </summary>
    [Table(Name = "sys_dict", OldName = "Dictionary")]
    public class DictionaryEntity : FullEntity, ITree<int>
    {
        /// <summary>
        ///     父级Id
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        ///     字典文本
        /// </summary>
        [MaxLength(50)]
        public string DictionaryText { get; set; }

        /// <summary>
        ///     字典值
        /// </summary>
        [MaxLength(100)]
        public string DictionaryValue { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        [MaxLength(255)]
        public string Description { get; set; }
    }
}