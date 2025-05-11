using VC.Core.Common;

namespace VC.Core.Events;

public interface IDomainEventHandler<in TEvent> 
    where TEvent : DomainEvent
{
    Task HandleAsync(TEvent domainEvent);
}