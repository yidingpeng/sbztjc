namespace RW.PMS.Domain.ValueObject
{
    /// <summary>
    ///     消息提醒方式
    /// </summary>
    public enum MessageRemindWay
    {
        /// <summary>
        ///     系统消息
        /// </summary>
        System = 1,

        /// <summary>
        ///     短信
        /// </summary>
        SMS = 2,

        /// <summary>
        ///     邮件
        /// </summary>
        Email = 3,
    }
}