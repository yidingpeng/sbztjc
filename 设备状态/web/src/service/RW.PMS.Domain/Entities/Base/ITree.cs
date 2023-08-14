namespace RW.PMS.Domain.Entities.Base
{
    /// <summary>
    ///     树结构表接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITree<T>
    {
        /// <summary>
        ///     上级Id
        /// </summary>
        T ParentId { get; set; }
    }
}