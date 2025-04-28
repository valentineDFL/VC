using FluentValidation;
using VC.Recources.Application.Endpoints.Models.Requests;

namespace VC.Recources.Application.Validators;

public class CreateResourceDtoValidator : AbstractValidator<CreateDto>
{
    public CreateResourceDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(50).WithMessage("Name must not exceed 50 characters")
            .Must(name => name.All(char.IsLetter)).WithMessage("Name contains invalid characters");

        RuleFor(d => d.Description)
            .NotEmpty().WithMessage("Description is required")
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters");

        RuleFor(x => x.Skills)
            .ForEach(skillRule => skillRule.SetValidator(new SkillDtoValidator()));
    }
}