using RW.PMS.Application.Contracts.DTO;

namespace RW.PMS.Application.Contracts.Input.Message
{
    public class MessageReceiverSearchInput : PagedQuery
    {
        public int? ContentId { get; set; }
    }
}