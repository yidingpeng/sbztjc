using System.Collections.Generic;
using System.Linq;
using FreeSql;
using Microsoft.Extensions.Logging;
using RW.PMS.Domain.Entities.System;
using RW.PMS.Domain.Repositories.System;

namespace RW.PMS.Infrastructure.Repositories.System;

public class RoleOperationRepository : UowBaseRepository<RoleOperationEntity>, IRoleOperationRepository
{
    public RoleOperationRepository(UnitOfWorkManager uowManger) : base(uowManger)
    {
    }

    public void Insert(int roleId, int[] operation)
    {
        var list = operation.Select(i => new RoleOperationEntity {RoleId = roleId, OperationId = i}).ToList();
        base.Insert(list);
    }

    public List<string> GetOperationCodeForRole(int[] roleId)
    {
        var list = Select.From<OperationEntity>((ro, o) => ro
                .InnerJoin(t => t.OperationId == o.Id)).Where(t => roleId.Contains(t.t1.RoleId))
            .ToList(t => t.t2.OperationCode).Distinct().ToList();
        return list;
    }
}