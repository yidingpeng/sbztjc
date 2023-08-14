using FreeSql;

namespace RW.PMS.Infrastructure.Repositories;

public class UowBaseRepository<TEntity> : BaseRepository<TEntity> where TEntity : class
{
    public UowBaseRepository(UnitOfWorkManager uowManger) : base(uowManger.Orm, null)
    {
        uowManger.Binding(this);
    }
}