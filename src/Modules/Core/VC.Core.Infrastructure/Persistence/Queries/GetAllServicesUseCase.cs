using Microsoft.EntityFrameworkCore;
using VC.Core.Application.ServicesUseCases;
using VC.Core.Application.ServicesUseCases.Models;

namespace VC.Core.Infrastructure.Persistence.Queries;

public class GetAllServicesUseCase(DatabaseContext _dbContext) : IGetAllServicesUseCase
{
    public async Task<List<ServiceDetailsDto>> ExecuteAsync()
    {
        return await _dbContext.Services.ToServiceDetailsDtos(_dbContext).ToListAsync();
    }
}