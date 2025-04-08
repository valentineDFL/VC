using FluentValidation;
using VC.Recources.Application.Endpoints.Models.Dto;

namespace VC.Recources.Application.Validators;

internal sealed class SkillDtoValidator : AbstractValidator<SkillDto>
{
    public SkillDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Skill name is required")
            .MaximumLength(50).WithMessage("Skill name must not exceed 50 characters")
            .Must(name => name.All(char.IsLetter)).WithMessage("Skill name contains invalid characters");

        RuleFor(x => x.Experience).SetValidator(new ExperienceDtoValidator());
    }
}