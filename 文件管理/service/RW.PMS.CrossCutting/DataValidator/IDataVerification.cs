namespace RW.PMS.CrossCutting.DataValidator;

public interface IDataVerification
{
    /// <summary>
    ///     数据校验
    /// </summary>
    /// <returns></returns>
    DataValidatorResult TryValidate();
}