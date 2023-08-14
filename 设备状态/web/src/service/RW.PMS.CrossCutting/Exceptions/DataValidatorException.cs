using System;
using System.Collections.Generic;
using RW.PMS.CrossCutting.DataValidator;

namespace RW.PMS.CrossCutting.Exceptions;

public class DataValidatorException : ApplicationException
{
    /// <summary>
    ///     验证结果
    /// </summary>
    public readonly IList<FieldValidationResult> ValidationResults;

    public DataValidatorException(IList<FieldValidationResult> validationResults)
    {
        ValidationResults = validationResults;
    }
}