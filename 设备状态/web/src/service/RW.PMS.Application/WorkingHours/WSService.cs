using AutoMapper;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.WorkingHours;
using RW.PMS.Application.Contracts.WorkingHours;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.Extensions;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.System;
using RW.PMS.Domain.Entities.WorkingHours;
using RW.PMS.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RW.PMS.Application.WorkingHours
{
    public class WSService : CrudApplicationService<WHDetailEntity, int>, IWSService
    {
        public WSService(IDataValidatorProvider dataValidator,
                             IRepository<WHDetailEntity, int> configRepository,
                             IMapper mapper,
                             Lazy<ICurrentUser> currentUser) :
            base(dataValidator, configRepository, mapper, currentUser)
        {
        }

        /// <summary>
        /// 工时信息分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PagedResult<WHDetailDto> GetPageList(WHDetailSearchDto input)
        {
            var list = Repository.Select.From<UserEntity>((a, b) => a
                  .LeftJoin(a => a.UserId == b.Id))
                  .WhereIf(input.WHStartDate == null && input.WHStartDate != null, (a, b) => a.WHDate <= input.WHStartDate)
                  .WhereIf(input.WHStartDate != null && input.WHStartDate == null, (a, b) => a.WHDate >= input.WHStartDate)
                  .WhereIf(input.WHStartDate != null && input.WHStartDate != null, (a, b) => a.WHDate >= input.WHStartDate && a.WHDate <= input.WHEndDate)
                  .WhereIf(input.ProjectCode.NotNullOrEmpty(), (a, b) => a.ProjectCode.Contains(input.ProjectCode) || a.ProjectName.Contains(input.ProjectCode))
                  .Count(out var total)
                  .Page(input.PageNo, input.PageSize)
                  .ToList((a, b) => new
                  {
                      All = a,
                      user = b,
                  })
                  .Select(t =>
                  {
                      var detail = Mapper.Map<WHDetailDto>(t.All);
                      detail.UserName = t.user == null ? "" : t.user.Account;
                      return detail;
                  }).ToList();
            return new PagedResult<WHDetailDto>(list, total);
        }
    }
}
