using FreeSql;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.Base;
using RW.PMS.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace RW.PMS.Infrastructure.Repositories
{
    /// <summary>
    ///     仓储基类
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TKey">主键类型</typeparam>
    public class Repository<TEntity, TKey> : DefaultRepository<TEntity, TKey>, IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>, new()
    {
        /// <summary>
        ///     当前用户
        /// </summary>
        public readonly ICurrentUser CurrentUser;

        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="uowManger"></param>
        /// <param name="currentUser"></param>
        public Repository(UnitOfWorkManager uowManger, ICurrentUser currentUser) : base(uowManger.Orm, uowManger)
        {
            CurrentUser = currentUser;
        }

        public override TEntity Insert(TEntity entity)
        {
            BeforeInsert(entity);
            return base.Insert(entity);
        }

        public override async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            BeforeInsert(entity);
            return await base.InsertAsync(entity, cancellationToken);
        }

        public override List<TEntity> Insert(IEnumerable<TEntity> entities)
        {
            var enumerable = entities as TEntity[] ?? entities.ToArray();
            foreach (var entity in enumerable) BeforeInsert(entity);
            return base.Insert(enumerable);
        }

        public override async Task<List<TEntity>> InsertAsync(IEnumerable<TEntity> entities,
            CancellationToken cancellationToken = default)
        {
            var enumerable = entities as TEntity[] ?? entities.ToArray();
            foreach (var entity in enumerable) BeforeInsert(entity);
            return await base.InsertAsync(enumerable, cancellationToken);
        }

        public override int Update(TEntity entity)
        {
            BeforeUpdate(entity);
            return base.Update(entity);
        }

        public override async Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            BeforeUpdate(entity);
            return await base.UpdateAsync(entity, cancellationToken);
        }

        public override int Update(IEnumerable<TEntity> entities)
        {
            var enumerable = entities as TEntity[] ?? entities.ToArray();
            foreach (var entity in enumerable) BeforeUpdate(entity);
            return base.Update(enumerable);
        }

        public override async Task<int> UpdateAsync(IEnumerable<TEntity> entities,
            CancellationToken cancellationToken = default)
        {
            var enumerable = entities as TEntity[] ?? entities.ToArray();
            foreach (var entity in enumerable) BeforeUpdate(entity);
            return await base.UpdateAsync(enumerable, cancellationToken);
        }

        public override int Delete(TKey id)
        {
            var entity = new TEntity {Id = id};
            if (entity is ISoftDelete e)
            {
                Attach(entity);
                e.IsDeleted = true;
                return Update(entity);
            }

            return base.Delete(id);
        }

        public override async Task<int> DeleteAsync(TKey id, CancellationToken cancellationToken = default)
        {
            var entity = new TEntity {Id = id};
            if (entity is ISoftDelete e)
            {
                Attach(entity);
                e.IsDeleted = true;
                return await UpdateAsync(entity, cancellationToken);
            }

            return await base.DeleteAsync(id, cancellationToken);
        }

        public override int Delete(TEntity entity)
        {
            if (entity is ISoftDelete e)
            {
                Attach(entity);
                e.IsDeleted = true;
                return Update(entity);
            }

            return base.Delete(entity);
        }

        public override async Task<int> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity is ISoftDelete e)
            {
                Attach(entity);
                e.IsDeleted = true;
                return await UpdateAsync(entity, cancellationToken);
            }

            return await base.DeleteAsync(entity, cancellationToken);
        }

        public override int Delete(IEnumerable<TEntity> entities)
        {
            var enumerable = entities as TEntity[] ?? entities.ToArray();
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
                if (enumerable.Any())
                {
                    Attach(enumerable);
                    foreach (var entity in enumerable) ((ISoftDelete) entity).IsDeleted = true;
                    return Update(enumerable);
                }

            return base.Delete(enumerable);
        }

        public override async Task<int> DeleteAsync(IEnumerable<TEntity> entities,
            CancellationToken cancellationToken = default)
        {
            var enumerable = entities as TEntity[] ?? entities.ToArray();
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
                if (enumerable.Any())
                {
                    Attach(enumerable);
                    foreach (var entity in enumerable) ((ISoftDelete) entity).IsDeleted = true;
                    return await UpdateAsync(enumerable, cancellationToken);
                }

            return await base.DeleteAsync(enumerable, cancellationToken);
        }

        public override int Delete(Expression<Func<TEntity, bool>> predicate)
        {
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
            {
                var update = UpdateDiy.SetDto(new {IsDeleted = true});
                if (typeof(IAuditable).IsAssignableFrom(typeof(TEntity)))
                    update.SetDto(new {LastModifiedBy = CurrentUser?.Id ?? 0, LastModifiedAt = DateTime.Now});
                return update.Where(predicate).ExecuteAffrows();
            }

            return base.Delete(predicate);
        }

        public override async Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
            {
                var update = UpdateDiy.SetDto(new {IsDeleted = true});
                if (typeof(IAuditable).IsAssignableFrom(typeof(TEntity)))
                    update.SetDto(new {LastModifiedBy = CurrentUser?.Id ?? 0, LastModifiedAt = DateTime.Now});
                return await update.Where(predicate).ExecuteAffrowsAsync(cancellationToken);
            }

            return await base.DeleteAsync(predicate, cancellationToken);
        }

        public int Delete(TKey[] keys)
        {
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
            {
                var list = new List<TEntity>();
                foreach (var key in keys)
                {
                    var item = new TEntity {Id = key};
                    Attach(item);
                    ((ISoftDelete) item).IsDeleted = true;
                    list.Add(item);
                }
                return Update(list);
            }
            return Orm.Delete<TEntity>(keys).ExecuteAffrows();
        }

        public async Task<int> DeleteAsync(TKey[] keys, CancellationToken cancellationToken = default)
        {
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
            {
                var list = new List<TEntity>();
                foreach (var key in keys)
                {
                    var item = new TEntity { Id = key };
                    Attach(item);
                    ((ISoftDelete)item).IsDeleted = true;
                    list.Add(item);
                }
                return await UpdateAsync(list, cancellationToken);
            }
            return await Orm.Delete<TEntity>(keys).ExecuteAffrowsAsync(cancellationToken);
        }

        /// <summary>
        ///     插入前操作
        /// </summary>
        /// <param name="entity"></param>
        private void BeforeInsert(TEntity entity)
        {
            if (entity is not IAuditable e) return;
            e.CreatedAt = DateTime.Now;
            e.LastModifiedAt = DateTime.Now;

            e.CreatedBy = CurrentUser?.Id ?? 0;
            e.LastModifiedBy = CurrentUser?.Id ?? 0;
          
        }

        /// <summary>
        ///     更新前操作
        /// </summary>
        /// <param name="entity"></param>
        private void BeforeUpdate(TEntity entity)
        {
            if (entity is not IAuditable e) return;
            e.LastModifiedAt = DateTime.Now;

            e.LastModifiedBy = CurrentUser?.Id ?? 0;
        }

        /// <summary>
        /// 树形列表基类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class TreeList<T> where T : TreeList<T>
        {
            public List<T> Children { get; set; }
        }
    }
}