using System;
using System.Collections.Generic;
using AutoMapper;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.Message;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.Message;
using RW.PMS.Domain.Entities.System;
using RW.PMS.Domain.Repositories;
using System.Linq;
using RW.PMS.Application.Contracts.DTO.Message;
using RW.PMS.Application.Contracts.System;
using RW.PMS.CrossCutting.Exceptions;

namespace RW.PMS.Application.Message
{
    public class MessageContentService : CrudApplicationService<MessageContentEntity, int>, IMessageContentService
    {
        IRepository<SystemNoticeEntity, int> noticeRepositiry;

        public MessageContentService(IDataValidatorProvider dataValidator,
            IRepository<MessageContentEntity, int> repository,
            IRepository<SystemNoticeEntity, int> noticeRepositiry,
            IMapper mapper, Lazy<ICurrentUser> currentUser) : base(
            dataValidator, repository, mapper, currentUser)
        {
            this.noticeRepositiry = noticeRepositiry;
        }

        public int CleanMessageByDate(TimeSpan old)
        {
            return Repository.Delete(x => x.CreatedAt > DateTime.Now.Add(old));
        }

        public bool ClearMessage()
        {
            return Repository.Delete(x => true) > 0;
        }

        public bool DeleteMessage(int id)
        {
            return Repository.Delete(x => x.Id == id) > 0;
        }

        public List<MessageContentOutputDto> GetNewMessage(int top)
        {
            var lst = Repository.Select.Limit(top).ToList()
                .Select(x => Mapper.Map<MessageContentOutputDto>(x)).ToList();
            return lst;
        }

        public PagedResult<MessageContentOutputDto> GetPagedList(MessageSearchInputDto input)
        {
            var uid = CurrentUser.Value.Id;
            var list = Repository.Select.From<DictionaryEntity, MessageReceiverEntity>(
                    (m, d, r) => m
                        .LeftJoin(b => b.MessageLevel == d.Id)
                        .InnerJoin(c => c.Id == r.ContentId && r.UserId == uid)
                     )
                .WhereIf(!string.IsNullOrEmpty(input.Title), t => t.t1.Title.Contains(input.Title))
                .WhereIf(input.Read != null, t => t.t3.IsRead == input.Read.Value)
                .WhereIf(input.Start != null, t => t.t1.CreatedAt > input.Start)
                .WhereIf(input.End != null, t => t.t1.CreatedAt < input.End)
                .WhereIf(!string.IsNullOrEmpty(input.MessageType), t => t.t1.Type == input.MessageType)
                .Count(out var total)
                .OrderByDescending(x => x.t1.Id)
                .Page(input.PageNo, input.PageSize)
                .ToList(a => new { a.t1, a.t2, a.t3 })
                .Select(x => Mapper.Map<MessageContentOutputDto>(x.t1).SetRead(x.t3))
                .ToList();

            return new PagedResult<MessageContentOutputDto>(list, total);
        }

        public List<MessageTopOutput> GetUnReadMessage(int count)
        {
            var uid = CurrentUser.Value.Id;
            var lst = Repository.Select
                 .From<MessageReceiverEntity>((m, r) => m.InnerJoin(a => a.Id == r.ContentId && r.UserId == uid))
                 .Where((m, r) => !r.IsRead)
                 .Limit(count).ToList((m, r) => new { m, r })
                 .OrderByDescending(x => x.m.Id)
                 .Select(x => Mapper.Map<MessageTopOutput>(x.m)).ToList();
            return lst;
        }

        public MessageDetailOutputDto ReadMessage(int id)
        {
            var uid = CurrentUser.Value.Id;
            var repo = Repository.Orm.Select<MessageReceiverEntity>();
            var item = repo.Where(x => x.ContentId == id && x.UserId == uid).First();
            if (item == null)
                throw new LogicException(ExceptionCode.Failed, "无法查看此消息");
            item.IsRead = true;
            item.ReadTime = DateTime.Now;
            var m = Repository.Orm.InsertOrUpdate<MessageReceiverEntity>();
            m.SetSource(item).ExecuteAffrows();


            var data = Repository.Get(item.ContentId);
            if (data.Type == "inform" && data.DataId.HasValue)
            {
                var notice = noticeRepositiry.Get(data.DataId.Value);
                notice.Read();
                noticeRepositiry.Update(notice);
            }

            return Mapper.Map<MessageDetailOutputDto>(data);
        }

        public bool SendMessage(MessageSendInputDto input)
        {
            var content = base.Insert(input);
            if (content == null) return false;
            var repo = Repository.Orm.Insert<MessageReceiverEntity>();
            repo.AppendData(input.UserIds.Select(x => new MessageReceiverEntity { UserId = x, ContentId = content.Id, RemindTime = DateTime.Now, }));
            repo.ExecuteAffrows();
            return true;

        }
    }
}