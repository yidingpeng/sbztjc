using System.Collections.Generic;

namespace RW.PMS.CrossCutting.DataValidator;

/// <summary>
///     数据验证结果
/// </summary>
public class DataValidatorResult
{
    /// <summary>
    ///     验证状态
    /// </summary>
    public bool IsValid { get; set; }

    /// <summary>
    ///     验证结果
    /// </summary>
    public IList<FieldValidationResult> Result { get; set; }
}

/// <summary>
///     字段错误信息
/// </summary>
public class FieldValidationResult
{
    /// <summary>
    ///     构造函数
    /// </summary>
    /// <param name="field">错误字段</param>
    /// <param name="message">错误信息</param>
    public FieldValidationResult(string field, string message)
    {
        ErrorField = field;
        ErrorMessage = message;
    }

    /// <summary>
    ///     错误信息
    /// </summary>
    public string ErrorMessage { get; set; }

    /// <summary>
    ///     错误字段
    /// </summary>
    public string ErrorField { get; set; }
}