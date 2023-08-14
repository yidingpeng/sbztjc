using AutoMapper;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Message;
using RW.PMS.Application.Contracts.DTO.Notice;
using RW.PMS.Application.Contracts.Message;
using RW.PMS.Application.Contracts.System;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.Exceptions;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.System;
using RW.PMS.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.System
{
    public class NoticeService : CrudApplicationService<SystemNoticeEntity, int>, INoticeService
    {
        IMessageContentService _messageSerivce;
        ILogService log;
        public NoticeService(
            IDataValidatorProvider dataValidator,
            IMessageContentService messageSerivce,
            IRepository<SystemNoticeEntity, int> repository,
            IMapper mapper,
            ILogService log,
            Lazy<ICurrentUser> currentUser)
            : base(dataValidator, repository, mapper, currentUser)
        {
            this.log = log;
            this._messageSerivce = messageSerivce;
        }

        public PagedResult<NoticeOutputDto> GetPagedList(NoticeQueryInputDto input)
        {
            var lst = Repository
                    .Select
                    .WhereIf(!string.IsNullOrEmpty(input.Key), x => x.Title.Contains(input.Key))
                    .WhereIf(!string.IsNullOrEmpty(input.Type), x => x.Type == input.Type)
                    .WhereIf(input.StartTime.HasValue, x => x.CreatedAt >= input.StartTime.Value)
                    .WhereIf(input.EndTime.HasValue, x => x.CreatedAt <= input.EndTime.Value)
                    .OrderByDescending(x => x.Id)
                    .Page(input.PageNo, input.PageSize)
                    .Count(out long total)
                    .ToList()
                    .Select(x => Mapper.Map<NoticeOutputDto>(x)).ToList();
            return new PagedResult<NoticeOutputDto>(lst, total);
        }

        public override SystemNoticeEntity Insert<TInput>(TInput input)
        {
            var entity = base.Insert(input);
            log.AddOperationLog(true, "添加公告成功", $"添加了公告[{(input as NoticeEditDto).Title}]");
            return entity;
        }

        public override int Update<TInput>(int key, TInput input)
        {
            var id = base.Update(key, input);
            log.AddOperationLog(true, "更新公告成功", $"修改了公告[{(input as NoticeEditDto).Title}]");
            return id;
        }

        public override int Delete(int key)
        {
            var count = base.Delete(key);
            log.AddOperationLog(true, "删除公告成功", $"删除了公告[{key}]");
            return count;
        }

        public bool Publish(int id)
        {
            var item = Get(id);
            if (item == null) throw new LogicException(ExceptionCode.ParamError, $"未找到指定的公告[id={id}]");
            int[] userIds = null;
            if (!string.IsNullOrEmpty(item.Users))
                userIds = item.Users.Split(',').Select(x => Convert.ToInt32(x)).ToArray();

            var users = Repository.Orm.Select<UserEntity>().WhereIf(!string.IsNullOrEmpty(item.Users), x => userIds.Contains(x.Id)).ToList(x => x.Id);

            item.Status = 1;
            Repository.Update(item);

            CrossCutting.EventBus.EventBus eve = new CrossCutting.EventBus.EventBus(null);

            return _messageSerivce.SendMessage(new MessageSendInputDto
            {
                Title = item.Title,
                Content = item.Content,
                UserIds = users.ToArray(),
                Type = "inform",
                DataId = id,
            });

        }

        public bool ReadInform(int id)
        {
            var item = Get(id);
            item.Read();
            return Repository.Update(item) > 0;
        }
    }
}
