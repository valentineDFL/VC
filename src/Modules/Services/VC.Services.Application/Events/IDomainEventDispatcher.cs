using VC.Services.Common;

namespace VC.Services.Application.Events;

public interface IDomainEventDispatcher
{
    Task DispatchAsync(DomainEvent domainEvent);
}