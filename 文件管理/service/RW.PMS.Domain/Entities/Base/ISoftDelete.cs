namespace RW.PMS.Domain.Entities.Base
{
    /// <summary>
    ///     逻辑删除接口
    /// </summary>
    public interface ISoftDelete
    {
        /// <summary>
        ///     是否删除
        /// </summary>
        bool IsDeleted { get; set; }
    }
}