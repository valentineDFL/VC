using FluentResults;
using FluentValidation;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using VC.Tenants.Api.Models.Request.Tenant;
using VC.Tenants.Api.Models.Response;
using VC.Tenants.Application.Models.Create;
using VC.Tenants.Application.Models.Update;
using VC.Tenants.Application.Tenants;
using VC.Utilities;
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

    private readonly ISendMailService _mailSenderService;

    private readonly EndpointsUrls _endpointsUrls;

    private readonly IMapper _mapper;

    public TenantsController(ITenantsService tenantService,
        IValidator<CreateTenantRequest> createTenantValidator,
        IValidator<UpdateTenantRequest> updateTenantValidator,
        ISendMailService mailSenderService,
        IOptions<EndpointsUrls> options,
        IMapper mapper
        )
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
        var response = await _tenantService.GetAsync();

        if (!response.IsSuccess)
            return BadRequest(response);

        var mappedResponseDto = _mapper.Map<ResponseTenantDto>(response.Value);

        return Ok(mappedResponseDto);
    }

    [HttpPost]
    public async Task<ActionResult> AddAsync(CreateTenantRequest createRequest)
    {
        Console.WriteLine(createRequest is null);

        var validationResult = await _createTenantValidator.ValidateAsync(createRequest);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var mappedCreateDto = _mapper.Map<CreateTenantRequest, CreateTenantParams>(createRequest);

        Console.WriteLine(mappedCreateDto.Contact.Address.Country);

        var response = await _tenantService.CreateAsync(mappedCreateDto);

        Message message = TenantEmailVerifyForm.RegistrationTenantEmailVerifyForm(_endpointsUrls.EmailVerifyEndpointUrl, createRequest.Name, createRequest.Contact.Email);

        await _mailSenderService.SendMailAsync(message);

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

        var mappedUpdateDto = _mapper.Map<UpdateTenantRequest, UpdateTenantParams>(updateRequest);

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