using VC.Core.Application.ServicesUseCases.Models;
using VC.Core.Services;

namespace VC.Core.Infrastructure.Persistence.Queries;

public static class ServiceDetailsDtoExtensions
{
    public static IQueryable<ServiceDetailsDto> ToServiceDetailsDtos(this IQueryable<Service> queryable, DatabaseContext dbContext)
    {
        return queryable.Select(s => new ServiceDetailsDto
        {
            Id = s.Id,
            Title = s.Title,
            Description = s.Description,
            BasePrice = s.BasePrice,
            BaseDuration = s.BaseDuration,
            Category = s.CategoryId != null
                ? dbContext.Categories
                    .Where(c => c.Id == s.CategoryId)
                    .Select(c => new CategoryDto { Id = c.Id, Title = c.Title })
                    .FirstOrDefault()
                : null,
            RequiredResources = dbContext.Resources
                .Where(r => s.RequiredResources.Contains(r.Id))
                .Select(r => new ResourceDto { Id = r.Id, Title = r.Title, Count = r.Count })
                .ToList(),
            IsActive = s.IsActive,
            EmployeeAssignments = s.EmployeeAssignments
                .Select(a => new EmployeeAssignmentDto
                {
                    EmployeeId = a.EmployeeId, Price = a.Price, Duration = a.Duration
                })
                .ToList()
        });
    }
}