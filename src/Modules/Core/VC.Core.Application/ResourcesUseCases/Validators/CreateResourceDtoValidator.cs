using FluentValidation;
using VC.Core.Application.ResourcesUseCases.Models;

namespace VC.Core.Application.ResourcesUseCases.Validators;

public class CreateResourceDtoValidator : AbstractValidator<CreateResourceParams>
{
    public CreateResourceDtoValidator()
    {
        RuleFor(r => r.Title)
            .NotEmpty()
            .MaximumLength(50)
            .Must(name => name.All(char.IsLetter)).WithMessage("Name contains invalid characters");

        RuleFor(r => r.Description)
            .NotEmpty()
            .MaximumLength(500);
        
        RuleFor(r => r.Count)
            .GreaterThan(0);
    }
}