using FluentValidation;
using VC.Auth.Application.Models;

namespace VC.Auth.Application.Validators;

public class SignUpValidation : AbstractValidator<RegisterAuthParams>
{
    public SignUpValidation()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Your password cannot be empty")
            .MinimumLength(4).WithMessage("Your password length must be at least 4.")
            .MaximumLength(64).WithMessage("Your password length must not exceed 64.")
            .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
            .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
            .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
            .Matches(@"[\!\?\*\.]*$").WithMessage("Your password must contain at least one (!? *.).");
    }
}