using RW.PMS.Application.Contracts.DTO;

namespace RW.PMS.Application.Contracts.Input.System
{
    /// <summary>
    ///     字典列表搜索输入
    /// </summary>
    public class DictionarySearchInput : PagedQuery
    {
        public int? ParentId { get; set; }

        public string DictionaryText { get; set; }

        public string DictionaryValue { get; set; }
    }
}