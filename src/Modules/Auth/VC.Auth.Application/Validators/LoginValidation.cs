using FluentValidation;
using VC.Auth.Application.Models;

namespace VC.Auth.Application.Validators;

public class LoginValidation : AbstractValidator<LoginAuthParams>
{
    public LoginValidation()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress();
            
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Your password cannot be empty");
    }
}