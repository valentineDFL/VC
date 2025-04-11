using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace VC.Recources.Application.Helpers;

public static class ValidationHelper
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