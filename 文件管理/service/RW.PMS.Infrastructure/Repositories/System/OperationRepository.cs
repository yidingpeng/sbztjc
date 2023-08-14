using FreeSql;
using RW.PMS.CrossCutting.Extensions;
using RW.PMS.Domain.Entities.System;
using RW.PMS.Domain.Repositories.System;
using System.Collections.Generic;
using System.Linq;

namespace RW.PMS.Infrastructure.Repositories.System;

public class OperationRepository : BaseRepository<OperationEntity>, IOperationRepository
{
    public OperationRepository(UnitOfWorkManager uowManger) : base(uowManger.Orm, null)
    {
    }

    public List<OperationEntity> GetList()
    {
        return Select.ToList();
    }

    public List<OperationEntity> GetList(int moduleId)
    {
        return Select.Where(t => t.ModuleId == moduleId).ToList();
    }

    public bool Edit(List<OperationEntity> listData)
    {
        if (listData != null)
        {
            Delete(t => t.ModuleId == listData[0].ModuleId);
            foreach (var item in listData)
            {
                Insert(item);
            }
            return true;
        }
        else
            return false;
    }

    public bool EditOne(OperationEntity entity)
    {
        return Update(entity) > 0;
    }

    public bool IsExist(OperationEntity input)
    {
        var exist = Select.Where(t => t.OperationCode == input.OperationCode).ToOne();
        if (exist == null) return false;
        return true;
    }

    public bool Add(OperationEntity input)
    {
        if (IsExist(input))
        {
            return false;
        }
        Insert(input);
        return true;
    }
}