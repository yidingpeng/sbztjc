using AutoMapper;
using Microsoft.AspNetCore.Http;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Log;
using RW.PMS.Application.Contracts.System;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.Base;
using RW.PMS.Domain.Entities.System;
using RW.PMS.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.System
{
    public class LogService : CrudApplicationService<LogEntity, int>, ILogService
    {
        IDictionaryService dictSerivce;
        public LogService(
            IDataValidatorProvider dataValidator,
            IRepository<LogEntity, int> repository,
            IMapper mapper,
            Lazy<ICurrentUser> currentUser,
             IDictionaryService dictSerivce
            )
            : base(dataValidator, repository, mapper, currentUser)
        {
            this.dictSerivce = dictSerivce;
        }

        public bool AddErrorLog(string msg, string desc)
        {
            return AddLog(new LogInputDto { Message = msg, Type = LogTypeEnum.Error, Result = false, Desc = desc });
        }

        public bool AddLog(LogInputDto input)
        {
            LogOutputDto output = Mapper.Map<LogOutputDto>(input);
            output.Datetime = DateTime.Now;
            output.Account = CurrentUser.Value.RealName;
            HttpContextAccessor context = new HttpContextAccessor();
            output.Ip = context.HttpContext.Connection.RemoteIpAddress.ToString();
            var entity = base.Insert(output);
            return entity != null && entity.Id > 0;
        }

        public bool AddLoginLog(bool result, string msg, string desc)
        {
            return AddLog(new LogInputDto { Result = result, Message = msg, Type = LogTypeEnum.Login, Desc = desc });
        }

        public bool AddOperationLog(bool result, string msg, string desc)
        {
            return AddLog(new LogInputDto { Result = result, Type = LogTypeEnum.Operation, Message = msg, Desc = desc });
        }

        public PagedResult<LogOutputDto> GetPagedList(LogQueryInputDto input)
        {
            var lst = Repository
                .WhereIf(!string.IsNullOrEmpty(input.Account), x => x.Account == input.Account)
                .WhereIf(!string.IsNullOrEmpty(input.Type), x => x.Type == input.Type)
                .WhereIf(input.Start.HasValue, x => x.Datetime >= input.Start)
                .WhereIf(input.To.HasValue, x => x.Datetime <= input.To)
                .Count(out long total)
                .Page(input.PageNo, input.PageSize)
                .OrderByDescending(x => x.Datetime)
                .ToList()
                .Select(x =>
                {
                    var dto = Mapper.Map<LogOutputDto>(x);
                    dto.Type = dictSerivce.GetCacheValue("LogType", dto.Type);
                    return dto;
                })
                .ToList();
            return new PagedResult<LogOutputDto>(lst, total);
        }
    }
}
