using FreeSql;
using RW.PMS.Domain.Entities.System;
using RW.PMS.Domain.Repositories.System;
using System.Collections.Generic;

namespace RW.PMS.Infrastructure.Repositories.System
{
    public class UserRoleRepository : BaseRepository<UserRoleEntity>, IUserRoleRepository
    {
        public UserRoleRepository(UnitOfWorkManager uowManger) : base(uowManger.Orm, null)
        {
        }

        public List<UserRoleEntity> GetList()
        {
            return Select.ToList();
        }
    }
}
