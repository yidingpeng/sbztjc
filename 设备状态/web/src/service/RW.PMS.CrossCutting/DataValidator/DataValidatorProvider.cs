using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.Extensions.Localization;

namespace RW.PMS.CrossCutting.DataValidator;

public class DataValidatorProvider : IDataValidatorProvider
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IStringLocalizerFactory _stringLocalizerFactory;

    public DataValidatorProvider(IStringLocalizerFactory stringLocalizerFactory, IServiceProvider serviceProvider)
    {
        _stringLocalizerFactory = stringLocalizerFactory;
        _serviceProvider = serviceProvider;
    }

    public DataValidatorResult TryValidate<T>(T item) where T : class
    {
        ICollection<ValidationResult> results = new List<ValidationResult>();
        var isValid =
            Validator.TryValidateObject(item, new ValidationContext(item, _serviceProvider, null), results, true);
        var vResult = new DataValidatorResult { IsValid = isValid };
        if (!isValid)
        {
            vResult.Result = new List<FieldValidationResult>();
            var localizer = _stringLocalizerFactory.Create(typeof(T));
            foreach (var validationResult in results)
                vResult.Result.Add(new FieldValidationResult(validationResult.MemberNames.First(),
                    localizer[validationResult.ErrorMessage ?? string.Empty]));
        }

        return vResult;
    }
}