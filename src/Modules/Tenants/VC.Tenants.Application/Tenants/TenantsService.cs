using FluentResults;
using VC.MailkitIntegration;
using VC.Tenants.Application.Models.Create;
using VC.Tenants.Application.Models.Update;
using VC.Tenants.Entities;
using VC.Tenants.Repositories;
using VC.Tenants.UnitOfWork;

namespace VC.Tenants.Application.Tenants;

internal class TenantsService : ITenantsService
{
    private readonly ITenantRepository _tenantRepository;
    private readonly IDbSaver _dbSaver;

    private readonly ISlugGenerator _slugGenerator;
    private readonly IEmailVerifyCodeGenerator _emailVerifyCodeGenerator;

    private readonly IMailSenderService _mailSenderService;

    private readonly ITEnantEmailVerificationFormFactory _formFactory;

    public TenantsService(ITenantRepository tenantRepository,
                          IDbSaver dbSaver,
                          ISlugGenerator slugGenerator,
                          IEmailVerifyCodeGenerator emailVerifyCodeGenerator,
                          IMailSenderService mailSenderService,
                          ITEnantEmailVerificationFormFactory formFactory)
    {
        _tenantRepository = tenantRepository;
        _dbSaver = dbSaver;
        _slugGenerator = slugGenerator;
        _emailVerifyCodeGenerator = emailVerifyCodeGenerator;
        _mailSenderService = mailSenderService;
        _formFactory = formFactory;
    }

    public async Task<Result<Tenant>> GetAsync()
    {
        var tenant = await _tenantRepository.GetAsync();

        if (tenant is null)
            return Result.Fail("Tenant Not Found");

        return Result.Ok(tenant);
    }

    public async Task<Result> CreateAsync(CreateTenantParams @params)
    {
        var paramsAddress = @params.ContactInfo.AddressDto;
        var address = Address.Create(paramsAddress.Country, paramsAddress.City, paramsAddress.Street, paramsAddress.House);

        var paramsEmailAddress = @params.ContactInfo.EmailAddressDto;

        var code = _emailVerifyCodeGenerator.GenerateCode();

        var emailAddress = EmailAddress.Create(paramsEmailAddress.Email, false, code, DateTime.UtcNow.AddMinutes(EmailAddress.CodeMinuteValidTime));

        var contactInfo = ContactInfo.Create(@params.ContactInfo.Phone, address, emailAddress);

        var paramConfig = @params.Config;
        var config = TenantConfiguration.Create(paramConfig.About, paramConfig.Currency, paramConfig.Language, paramConfig.TimeZoneId);

        var tenantId = Guid.CreateVersion7();

        var weekShedule = @params.WorkSchedule.WeekSchedule.Select(x => DaySchedule.Create(Guid.CreateVersion7(), tenantId, x.Day, x.StartWork, x.EndWork)).ToList();
        var workShedule = WorkSchedule.Create(weekShedule);

        var slug = _slugGenerator.GenerateSlug(@params.Name);
        var tenant = Tenant.Create(tenantId, @params.Name, slug, config, @params.Status, contactInfo, workShedule);

        await _tenantRepository.AddAsync(tenant);
        await _dbSaver.SaveAsync();

        var message = _formFactory.GetRegistrationMessageForm(tenant.ContactInfo.EmailAddress.Code, tenant.Name, tenant.ContactInfo.EmailAddress.Email);

        var sendResult = await _mailSenderService.SendMailAsync(message);

        if (!sendResult.IsSuccess)
            return Result.Fail(sendResult.Value);

        return Result.Ok();
    }

    public async Task<Result> UpdateAsync(UpdateTenantParams @params)
    {
        var tenant = await _tenantRepository.GetAsync();

        if (tenant == null)
            return Result.Fail("Tenant Not Found");

        var config = TenantConfiguration.Create(@params.Config.About, @params.Config.Currency, @params.Config.Language, @params.Config.TimeZoneId);

        var weekSchedule = @params.WorkSchedule.WeekSchedule
            .Select(d => DaySchedule.Create(d.Id, tenant.Id, d.Day, d.StartWork, d.EndWork))
            .OrderBy(d => d.Day).ToList();

        var workSchedule = WorkSchedule.Create(weekSchedule);

        var tenantContactInfo = tenant.ContactInfo.EmailAddress;
        var emailAddress = EmailAddress.Create(@params.ContactInfo.UpdateEmailAddressDto.Email, tenantContactInfo.IsVerify, tenantContactInfo.Code, tenantContactInfo.ConfirmationTime);

        var paramsAddress = @params.ContactInfo.AddressDto;
        var address = Address.Create(paramsAddress.Country, paramsAddress.City, paramsAddress.Street, paramsAddress.House);

        var contactInfo = ContactInfo.Create(@params.ContactInfo.Phone, address, emailAddress);

        tenant.Update(config, @params.Status, contactInfo, workSchedule);

        await _tenantRepository.UpdateAsync(tenant);
        await _dbSaver.SaveAsync();

        return Result.Ok();
    }

    public async Task<Result> DeleteAsync()
    {
        var existingTenant = await _tenantRepository.GetAsync();

        if (existingTenant is null)
            return Result.Fail("Tenant Not found");

        _tenantRepository.Remove(existingTenant);

        await _dbSaver.SaveAsync();

        return Result.Ok();
    }

    public async Task<Result> VerifyEmailAsync(string code)
    {
        var tenant = await _tenantRepository.GetAsync();

        if (tenant is null)
            return Result.Fail("Tenant Not Found");

        if (tenant.ContactInfo.EmailAddress.IsVerify)
            return Result.Fail("Tenant has already been verified");

        if (tenant.ContactInfo.EmailAddress.ConfirmationTimeExpired)
            return Result.Fail("Confirmation Time has expired");

        if (tenant.ContactInfo.EmailAddress.Code != code)
            return Result.Fail("Codes does not equals");

        var emailAddres = tenant.ContactInfo.EmailAddress;
        var updatedEmailAddress = EmailAddress.Create(emailAddres.Email, true, emailAddres.Code, emailAddres.ConfirmationTime);
        var updatedContactInfo = ContactInfo.Create(tenant.ContactInfo.Phone, tenant.ContactInfo.Address, updatedEmailAddress);

        tenant.Update(tenant.Config, tenant.Status, updatedContactInfo, tenant.WorkSchedule);

        await _tenantRepository.UpdateAsync(tenant);

        await _dbSaver.SaveAsync();

        return Result.Ok();
    }

    public async Task<Result> SendVerificationMailAsync()
    {
        var tenant = await _tenantRepository.GetAsync();

        if (tenant == null)
            return Result.Fail("Tenant not found");

        var emailAddress = tenant.ContactInfo.EmailAddress;

        var newVerifyCode = _emailVerifyCodeGenerator.GenerateCode();
        var updatedEmailAddress = EmailAddress.Create(emailAddress.Email, emailAddress.IsVerify, newVerifyCode, DateTime.UtcNow.AddMinutes(EmailAddress.CodeMinuteValidTime));

        var updatedContactInfo = ContactInfo.Create(tenant.ContactInfo.Phone, tenant.ContactInfo.Address, updatedEmailAddress);

        tenant.Update(tenant.Config, tenant.Status, updatedContactInfo, tenant.WorkSchedule);

        await _tenantRepository.UpdateAsync(tenant);

        await _dbSaver.SaveAsync();

        var message = _formFactory.GetVerifyMessageEmailForm(newVerifyCode, tenant.Name, tenant.ContactInfo.EmailAddress.Email);
        var sendMailResult = await _mailSenderService.SendMailAsync(message);

        if (!sendMailResult.IsSuccess)
            return Result.Fail(sendMailResult.Value);

        return Result.Ok();
    }
}