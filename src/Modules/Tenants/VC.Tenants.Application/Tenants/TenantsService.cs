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
    private readonly IEmailVerificationRepository _emailVerificationRepository;

    private readonly IDbSaver _dbSaver;

    private readonly ISlugGenerator _slugGenerator;
    private readonly IEmailVerifyCodeGenerator _emailVerifyCodeGenerator;

    private readonly IMailSender _mailSenderService;

    private readonly ITEnantEmailVerificationMessagesFactory _formFactory;

    public TenantsService(ITenantRepository tenantRepository,
                          IEmailVerificationRepository emailVerificationRepository,
                          IDbSaver dbSaver,
                          ISlugGenerator slugGenerator,
                          IEmailVerifyCodeGenerator emailVerifyCodeGenerator,
                          IMailSender mailSenderService,
                          ITEnantEmailVerificationMessagesFactory formFactory)
    {
        _tenantRepository = tenantRepository;
        _emailVerificationRepository = emailVerificationRepository;
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
        var tenant = CreateTenantFromParams(@params);

        await _tenantRepository.AddAsync(tenant);

        var code = _emailVerifyCodeGenerator.GenerateCode();

        var emailVerification = EmailVerification.Create(tenant.Id, 
                                                         tenant.ContactInfo.EmailAddress, 
                                                         code);

        await _emailVerificationRepository.AddAsync(emailVerification);
        await _dbSaver.SaveAsync();

        var message = _formFactory.CreateAfterRegistration(code, tenant.Name, tenant.ContactInfo.EmailAddress.Email);

        var sendResult = await _mailSenderService.SendMailAsync(message);

        if (!sendResult.IsSuccess)
            return Result.Fail(sendResult.Errors);

        return Result.Ok();
    }

    public async Task<Result> UpdateAsync(UpdateTenantParams @params)
    {
        var tenant = await _tenantRepository.GetAsync();

        if (tenant == null)
            return Result.Fail("Tenant Not Found");

        var tenantParams = CreateTenantParams(@params, tenant);

        var emailAddress = tenantParams.contactInfo.EmailAddress;
        var config = tenantParams.config;
        var contactInfo = tenantParams.contactInfo;
        var workSchedule = tenantParams.schedule;

        if(tenant.ContactInfo.EmailAddress != emailAddress)
        {
            var emailVerification = EmailVerification.Create(tenant.Id, 
                                                             emailAddress, 
                                                             _emailVerifyCodeGenerator.GenerateCode());

            await _emailVerificationRepository.UpdateAsync(emailVerification);
        }

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

        await _tenantRepository.RemoveAsync(existingTenant);

        await _dbSaver.SaveAsync();

        return Result.Ok();
    }

    public async Task<Result> VerifyEmailAsync(string code)
    {
        var tenant = await _tenantRepository.GetAsync();

        if (tenant is null)
            return Result.Fail("Tenant Not Found");

        if (tenant.ContactInfo.EmailAddress.IsConfirmed)
            return Result.Fail("Tenant has already been verified");

        var emailVerification = await _emailVerificationRepository.GetAsync(tenant.Id, tenant.ContactInfo.EmailAddress.Email);

        if (emailVerification == null)
            return Result.Fail("Confirmation Time has expired");

        if (emailVerification.Code != code)
            return Result.Fail("Codes does not equals");

        var emailAddres = tenant.ContactInfo.EmailAddress;
        var updatedEmailAddress = EmailAddress.Create(emailAddres.Email, true);
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
        var updatedEmailAddress = EmailAddress.Create(emailAddress.Email);

        var updatedContactInfo = ContactInfo.Create(tenant.ContactInfo.Phone, tenant.ContactInfo.Address, updatedEmailAddress);

        tenant.Update(tenant.Config, tenant.Status, updatedContactInfo, tenant.WorkSchedule);

        await _tenantRepository.UpdateAsync(tenant);

        await _dbSaver.SaveAsync();

        var emailVerification = EmailVerification.Create(tenant.Id, tenant.ContactInfo.EmailAddress, newVerifyCode);
        await _emailVerificationRepository.UpdateAsync(emailVerification);

        var message = _formFactory.CreateMessageForVerify(newVerifyCode, tenant.Name, tenant.ContactInfo.EmailAddress.Email);
        var sendMailResult = await _mailSenderService.SendMailAsync(message);

        if (!sendMailResult.IsSuccess)
            return Result.Fail(sendMailResult.Errors);

        return Result.Ok();
    }

    private Tenant CreateTenantFromParams(CreateTenantParams @params)
    {
        var paramsAddress = @params.ContactInfo.AddressDto;
        var address = Address.Create(paramsAddress.Country, paramsAddress.City, paramsAddress.Street, paramsAddress.House);

        var paramsEmailAddress = @params.ContactInfo.EmailAddressDto;

        var emailAddress = EmailAddress.Create(paramsEmailAddress.Email);

        var contactInfo = ContactInfo.Create(@params.ContactInfo.Phone, address, emailAddress);

        var paramConfig = @params.Config;
        var config = TenantConfiguration.Create(paramConfig.About, paramConfig.Currency, paramConfig.Language, paramConfig.TimeZoneId);

        var tenantId = Guid.CreateVersion7();

        var weekShedule = @params.WorkSchedule.WeekSchedule.Select(x => DaySchedule.Create(Guid.CreateVersion7(), tenantId, x.Day, x.StartWork, x.EndWork)).ToList();
        var workShedule = WorkSchedule.Create(weekShedule);

        var slug = _slugGenerator.GenerateSlug(@params.Name);

        return Tenant.Create(tenantId, @params.Name, slug, config, @params.Status, contactInfo, workShedule);
    }

    private (TenantConfiguration config, WorkSchedule schedule, ContactInfo contactInfo) CreateTenantParams(UpdateTenantParams @params, Tenant tenant)
    {
        var config = TenantConfiguration.Create(@params.Config.About, @params.Config.Currency, @params.Config.Language, @params.Config.TimeZoneId);

        var weekSchedule = @params.WorkSchedule.WeekSchedule
            .Select(d => DaySchedule.Create(d.Id, tenant.Id, d.Day, d.StartWork, d.EndWork))
            .OrderBy(d => d.Day).ToList();

        var workSchedule = WorkSchedule.Create(weekSchedule);

        var tenantContactInfo = tenant.ContactInfo.EmailAddress;
        var emailAddress = EmailAddress.Create(@params.ContactInfo.UpdateEmailAddressDto.Email);

        var paramsAddress = @params.ContactInfo.AddressDto;
        var address = Address.Create(paramsAddress.Country, paramsAddress.City, paramsAddress.Street, paramsAddress.House);

        var contactInfo = ContactInfo.Create(@params.ContactInfo.Phone, address, emailAddress);

        return new (config, workSchedule, contactInfo);
    }
}