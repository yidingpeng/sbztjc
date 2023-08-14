using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.Input.Message;
using RW.PMS.Application.Contracts.Output.Message;

namespace RW.PMS.Application.Contracts.Message
{
    public interface IMessageService : IApplicationService
    {
        /// <summary>
        ///     返回实体并设置为已读
        /// </summary>
        /// <returns></returns>
        MessageContentView OpenAndRead(int id);

        PagedResult<MessageOutput> GetPagedList(MessageSearchInput input);

        PagedResult<MessageReceiverOutput> GetReceiverPagedList(MessageReceiverSearchInput input);

    }
}