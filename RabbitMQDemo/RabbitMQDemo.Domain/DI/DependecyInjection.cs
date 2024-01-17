using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQDemo.Domain.Vhosts.Develop;
using RabbitMQDemo.Domain.Vhosts.Test;
namespace RabbitMQDemo.Domain.DI;

public static class DependecyInjection
{
    public static IServiceCollection AddDomain(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTestVhost(configuration);
        services.AddDevelopVhost(configuration);

        return services;
    }
}
