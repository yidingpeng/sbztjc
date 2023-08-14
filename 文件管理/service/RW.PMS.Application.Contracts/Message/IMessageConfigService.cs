using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.Input.Message;
using RW.PMS.Application.Contracts.Output.Message;
using RW.PMS.Domain.Entities.Message;

namespace RW.PMS.Application.Contracts.Message
{
    /// <summary>
    ///     消息配置应用服务
    /// </summary>
	public interface IMessageConfigService : ICrudApplicationService<MessageConfigEntity, int>
    {
        MessageConfigView GetView(int id);

        /// <summary>
        ///     获取消息配置分页数据
        /// </summary>
        /// <param name="input"><see cref="MessageConfigSearchInput" />搜索输入项</param>
        /// <returns></returns>
        PagedResult<MessageConfigOutput> GetPagedList(MessageConfigSearchInput input);

        bool MessageCodeExist(MessageConfigInput input);

    }
}
