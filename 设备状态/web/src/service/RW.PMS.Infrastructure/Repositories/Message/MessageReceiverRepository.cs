using System;
using FreeSql;
using RW.PMS.Domain.Entities.Message;
using RW.PMS.Domain.Repositories.Message;

namespace RW.PMS.Infrastructure.Repositories.Message
{
    public class MessageReceiverRepository : BaseRepository<MessageReceiverEntity, int>, IMessageReceiverRepository
    {
        public MessageReceiverRepository(UnitOfWorkManager uowManger) : base(uowManger.Orm, null)
        {
        }

        public int Read(int id)
        {
            return Orm.Update<MessageReceiverEntity>()
                .Set(t => t.IsRead, true)
                .Set(t => t.ReadTime, DateTime.Now)
                .Where(t => t.Id == id && !t.IsRead)
                .ExecuteAffrows();
        }
    }
}