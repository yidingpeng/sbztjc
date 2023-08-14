using System;
using FreeSql.DataAnnotations;

namespace RW.PMS.Domain.Entities.Base
{

    /// <summary>
    /// 完整实体
    /// </summary>
    public class FullEntity : FullEntity<int>
    {
    }

    /// <summary>
    /// 完整实体
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class FullEntity<TKey> : IEDRoomJiaoZhunTimeEntityntity<TKey>, IFullEntity<TKey>
    {
        /// <summary>
        ///     创建人
        /// </summary>
        [Column(Position = -5)]
        public int CreatedBy { get; set; }

        /// <summary>
        ///     创建时间
        /// </summary>
        [Column(Position = -4)]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        ///     最后修改人
        /// </summary>
        [Column(Position = -3)]
        public int LastModifiedBy { get; set; }

        /// <summary>
        ///     最后修改时间
        /// </summary>
        [Column(Position = -2)]
        public DateTime LastModifiedAt { get; set; }

        /// <summary>
        ///     是否删除
        /// </summary>
        [Column(Position = -1)]
        public bool IsDeleted { get; set; }
    }
}