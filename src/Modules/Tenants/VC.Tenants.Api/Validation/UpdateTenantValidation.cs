using FluentValidation;
using VC.Tenants.Api.Models.Request.Tenant;
using VC.Tenants.Entities;

namespace VC.Tenants.Api.Validation;

internal class UpdateTenantValidation : AbstractValidator<UpdateTenantRequest>
{
    public UpdateTenantValidation()
    {
        RuleFor(ctr => ctr)
           .NotNull();

        RuleFor(ctr => ctr.Name)
            .MinimumLength(Tenant.NameMinLenght)
            .MaximumLength(Tenant.NameMaxLenght)
            .NotEmpty();

        RuleFor(ctr => ctr.Slug)
            .MinimumLength(Tenant.SlugMinLength)
            .MaximumLength(Tenant.SlugMaxLength)
            .NotEmpty();

        RuleFor(ctr => ctr.Config)
            .NotNull();

        RuleFor(ctr => ctr.Config)
            .ChildRules(tcd =>
            {
                tcd.RuleFor(tcd => tcd.About)
                    .MinimumLength(TenantConfiguration.AboutMinLength)
                    .MaximumLength(TenantConfiguration.AboutMaxLength)
                    .NotEmpty();

                tcd.RuleFor(tcd => tcd.Currency)
                    .MinimumLength(TenantConfiguration.CurrencyMinLength)
                    .MaximumLength(TenantConfiguration.CurrencyMaxLength)
                    .NotEmpty();

                tcd.RuleFor(tcd => tcd.Language)
                    .MinimumLength(TenantConfiguration.LanguageMinLength)
                    .MaximumLength(TenantConfiguration.LanguageMaxLength)
                    .NotEmpty();

                tcd.RuleFor(tcd => tcd.TimeZoneId)
                    .MinimumLength(TenantConfiguration.TimeZoneIdMinLength)
                    .MaximumLength(TenantConfiguration.TimeZoneIdMaxLength)
                    .NotEmpty();
            });

        RuleFor(ctr => ctr.Status)
            .Must(ctr => ctr != TenantStatus.None)
            .IsInEnum();

        RuleFor(ctr => ctr.ContactInfo)
            .ChildRules(con =>
            {
                con.RuleFor(ctr => ctr.Email)
                .MaximumLength(ContactInfo.EmailAddressMaxLength)
                .NotNull()
                .EmailAddress();

                con.RuleFor(ctr => ctr.Phone)
                .MinimumLength(ContactInfo.PhoneNumberMinLength)
                .MaximumLength(ContactInfo.PhoneNumberMaxLength)
                .NotEmpty();

                con.RuleFor(ctr => ctr.Address)
                .NotNull()
                .ChildRules(add =>
                {
                    add.RuleFor(tn => tn.Country)
                    .MinimumLength(Address.CountryMinLength)
                    .MaximumLength(Address.CountryMaxLength)
                    .NotEmpty();

                    add.RuleFor(tn => tn.City)
                    .MinimumLength(Address.CityMinLength)
                    .MaximumLength(Address.CityMaxLength)
                    .NotEmpty();

                    add.RuleFor(tn => tn.Street)
                    .MinimumLength(Address.StreetMinLength)
                    .MaximumLength(Address.StreetMaxLength)
                    .NotEmpty();

                    add.RuleFor(tn => tn.House)
                    .Must(tn => tn >= Address.HouseMinNum && tn <= Address.HouseMaxNum);
                });

            });

        RuleFor(ctr => ctr.WorkSchedule)
            .NotNull();

        RuleFor(ctr => ctr.WorkSchedule)
            .ChildRules(wc =>
            {
                wc.RuleFor(wc => wc)
                .NotNull()
                .Must(wk => wk.Count == Enum.GetValues(typeof(DayOfWeek)).Length)
                .Must(wk => wk.DistinctBy(wd => wd.Day).Count() == wk.Count)
                .Must(wk => wk.All(x => x.StartWork != x.EndWork && x.StartWork < x.EndWork))
                .Must(wk => wk.All(t => t.StartWork.Kind == DateTimeKind.Utc && t.EndWork.Kind == DateTimeKind.Utc))
                .WithMessage("Time Must be in UTC format");
            });
    }
}