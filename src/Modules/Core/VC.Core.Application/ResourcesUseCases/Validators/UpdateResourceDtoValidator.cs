using FluentValidation;
using VC.Core.Application.ResourcesUseCases.Models;

namespace VC.Core.Application.ResourcesUseCases.Validators;

public sealed class UpdateResourceDtoValidator : AbstractValidator<UpdateResourceParams>
{
    public UpdateResourceDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(50)
            .Must(name => name.All(char.IsLetter)).WithMessage("Name contains invalid characters");

        RuleFor(d => d.Description)
            .NotEmpty()
            .MaximumLength(500);
        
        RuleFor(r => r.Count)
            .GreaterThan(0);
    }
}