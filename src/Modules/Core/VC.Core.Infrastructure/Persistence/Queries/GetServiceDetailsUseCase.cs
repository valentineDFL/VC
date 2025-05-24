using Microsoft.EntityFrameworkCore;
using VC.Core.Application.ServicesUseCases;
using VC.Core.Application.ServicesUseCases.Models;

namespace VC.Core.Infrastructure.Persistence.Queries;

public class GetServiceDetailsUseCase(DatabaseContext _dbContext) : IGetServiceDetailsUseCase
{
    public async Task<ServiceDetailsDto?> ExecuteAsync(Guid serviceId)
    {
        return await _dbContext.Services
            .Where(s => s.Id == serviceId)
            .ToServiceDetailsDtos(_dbContext)
            .FirstOrDefaultAsync();
    }
}