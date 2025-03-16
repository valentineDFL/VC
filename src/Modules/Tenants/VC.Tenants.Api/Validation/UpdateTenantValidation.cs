using FluentValidation;
using VC.Tenants.Api.Endpoints.Tenants.Models;

namespace VC.Tenants.Api.Validation;

internal class UpdateTenantValidation : AbstractValidator<UpdateTenantRequest>
{
    public UpdateTenantValidation()
    {
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

        RuleFor(ctr => ctr.Config)
            .ChildRules(tcd =>
            {
                tcd.RuleFor(tcd => tcd.About)
                    .MinimumLength(16)
                    .MaximumLength(256)
                    .NotEmpty();

                tcd.RuleFor(tcd => tcd.Currency)
                    .MinimumLength(3)
                    .MaximumLength(3)
                    .NotEmpty();

                tcd.RuleFor(tcd => tcd.Language)
                    .MinimumLength(2)
                    .MaximumLength(3)
                    .NotEmpty();

                tcd.RuleFor(tcd => tcd.TimeZoneId)
                    .MinimumLength(2)
                    .MaximumLength(3)
                    .NotEmpty();
            });

        RuleFor(ctr => ctr.Status)
            .Must(ctr => ctr != Entities.TenantStatus.None)
            .IsInEnum();

        RuleFor(ctr => ctr.Contact)
            .ChildRules(con =>
            {
                con.RuleFor(ctr => ctr.Email)
                .MaximumLength(64)
                .NotNull()
                .EmailAddress();

                con.RuleFor(ctr => ctr.Phone)
                .MinimumLength(15)
                .MaximumLength(16)
                .NotEmpty();

                con.RuleFor(ctr => ctr.Address)
                .MinimumLength(8)
                .MaximumLength(48);
            });

        RuleFor(ctr => ctr.WorkSchedule)
            .NotNull();

        RuleFor(ctr => ctr.WorkSchedule)
            .ChildRules(wc =>
            {
                wc.RuleFor(wc => wc.WeekDays)
                .NotNull()
                .Must(wk => wk.Count == 7)
                .Must(wk => wk.DistinctBy(wd => wd.Day).Count() == wk.Count);
            });
    }
}
