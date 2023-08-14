using AutoMapper;
using RW.PMS.Application.Contracts.Basics;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Basics;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.Extensions;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.Basics;
using RW.PMS.Domain.Entities.DeviceFile;
using RW.PMS.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Basics
{
    public class LogDetailsService : CrudApplicationService<LogdetailsEntity, int>, ILogDetailsService
    {
        public LogDetailsService(IDataValidatorProvider dataValidator,
                           IRepository<LogdetailsEntity, int> productionRepository,
                           IMapper mapper,
                           Lazy<ICurrentUser> currentUser) :
          base(dataValidator, productionRepository, mapper, currentUser)
        {
        }

        public int addlog(LogdetailsDto input)
        {
            var entity = Mapper.Map<LogdetailsEntity>(input);
            return ((int)Repository.Orm.Insert<LogdetailsEntity>(entity).ExecuteIdentity());
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PagedResult<LogdetailsDto> GetPagedList(LogdetailsSearchDto input)
        {
            var list = Repository.Select
                .WhereIf(input.LogLevel.NotNullOrWhiteSpace(), x => x.LogLevel.Contains(input.LogLevel))
                .WhereIf(input.RequestType.NotNullOrWhiteSpace(), x => x.RequestType.Contains(input.RequestType))
                .WhereIf(input.startTime.NotNullOrWhiteSpace(), x => x.LogDate >= Convert.ToDateTime(input.startTime))
                .WhereIf(input.endTime.NotNullOrWhiteSpace(), x => x.LogDate <= Convert.ToDateTime(input.endTime))
                .Count(out var total).Page(input.PageNo, input.PageSize).OrderByDescending(t => t.Id).ToList();
            return new PagedResult<LogdetailsDto>(Mapper.Map<List<LogdetailsDto>>(list), total);
        }

    }
}
