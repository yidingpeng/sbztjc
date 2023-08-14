using FluentValidation;
using RW.PMS.Application.Contracts.DTO.Module;

namespace RW.PMS.Application.Validator.Module;

public class ModuleDetailValidator : AbstractValidator<ModuleDetailDto>
{
    public ModuleDetailValidator()
    {
        //RuleFor(t=>t.ModuleCode).NotEmpty().MaximumLength(10).EmailAddress();
    }
}