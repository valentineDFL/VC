using FluentResults;
using FluentValidation;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using VC.MailkitIntegration;
using VC.Tenants.Api.Models.Request.Tenant;
using VC.Tenants.Api.Models.Response;
using VC.Tenants.Application.Models.Create;
using VC.Tenants.Application.Models.Update;
using VC.Tenants.Application.Tenants;
using VC.Utilities;

namespace VC.Tenants.Api.Controllers;

[ApiController]
[Route("[Controller]")]
[ApiExplorerSettings(GroupName = OpenApi.OpenApiConfig.GroupName)]
public class TenantsController : ControllerBase
{
    private readonly ITenantsService _tenantService;
    private readonly IValidator<CreateTenantRequest> _createTenantValidator;
    private readonly IValidator<UpdateTenantRequest> _updateTenantValidator;

    private readonly ISendMailService _mailSenderService;

    private readonly EndpointsUrls _endpointsUrls;

    private readonly IMapper _mapper;

    public TenantsController(ITenantsService tenantService,
        IValidator<CreateTenantRequest> createTenantValidator,
        IValidator<UpdateTenantRequest> updateTenantValidator,
        ISendMailService mailSenderService,
        IOptions<EndpointsUrls> options,
        IMapper mapper)
    {
        _tenantService = tenantService;
        _createTenantValidator = createTenantValidator;
        _updateTenantValidator = updateTenantValidator;

        _mailSenderService = mailSenderService;
        _endpointsUrls = options.Value;

        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<ResponseTenantDto>> GetAsync()
    {
        var getResult = await _tenantService.GetAsync();

        if (!getResult.IsSuccess)
            return BadRequest(getResult);

        var mappedResponseDto = _mapper.Map<ResponseTenantDto>(getResult.Value);

        return Ok(mappedResponseDto);
    }

    /// <summary>
    /// Эндпоинт принимает дату только в формате UTC
    /// </summary>
    [HttpPost("tenants/tenant")]
    public async Task<ActionResult> AddAsync(CreateTenantRequest createRequest)
    {
        var validationResult = await _createTenantValidator.ValidateAsync(createRequest);

        if (!validationResult.IsValid)
            return BadRequest(validationResult);

        var mappedCreateDto = _mapper.Map<CreateTenantParams>(createRequest);

        var createResult = await _tenantService.CreateAsync(mappedCreateDto);

        Message message = TenantEmailVerifyForm.RegistrationTenantEmailVerifyForm(_endpointsUrls.EmailVerifyEndpointUrl, createRequest.Name, createRequest.ContactInfo.Email);

        var sendResult = await _mailSenderService.SendMailAsync(message);

        if(!sendResult.IsSuccess)
            return BadRequest(sendResult);

        if (!createResult.IsSuccess)
            return BadRequest(createResult);

        return Ok(createResult);
    }

    [HttpGet("verify-email")]
    public async Task<ActionResult<Result>> VerifyMailAsync()
    {
        var verifyResult = await _tenantService.VerifyEmailAsync();

        if(verifyResult.IsSuccess)
            return Ok(verifyResult);

        return BadRequest(verifyResult);
    }

    [HttpPost("send-verify-mail")]
    public async Task<ActionResult<Result>> SendVerifyMailAgain()
    {
        var getResult = await _tenantService.GetAsync();

        if(!getResult.IsSuccess)
            return BadRequest(getResult);

        var tenant = getResult.Value;
        tenant.ChangeTimeToExpireVerifyLink();

        var updateTenantParams = _mapper.Map<UpdateTenantParams>(tenant);
        var putResult = await _tenantService.UpdateAsync(updateTenantParams);

        if(!putResult.IsSuccess)
            return BadRequest(putResult);

        Message message = TenantEmailVerifyForm.VerifyTenantEmailForm(_endpointsUrls.EmailVerifyEndpointUrl, tenant.Name, tenant.ContactInfo.Email);
        var sendResult = await _mailSenderService.SendMailAsync(message);

        if (!sendResult.IsSuccess)
            return BadRequest(sendResult);

        return Ok();
    }

    /// <summary>
    /// Эндпоинт принимает дату только в формате UTC
    /// </summary>
    [HttpPut]
    public async Task<ActionResult> UpdateAsync(UpdateTenantRequest updateRequest)
    {
        var validationResult = await _updateTenantValidator.ValidateAsync(updateRequest);

        if (!validationResult.IsValid)
            return BadRequest(validationResult);

        var mappedUpdateDto = _mapper.Map<UpdateTenantRequest, UpdateTenantParams>(updateRequest);

        var updateResult = await _tenantService.UpdateAsync(mappedUpdateDto);

        if (updateResult.IsSuccess)
            return Ok(updateResult);

        return BadRequest(updateResult);
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteByIdAsync()
    {
        var deleteResult = await _tenantService.DeleteAsync();

        if (deleteResult.IsSuccess)
            return Ok();

        return BadRequest(deleteResult);
    }
}