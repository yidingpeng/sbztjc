using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using RW.PMS.Application.Contracts;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.Exceptions;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.Base;
using RW.PMS.Domain.Entities.DeviceStatus;
using RW.PMS.Domain.Repositories;

namespace RW.PMS.Application
{
    public abstract class CrudApplicationService<TEntity, TKey> : CrudApplicationService<TEntity, TKey, IRepository<TEntity, TKey>>
        where TEntity : class, IEntity<TKey>, new()
    {
       // private IRepository<DeviceTestRoomEntity, int> roleRepository;

        protected CrudApplicationService(IDataValidatorProvider dataValidator, IRepository<TEntity, TKey> repository,
            IMapper mapper, Lazy<ICurrentUser> currentUser) : base(dataValidator, repository, mapper, currentUser)
        {
        }

       
    }

    public abstract class CrudApplicationService<TEntity, TKey, TRepository> : ApplicationService,
        ICrudApplicationService<TEntity, TKey>
        where TEntity : class, IEntity<TKey>, new() where TRepository : IRepository<TEntity, TKey>
    {
        protected readonly TRepository Repository;

        protected CrudApplicationService(IDataValidatorProvider dataValidator, TRepository repository,
            IMapper mapper, Lazy<ICurrentUser> currentUser) : base(dataValidator, mapper, currentUser)
        {
            Repository = repository;
        }

        public virtual TEntity Get(TKey key)
        {
            return Repository.Get(key);
        }

        public virtual TEntity Insert<TInput>(TInput input) where TInput : class
        {
            DataValidate(input);
            var entity = Mapper.Map<TEntity>(input);
            return Repository.Insert(entity);
        }

        public virtual int Update<TInput>(TKey key, TInput input) where TInput : class
        {
            DataValidate(input);
            var entity = Repository.Where(a => a.Id.Equals(key)).First();
            Mapper.Map(input, entity);
            return Repository.Update(entity);
        }

        public virtual int Delete(TKey key)
        {
            return Repository.Delete(key);
        }

        public virtual int Delete(TKey[] keys)
        {
            return Repository.Delete(keys);
        }

        public virtual IList<TEntity> GetList()
        {
            return Repository.Select.ToList();
        }

        public IList<TEntity> GetList<TMember>(Expression<Func<TEntity, TMember>> order, bool descending = false,
            int count = 10)
        {
            var query = descending ? Repository.Select.OrderByDescending(order) : Repository.Select.OrderBy(order);
            return query.Take(count).ToList();
        }

        public virtual IList<TOutput> GetList<TOutput>()
        {
            var list = GetList();
            return Mapper.Map<IList<TOutput>>(list);
        }

        public IList<TOutput> GetList<TOutput, TMember>(Expression<Func<TEntity, TMember>> order,
            bool descending = false, int count = 10)
        {
            var list = GetList(order, descending, count);
            return Mapper.Map<IList<TOutput>>(list);
        }

        public List<TOut> BuildTreeList<TOut, TIn, TTreeKey>(IList<TIn> sources, TTreeKey parentId = default, Func<TIn, TKey> order = null)
            where TOut : TreeList<TOut> where TIn : IEntity<TTreeKey>, ITree<TTreeKey>
        {
            if (order == null) order = x => default(TKey);
            List<TOut> treeList = null;
            var item = sources.Where(t => t.ParentId.Equals(parentId)).OrderBy(order).ToList();
            if (item.Any())
            {
                treeList = new List<TOut>();
                foreach (var treeItem in item)
                {
                    var outItem = Mapper.Map<TOut>(treeItem);
                    outItem.Children = BuildTreeList<TOut, TIn, TTreeKey>(sources, treeItem.Id, order);
                    treeList.Add(outItem);
                }
            }

            return treeList;
        }

        public List<TOut> BuildTreeList<TOut, TIn>(IList<TIn> sources, int parentId = default, Func<TIn, TKey> order = null)
            where TOut : TreeList<TOut> where TIn : IEntity<int>, ITree<int>
        {
            return BuildTreeList<TOut, TIn, int>(sources, parentId, order);
        }

        /// <summary>
        ///     数据验证
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="input"></param>
        /// <exception cref="DataValidatorException"></exception>
        protected void DataValidate<TInput>(TInput input) where TInput : class
        {
            var results = new List<FieldValidationResult>();
            var vPara = DataValidator.TryValidate(input);
            if (!vPara.IsValid)
            {
                results.AddRange(vPara.Result);
            }
            if (this is IDataVerification v)
            {
                var vData = v.TryValidate();
                if (!vData.IsValid)
                {
                    results.AddRange(vData.Result);
                }
            }
            if (results.Any()) throw new DataValidatorException(results);
        }
    }
}