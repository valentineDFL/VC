using MediatR;
using VC.Services.Entities;
using VC.Services.Repositories;
using VC.Services.UnitOfWork;

namespace VC.Services.Application.Services.Commands.CreateService;

public class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand, Guid>
{
    private readonly IServicesRepository _dbContext;
    private readonly IDbSaver _dbSaver;

    public CreateServiceCommandHandler(IServicesRepository dbContext, IDbSaver dbSaver)
    {
        _dbContext = dbContext;
        _dbSaver = dbSaver;
    }

    public async Task<Guid> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
    {
        var service = new Service
        {
            Id = Guid.CreateVersion7(),
            Title = request.Title,
            Description = request.Description,
            Price = request.Price,
            Duration = request.Duration,
            IsActive = request.IsActive,
            CreatedAt = DateTime.UtcNow,
            TenantId = request.TenantId,
            UpdatedAt = null,
            ResourceRequirement = new()
        };

        await _dbContext.AddAsync(service, cancellationToken);
        await _dbSaver.SaveAsync();

        return service.Id;
    }
}
