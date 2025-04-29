using Microsoft.AspNetCore.Mvc;

namespace VC.Services.Api.Controllers;

[ApiController]
[ApiExplorerSettings(GroupName = OpenApi.OpenApiConfig.GroupName)]
public class ApiController : ControllerBase;