using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VC.Shared.RabbitMQIntegration.Consumers.Interfaces;

namespace VC.Core.Infrastructure.BackgroundServices;

public class RabbitConsumersBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public RabbitConsumersBackgroundService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    /// <summary>
    /// Инициализирует Всех Consumers в проекте.
    /// </summary>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var scope = _scopeFactory.CreateScope();

        var consumers = scope.ServiceProvider.GetServices<IConsumer>();

        var tasks = new List<Task>();
        foreach(var consumer in consumers)
        {
            var task = consumer.ConnectAsync(stoppingToken);
            tasks.Add(task);
        }
        await Task.WhenAll(tasks);
    }
}