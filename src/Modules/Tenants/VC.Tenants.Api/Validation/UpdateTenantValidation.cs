using FluentValidation;
using VC.Tenants.Api.Endpoints.Tenants.Models;

namespace VC.Tenants.Api.Validation;

internal class UpdateTenantValidation : AbstractValidator<UpdateTenantRequest>
{
    public UpdateTenantValidation()
    {
        RuleFor(ctr => ctr.TenantId)
            .NotEmpty();

        RuleFor(ctr => ctr)
            .NotNull();

        RuleFor(ctr => ctr.Name)
            .MinimumLength(3)
            .MaximumLength(32)
            .NotEmpty();

        RuleFor(ctr => ctr.Slug)
            .MinimumLength(10)
            .MaximumLength(128)
            .NotEmpty();

        RuleFor(ctr => ctr.Config)
            .NotNull();

        RuleFor(ctr => ctr)
            .ChildRules(ctr =>
            {
                ctr.RuleFor(tcd => tcd.Config.About)
                    .MinimumLength(16)
                    .MaximumLength(256)
                    .NotEmpty();

                ctr.RuleFor(tcd => tcd.Config.Currency)
                    .MinimumLength(3)
                    .MaximumLength(3)
                    .NotEmpty();

                ctr.RuleFor(tcd => tcd.Config.Language)
                    .MinimumLength(2)
                    .MaximumLength(3)
                    .NotEmpty();

                ctr.RuleFor(tcd => tcd.Config.TimeZoneId) // RE WRITE IN FUTURE
                    .MinimumLength(2)
                    .MaximumLength(3)
                    .NotEmpty();

            });

        RuleFor(ctr => ctr.Status)
            .Must(ctr => ctr != Entities.TenantStatus.None)
            .IsInEnum();

        RuleFor(ctr => ctr)
            .NotNull();

        RuleFor(ctr => ctr)
            .ChildRules(ctr =>
            {
                ctr.RuleFor(ctr => ctr.Contact.Email)
                .MaximumLength(64)
                .NotNull();

                ctr.RuleFor(ctr => ctr.Contact.Phone)
                .MinimumLength(15)
                .MaximumLength(16)
                .NotEmpty();

                ctr.RuleFor(ctr => ctr.Contact.Address)
                .MinimumLength(8)
                .MaximumLength(48);
            });

        RuleFor(ctr => ctr.WorkSchedule)
            .NotNull();

        RuleFor(ctr => ctr)
            .ChildRules(ctr =>
            {
                ctr.RuleFor(ctr => ctr.WorkSchedule.WeekDays)
                .NotNull()
                .Must(wk => wk.Count == 7)
                .Must(x => DayRepeatChecker.IsDayRepeat(x) == false);
            });
    }
}
