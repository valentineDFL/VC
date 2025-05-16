using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace VC.Auth.Api.Validations;

public static class FluentValidationExtensions
{
    public static ActionResult ToErrorActionResult(this ValidationResult validationResult)
        => validationResult.IsValid
            ? throw new Exception("The result is valid.")
            : new BadRequestObjectResult(new
            {
                Errors = validationResult.Errors.Select(e => new
                {
                    e.PropertyName,
                    e.ErrorMessage
                })
            });
}