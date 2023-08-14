using System.Threading;
using System.Threading.Tasks;
using FreeSql;
using RW.PMS.Domain.Entities.Base;

namespace RW.PMS.Domain.Repositories
{
    /// <summary>
    ///     仓储基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>, new()
    {
        /// <summary>
        ///     根据主键批量删除
        /// </summary>
        /// <param name="keys">主键数组</param>
        /// <returns></returns>
        int Delete(TKey[] keys);

        /// <summary>
        ///     根据主键批量删除(异步)
        /// </summary>
        /// <param name="keys">主键数组</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(TKey[] keys, CancellationToken cancellationToken = default);
    }
}