using VC.Core.Common;

namespace VC.Core.Events;

public class ServiceCreatedEvent : DomainEvent
{
    public ServiceCreatedEvent(Service service)
        => Service = service;
    
    public Service Service { get; }
}