using System;
using System.Collections.Generic;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace RW.PMS.CrossCutting.DataValidator;

public class FluentDataValidatorProvider : IDataValidatorProvider
{
    private readonly IServiceProvider _serviceProvider;

    public FluentDataValidatorProvider(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public DataValidatorResult TryValidate<T>(T item) where T : class
    {
        var validatorResult = new DataValidatorResult { IsValid = true };
        var validator = _serviceProvider.GetService<IValidator<T>>();
        var result = validator?.Validate(item);
        if (result == null || result.IsValid) return validatorResult;
        validatorResult.IsValid = false;
        validatorResult.Result = new List<FieldValidationResult>();
        foreach (var error in result.Errors)
            validatorResult.Result.Add(new FieldValidationResult(error.PropertyName, error.ErrorMessage));
        return validatorResult;
    }
}