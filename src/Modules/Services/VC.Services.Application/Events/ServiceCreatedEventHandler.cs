using VC.Services.Events;

namespace VC.Services.Application.Events;

public class ServiceCreatedEventHandler : IDomainEventHandler<ServiceCreatedEvent>
{
    public Task HandleAsync(ServiceCreatedEvent domainEvent)
    {
        throw new NotImplementedException();
    }
}