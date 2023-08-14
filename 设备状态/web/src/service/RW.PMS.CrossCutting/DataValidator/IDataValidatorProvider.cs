namespace RW.PMS.CrossCutting.DataValidator
{
    /// <summary>
    ///     数据验证接口
    /// </summary>
    public interface IDataValidatorProvider
    {
        /// <summary>
        ///     是否通过验证
        /// </summary>
        /// <typeparam name="T">需要验证的实体类型</typeparam>
        /// <param name="item">需要验证的实体</param>
        /// <returns></returns>
        DataValidatorResult TryValidate<T>(T item) where T : class;
    }
}