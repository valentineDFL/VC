using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VC.Tenants.Repositories;
using VC.Tenants.UnitOfWork;

namespace VC.Tenants.Api.Controller
{
    [ApiController]
    [Route("[Controller]")]
    [ApiExplorerSettings(GroupName = OpenApi.OpenApiConfig.GroupName)]
    public class TenantsController : ControllerBase
    {
        private readonly ITenantRepository _tenantRepository;
        private readonly IDbSaver _dbSaver;

        public TenantsController(ITenantRepository tenantRepository, IDbSaver dbSaver)
        {
            _tenantRepository = tenantRepository;
            _dbSaver = dbSaver;
        }

        [HttpGet]
        public async Task<ActionResult> GetByIdAsync(Guid id)
        {
            var response = _tenantRepository.GetByIdAsync(id);


            return Ok(id);
        }

    }
}
