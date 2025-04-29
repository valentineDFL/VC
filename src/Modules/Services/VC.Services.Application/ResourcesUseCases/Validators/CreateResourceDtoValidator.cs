using FluentValidation;
using VC.Services.Application.ResourcesUseCases.Models;

namespace VC.Services.Application.ResourcesUseCases.Validators;

public class CreateResourceDtoValidator : AbstractValidator<CreateResourceParams>
{
    public CreateResourceDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(50).WithMessage("Name must not exceed 50 characters")
            .Must(name => name.All(char.IsLetter)).WithMessage("Name contains invalid characters");

        RuleFor(d => d.Description)
            .NotEmpty().WithMessage("Description is required")
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters");
    }
}