using FluentResults;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using VC.Tenants.Api.Endpoints.Tenants.Models.Request;
using VC.Tenants.Api.Endpoints.Tenants.Models.Response;
using VC.Tenants.Application.Tenants;
using VC.Utilities.MailSend;

namespace VC.Tenants.Api.Controllers;

[ApiController]
[Route("[Controller]")]
[ApiExplorerSettings(GroupName = OpenApi.OpenApiConfig.GroupName)]
public class TenantsController : ControllerBase
{
    private readonly ITenantsService _tenantService;
    private readonly IValidator<CreateTenantRequest> _createTenantValidator;
    private readonly IValidator<UpdateTenantRequest> _updateTenantValidator;

    private readonly IMailSenderService _mailSenderService;

    public TenantsController(ITenantsService tenantService,
        IValidator<CreateTenantRequest> createTenantValidator,
        IValidator<UpdateTenantRequest> updateTenantValidator,
        IMailSenderService mailSenderService
        )
    {
        _tenantService = tenantService;
        _createTenantValidator = createTenantValidator;
        _updateTenantValidator = updateTenantValidator;

        _mailSenderService = mailSenderService;
    }

    [HttpGet]
    public async Task<ActionResult<ResponseTenantDto>> GetAsync()
    {
        var response = await _tenantService.GetAsync();

        if (!response.IsSuccess)
            return BadRequest(response);

        var mappedResponseDto = response
            .Value
            .ToResponseDto();

        return Ok(mappedResponseDto);
    }

    [HttpPost]
    public async Task<ActionResult> AddAsync(CreateTenantRequest createRequest)
    {
        var validationResult = await _createTenantValidator.ValidateAsync(createRequest);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var mappedCreateDto = createRequest.ToApplicationCreateDto();

        var response = await _tenantService.CreateAsync(mappedCreateDto);

        var subject = "Регистрация на сайте";
        var receiverName = createRequest.Name;
        var receiverMail = createRequest.Contact.Email;
        string header = "Спасибо за регистрацию на сайте!";

        string link = "http://localhost:5056/Tenants/verify-email";
        var linkTag = $"<a href= \"{link}\">Verify Link</a>";
        var text = $"Вы зарегестрировались на нашем сайте, но для возможности иметь все возможности при использовании ресурса вам необходимо подтвердить вашу почту, для этого вам нужно перейти по ссылке: {linkTag}";

        Message message = new Message(subject, text, receiverName, receiverMail, header);

        await _mailSenderService.SendMailAsync(message);

        Console.WriteLine("Отправлено");

        if (response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    }

    [HttpGet("verify-email")]
    public async Task<ActionResult<Result>> VerifyMailAsync()
    {
        Console.WriteLine("Verify");
        var response = await _tenantService.VerifyEmailAsync();

        if(response.IsSuccess) 
            return Ok(response);

        return BadRequest(response);
    }

    [HttpPost("sendMailAgain")]
    public async Task<ActionResult<Result>> SendVerifyMailAgain()
    {


        return Ok();
    }

    [HttpPut]
    public async Task<ActionResult> UpdateAsync(UpdateTenantRequest updateRequest)
    {
        var validationResult = await _updateTenantValidator.ValidateAsync(updateRequest);

        if (!validationResult.IsValid)
            return BadRequest(validationResult);

        var mappedUpdateDto = updateRequest.ToApplicationUpdateDto();

        var response = await _tenantService.UpdateAsync(mappedUpdateDto);

        if (response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteByIdAsync()
    {
        var response = await _tenantService.DeleteAsync();

        if (response.IsSuccess)
            return Ok();

        return BadRequest(response);
    }
}