namespace RW.PMS.Application.Contracts.Output.System
{
    public class DictionaryOutput
    {
        public int Id { get; set; }

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

        /// <summary>
        /// 是否包含子项（默认一级都包含子项，二级不包含子项）
        /// </summary>
        public bool HaveChild { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        public int ParentId { get; set; }
        
    }
}