using RW.PMS.Application.Contracts.DTO;

namespace RW.PMS.Application.Contracts.Input.Message
{
	/// <summary>
    ///     消息配置搜索实体
    /// </summary>
	public class MessageConfigSearchInput : PagedQuery
	{
        public string MessageCode { get; set; }
    }
}
