namespace RW.PMS.Domain.ValueObject
{
    /// <summary>
    ///     报警来源
    /// </summary>
    public enum AlarmSource
    {
        /// <summary>
        ///     设备
        /// </summary>
        Equipment = 1,

        /// <summary>
        ///     MOM系统
        /// </summary>
        MOM = 2,

        /// <summary>
        ///     物流
        /// </summary>
        Logistics = 3
    }
}