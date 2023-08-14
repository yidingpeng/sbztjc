using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.Input.Message;
using RW.PMS.Application.Contracts.Message;
using RW.PMS.Application.Contracts.Output.Message;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.Extensions;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.Message;
using RW.PMS.Domain.Entities.System;
using RW.PMS.Domain.Repositories;
using RW.PMS.Domain.ValueObject;

namespace RW.PMS.Application.Message
{
    public class MessageConfigService : CrudApplicationService<MessageConfigEntity, int>, IMessageConfigService
    {
        private readonly IRepository<OrganizationEntity, int> _organizationRepository;
        private readonly IRepository<RoleEntity, int> _roleRepository;
        private readonly IRepository<UserEntity, int> _userRepository;

        public MessageConfigService(IDataValidatorProvider dataValidator,
            IRepository<MessageConfigEntity, int> repository, IMapper mapper, Lazy<ICurrentUser> currentUser,
            IRepository<OrganizationEntity, int> organizationRepository, IRepository<RoleEntity, int> roleRepository,
            IRepository<UserEntity, int> userRepository) : base(dataValidator, repository, mapper, currentUser)
        {
            _organizationRepository = organizationRepository;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
        }

        public MessageConfigView GetView(int id)
        {
            var entity = Get(id);
            var view = Mapper.Map<MessageConfigView>(entity);
            if (entity.Target.NotNullOrWhiteSpace())
            {
                var targetId = entity.Target.Split(',').Select(t => t.To<int>()).ToArray();
                if (targetId.Length > 0)
                    view.Target = new List<dynamic>();
                switch (entity.MessageTarget)
                {
                    case MessageTarget.Organization:
                        view.Target = _organizationRepository.Select.Where(t => targetId.Contains(t.Id))
                            .ToList(t => new {t.Id, t.OrganizationName}).Select(t => new
                                {t.Id, t.OrganizationName});
                        break;
                    case MessageTarget.Role:
                        view.Target = _roleRepository.Select.Where(t => targetId.Contains(t.Id))
                            .ToList(t => new {t.Id, t.RoleName}).Select(t => new
                                {t.Id, t.RoleName});
                        break;
                    case MessageTarget.User:
                        view.Target = _userRepository.Select.Where(t => targetId.Contains(t.Id))
                            .ToList(t => new {t.Id, t.RealName}).Select(t => new
                                {t.Id, t.RealName});
                        break;
                }
            }

            return view;
        }

        public PagedResult<MessageConfigOutput> GetPagedList(MessageConfigSearchInput input)
        {
            var list = Repository.Select.From<DictionaryEntity>((c, d) => c
                    .LeftJoin(a => a.MessageLevel == d.Id))
                .WhereIf(input.MessageCode.NotNullOrWhiteSpace(), t => t.t1.MessageCode.Contains(input.MessageCode))
                .Count(out var total).Page(input.PageNo, input.PageSize).ToList((c, d) => new
                {
                    Entity = c,
                    MessageLevel = d.DictionaryText
                }).Select(t =>
                {
                    var entity = Mapper.Map<MessageConfigOutput>(t.Entity);
                    entity.MessageLevel = t.MessageLevel;
                    return entity;
                });
            return new PagedResult<MessageConfigOutput>(Mapper.Map<List<MessageConfigOutput>>(list), total);
        }

        public bool MessageCodeExist(MessageConfigInput input)
        {
            var exist = Repository.Where(t => t.MessageCode == input.MessageCode).ToOne();
            if (exist == null) return false;
            if (input.Id.HasValue) return input.Id.Value != exist.Id;
            return true;
        }
    }
}