using FluentValidation.Results;
using Microsoft.AspNetCore.Http;

namespace VC.Recources.Application.Helpers;

public static class ValidationHelper
{
    public static IResult ToErrorActionResult(this ValidationResult validationResult)
    {
        return validationResult.IsValid
            ? throw new Exception("Результат оказался валидным")
            : Results.BadRequest(new
            {
                Errors = validationResult.Errors.Select(e => new
                {
                    e.PropertyName,
                    e.ErrorMessage
                })
            });
    }
}