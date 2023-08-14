using FreeSql;
using RW.PMS.Domain.Entities.Message;

namespace RW.PMS.Domain.Repositories.Message
{
    /// <summary>
    ///     消息接收者仓储基类
    /// </summary>
    public interface IMessageReceiverRepository : IBaseRepository<MessageReceiverEntity, int>
    {
        /// <summary>
        ///     设为已读
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int Read(int id);
    }
}