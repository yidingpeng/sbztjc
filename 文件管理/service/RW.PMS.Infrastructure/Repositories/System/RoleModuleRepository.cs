using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeSql;
using Microsoft.Extensions.Logging;
using RW.PMS.Domain.Entities.System;
using RW.PMS.Domain.Repositories.System;

namespace RW.PMS.Infrastructure.Repositories.System
{
    public class RoleModuleRepository : UowBaseRepository<RoleModuleEntity>, IRoleModuleRepository
    {
        public RoleModuleRepository(UnitOfWorkManager uowManger) : base(uowManger)
        {
        }

        public void Insert(int roleId, int[] module)
        {
            var list = module.Select(i => new RoleModuleEntity { RoleId = roleId, ModuleId = i }).ToList();
            base.Insert(list);
        }
    }
}
