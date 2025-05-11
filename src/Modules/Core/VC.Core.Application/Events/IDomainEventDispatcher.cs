using VC.Core.Common;

namespace VC.Core.Application.Events;

public interface IDomainEventDispatcher
{
    Task DispatchAsync(DomainEvent domainEvent);
}