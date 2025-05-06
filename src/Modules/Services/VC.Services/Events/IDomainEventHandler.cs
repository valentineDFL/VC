using VC.Services.Common;

namespace VC.Services.Events;

public interface IDomainEventHandler<in TEvent> 
    where TEvent : DomainEvent
{
    Task HandleAsync(TEvent domainEvent);
}