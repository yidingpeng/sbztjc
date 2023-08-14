using FreeSql.DataAnnotations;

namespace RW.PMS.Domain.Entities.Base
{
    /// <summary>
    /// 实体基类
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    public class IEDRoomJiaoZhunTimeEntityntity<TKey> : IEntity<TKey>
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Column(IsPrimary = true, IsIdentity = true, Position = 1)]
        public virtual TKey Id { get; set; }
    }
}