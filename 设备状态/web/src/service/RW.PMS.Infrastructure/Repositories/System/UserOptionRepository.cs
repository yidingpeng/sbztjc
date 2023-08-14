using FreeSql;
using RW.PMS.Domain.Entities.System;
using RW.PMS.Domain.Repositories.System;
using System.Collections.Generic;

namespace RW.PMS.Infrastructure.Repositories.System
{
    public class UserOptionRepository : UowBaseRepository<UserOperationEntity>, IUserOptionRepository
    {
        public UserOptionRepository(UnitOfWorkManager uowManger) : base(uowManger)
        {
        }
    }
}
