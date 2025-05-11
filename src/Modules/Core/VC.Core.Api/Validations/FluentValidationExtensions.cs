using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace VC.Core.Api.Validations;

public static class FluentValidationExtensions
{
    public static ActionResult ToErrorActionResult(this ValidationResult validationResult)
    {
        return validationResult.IsValid
            ? throw new Exception("Результат оказался валидным")
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