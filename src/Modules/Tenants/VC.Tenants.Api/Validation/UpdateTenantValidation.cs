using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VC.Tenants.Api.Endpoints.Tenants.Models;

namespace VC.Tenants.Api.Validation
{
    internal class UpdateTenantValidation : AbstractValidator<UpdateTenantRequest>
    {
        public UpdateTenantValidation()
        {
            // Id Validation
            RuleFor(ctr => ctr.TenantId)
                .NotEmpty();

            // UpdateTenantRequestDto Validation
            RuleFor(ctr => ctr)
                .NotNull();

            // Name Validation
            RuleFor(ctr => ctr.Name)
                .MinimumLength(3)
                .MaximumLength(32)
                .NotEmpty();

            // Slug Validation
            RuleFor(ctr => ctr.Slug)
                .MinimumLength(10)
                .MaximumLength(128)
                .NotEmpty();

            //Config Validation
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

            // Status Validation
            RuleFor(ctr => ctr.Status)
                .Must(ctr => ctr != Models.TenantStatus.None)
                .IsInEnum();


            // Contact Validation
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

            // TenantWeekWorkScheduleDto Validation
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
}
