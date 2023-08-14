using AutoMapper;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.UserData;
using RW.PMS.Application.Contracts.Service;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.User;
using RW.PMS.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.UserData
{
    public class UserQuickService : CrudApplicationService<UserQuickEntity, int>, IUserQuickService
    {
        public UserQuickService(IDataValidatorProvider dataValidator, IRepository<UserQuickEntity, int> repository, IMapper mapper, Lazy<ICurrentUser> currentUser) : base(dataValidator, repository, mapper, currentUser)
        {
        }

        public PagedResult<QuickDto> GetQuickList(int top)
        {
            var uid = CurrentUser.Value.Id;
            var lst = Repository.Select.Where(x=>x.CreatedBy == uid).Limit(top).OrderBy(x => x.OrderIndex).ToList().Select(x => Mapper.Map<QuickDto>(x)).ToList();
            return new PagedResult<QuickDto>(lst, lst.Count);
        }
    }
}
