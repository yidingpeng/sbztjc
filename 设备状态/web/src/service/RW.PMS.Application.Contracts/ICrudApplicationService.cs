using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Domain.Entities.Base;

namespace RW.PMS.Application.Contracts
{
    /// <summary>
    ///     应用服务接口
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TKey">主键</typeparam>
    public interface ICrudApplicationService<TEntity, TKey> : IApplicationService
        where TEntity : class, IEntity<TKey>, new()
    {
        /// <summary>
        ///     根据主键获取实体
        /// </summary>
        /// <param name="key">主键</param>
        /// <returns></returns>
        TEntity Get(TKey key);

        /// <summary>
        ///     新增
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        TEntity Insert<TInput>(TInput input) where TInput : class;


        /// <summary>
        ///     更新
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="key">主键</param>
        /// <param name="input"></param>
        /// <returns></returns>
        int Update<TInput>(TKey key, TInput input) where TInput : class;

        /// <summary>
        ///     删除
        /// </summary>
        /// <param name="key">主键</param>
        /// <returns></returns>
        int Delete(TKey key);

        /// <summary>
        ///     批量删除
        /// </summary>
        /// <param name="keys">主键数组</param>
        /// <returns></returns>
        int Delete(TKey[] keys);

        /// <summary>
        ///     获取全部数据
        /// </summary>
        /// <returns></returns>
        IList<TEntity> GetList();

        /// <summary>
        ///     获取部分数据
        /// </summary>
        /// <typeparam name="TMember"></typeparam>
        /// <param name="order">排序</param>
        /// <param name="descending">是否倒序</param>
        /// <param name="count">需要获取数据的条数</param>
        /// <returns></returns>
        IList<TEntity> GetList<TMember>(Expression<Func<TEntity, TMember>> order, bool descending = false,
            int count = 10);

        /// <summary>
        ///     获取全部数据
        /// </summary>
        /// <typeparam name="TOutput">输入实体</typeparam>
        /// <returns></returns>
        IList<TOutput> GetList<TOutput>();

        /// <summary>
        ///     获取部分数据
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TMember"></typeparam>
        /// <param name="order">排序</param>
        /// <param name="descending">是否倒序</param>
        /// <param name="count">需要获取数据的条数</param>
        /// <returns></returns>
        IList<TOutput> GetList<TOutput, TMember>(Expression<Func<TEntity, TMember>> order, bool descending = false,
            int count = 10);

        /// <summary>
        ///     构建树列表
        /// </summary>
        /// <typeparam name="TOut">输出对象</typeparam>
        /// <typeparam name="TIn">输入对象</typeparam>
        /// <typeparam name="TTreeKey">树对象主键</typeparam>
        /// <param name="sources">源数据</param>
        /// <param name="parentId">父级Id</param>
        /// <returns></returns>
        List<TOut> BuildTreeList<TOut, TIn, TTreeKey>(IList<TIn> sources, TTreeKey parentId = default, Func<TIn, TKey> order = null)
            where TOut : TreeList<TOut> where TIn : IEntity<TTreeKey>, ITree<TTreeKey>;

        /// <summary>
        ///     构建树列表
        /// </summary>
        /// <typeparam name="TOut">输出对象</typeparam>
        /// <typeparam name="TIn">输入对象</typeparam>
        /// <param name="sources">源数据</param>
        /// <param name="parentId">父级Id</param>
        /// <returns></returns>
        List<TOut> BuildTreeList<TOut, TIn>(IList<TIn> sources, int parentId = default, Func<TIn, TKey> order = null)
            where TOut : TreeList<TOut> where TIn : IEntity<int>, ITree<int>;
    }
}