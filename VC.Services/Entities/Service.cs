namespace VC.Services.Entities;
public class Service
{
    public Guid Id { get; set; }

    public ServiceStatus Status { get; set; }

    public ServiceConfiguration Config {  get; set; }

    public ServiceInfo Info { get; set; }
}
