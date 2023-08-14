using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.UserData;
using RW.PMS.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.Service
{
    public interface IUserQuickService : ICrudApplicationService<UserQuickEntity, int>
    {
        PagedResult<QuickDto> GetQuickList(int top);
    }
}
