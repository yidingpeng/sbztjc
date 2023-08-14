using System.Collections.Generic;

namespace RW.PMS.Application.Contracts.DTO
{
    /// <summary>
    /// 分页数据结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedResult<T>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="list">数据</param>
        public PagedResult(IReadOnlyList<T> list)
        {
            List = list;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="list">数据</param>
        /// <param name="total">数据总数</param>
        public PagedResult(IReadOnlyList<T> list, long total) : this(list)
        {
            Total = total;
        }

        /// <summary>
        /// 数据总数
        /// </summary>
        public long Total { get; }

        /// <summary>
        /// 数据
        /// </summary>
        public IReadOnlyList<T> List { get; }
    }
}
