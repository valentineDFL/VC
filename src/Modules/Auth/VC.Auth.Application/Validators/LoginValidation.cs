using FluentValidation;
using VC.Auth.Application.Models.Requests;

namespace VC.Auth.Application.Validators;

public class LoginValidation : AbstractValidator<LoginRequest>
{
    public LoginValidation()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Your password cannot be empty");
    }
}