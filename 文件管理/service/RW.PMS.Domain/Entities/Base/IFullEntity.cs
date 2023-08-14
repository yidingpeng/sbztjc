namespace RW.PMS.Domain.Entities.Base
{
    /// <summary>
    /// 完整实体接
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IFullEntity<TKey> : IEntity<TKey>, IAuditable, ISoftDelete
    {
    }
}