namespace RW.PMS.Domain.ValueObject
{
    /// <summary>
    ///     消息目标类型
    /// </summary>
    public enum MessageTarget
    {
        /// <summary>
        ///     缺省（由业务确认）
        /// </summary>
        Default = 1,

        /// <summary>
        ///     组织机构
        /// </summary>
        Organization = 2,

        /// <summary>
        ///     角色
        /// </summary>
        Role = 3,

        /// <summary>
        ///     用户
        /// </summary>
        User = 4
    }
}