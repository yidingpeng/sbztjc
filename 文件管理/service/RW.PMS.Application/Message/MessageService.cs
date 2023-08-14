using System;
using AutoMapper;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.Input.Message;
using RW.PMS.Application.Contracts.Message;
using RW.PMS.Application.Contracts.Output.Message;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.Message;
using RW.PMS.Domain.Entities.System;
using RW.PMS.Domain.Repositories;
using RW.PMS.Domain.Repositories.Message;

namespace RW.PMS.Application.Message
{
    public class MessageService : ApplicationService, IMessageService
    {
        private readonly IRepository<MessageContentEntity, int> _messageContentRepository;
        private readonly IMessageReceiverRepository _messageReceiverRepository;

        public MessageService(IDataValidatorProvider dataValidator, IMapper mapper,
            Lazy<ICurrentUser> currentUser, IMessageReceiverRepository messageReceiverRepository,
            IRepository<MessageContentEntity, int> messageContentRepository) : base(dataValidator, mapper,
            currentUser)
        {
            _messageReceiverRepository = messageReceiverRepository;
            _messageContentRepository = messageContentRepository;
        }

        public MessageContentView OpenAndRead(int id)
        {
            var entity = _messageReceiverRepository.Get(id);
            if (entity != null)
            {
                _messageReceiverRepository.Read(entity.Id);
                var view = _messageContentRepository.Select.From<MessageConfigEntity, DictionaryEntity>((m, c, d) => m
                        .LeftJoin(a => a.ConfigId == c.Id)
                        .LeftJoin(b => b.MessageLevel == d.Id))
                    .Where(t => t.t1.Id == entity.ContentId)
                    .ToOne(t => new MessageContentView());
                return view;
            }
            return null;
        }

        public PagedResult<MessageOutput> GetPagedList(MessageSearchInput input)
        {
            var list = _messageReceiverRepository.Select.From<MessageContentEntity>((r, c) => r
                    .InnerJoin(a => a.ContentId == c.Id))
                .Where(t => t.t1.UserId == CurrentUser.Value.Id && t.t1.RemindTime <= DateTime.Now)
                .Count(out var total).Page(input.PageNo, input.PageSize).ToList(t => new MessageOutput());
            return new PagedResult<MessageOutput>(list, total);
        }

        public PagedResult<MessageReceiverOutput> GetReceiverPagedList(MessageReceiverSearchInput input)
        {
            var list = _messageReceiverRepository.Select.From<UserEntity>((r, u) => r
                    .LeftJoin(a => a.UserId == u.Id))
                .WhereIf(input.ContentId.HasValue, t => t.t1.ContentId == input.ContentId)
                .Count(out var total).Page(input.PageNo, input.PageSize).ToList(t => new MessageReceiverOutput());
            return new PagedResult<MessageReceiverOutput>(list, total);
        }
    }
}