using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RW.PMS.Application.Contracts.Input.System
{
    public class DictionaryInput
    {
        public int? Id { get; set; }

        public int ParentId { get; set; }

        /// <summary>
        ///     字典文本
        /// </summary>
        public string DictionaryText { get; set; }

        /// <summary>
        ///     字典值
        /// </summary>
        public string DictionaryValue { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; set; }
    }
}