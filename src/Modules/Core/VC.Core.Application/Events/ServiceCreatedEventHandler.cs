using VC.Core.Events;

namespace VC.Core.Application.Events;

public class ServiceCreatedEventHandler : IDomainEventHandler<ServiceCreatedEvent>
{
    public Task HandleAsync(ServiceCreatedEvent domainEvent)
    {
        return Task.CompletedTask;
    }
}