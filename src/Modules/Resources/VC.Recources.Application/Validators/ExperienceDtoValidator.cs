using FluentValidation;
using VC.Recources.Application.Endpoints.Models.Dto;

namespace VC.Recources.Application.Validators;

internal sealed class ExperienceDtoValidator : AbstractValidator<ExperienceDto>
{
    public ExperienceDtoValidator()
    {
        RuleFor(x => x.Years)
            .GreaterThanOrEqualTo(0).WithMessage("Years must be greater than or equal to 0");

        RuleFor(x => x.Months)
            .InclusiveBetween(0, 11).WithMessage("Months must be greater than or equal to 1 and not more than 11");
    }
}