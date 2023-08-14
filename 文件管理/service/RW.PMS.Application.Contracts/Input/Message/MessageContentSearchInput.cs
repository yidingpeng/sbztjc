using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Domain.ValueObject;

namespace RW.PMS.Application.Contracts.Input.Message
{
	/// <summary>
    ///     消息内容搜索实体
    /// </summary>
	public class MessageContentSearchInput : PagedQuery
	{
        public MessageType? MessageType { get; set; }
	}
}
