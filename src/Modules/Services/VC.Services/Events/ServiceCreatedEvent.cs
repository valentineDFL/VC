using VC.Services.Common;

namespace VC.Services.Events;

public class ServiceCreatedEvent : DomainEvent
{
    public ServiceCreatedEvent(Service service)
        => Service = service;
    
    public Service Service { get; }
}