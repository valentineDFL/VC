using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace VC.Recources.Application.Helpers;

public static class ValidationHelper
{
    public static ActionResult  Validate<T>(T dto, AbstractValidator<T> validator)
    {
        var validationResult = validator.Validate(dto);

        return validationResult.IsValid
            ? null
            : new BadRequestObjectResult(new
            {
                Errors = validationResult.Errors.Select(e => new
                {
                    e.PropertyName,
                    e.ErrorMessage
                })
            });
    }
}