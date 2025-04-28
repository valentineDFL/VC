using FluentValidation;
using VC.Services.Application.ResourcesUseCases.Models;

namespace VC.Services.Application.ResourcesUseCases.Validators;

public sealed class UpdateResourceDtoValidator : AbstractValidator<UpdateResourceParams>
{
    public UpdateResourceDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(50).WithMessage("Name must not exceed 50 characters")
            .Must(name => name.All(char.IsLetter));

        RuleFor(d => d.Description)
            .NotEmpty().WithMessage("Description is required")
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters");
    }
}