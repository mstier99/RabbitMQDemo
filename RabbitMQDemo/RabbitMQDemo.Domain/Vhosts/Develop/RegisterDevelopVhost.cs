using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RabbitMQDemo.Domain.Vhosts.Develop;

public static class RegisterTestVhost
{
    public static void AddDevelopVhost(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DevelopVhostOption>(configuration.GetSection(nameof(DevelopVhostOption)));
        services.AddSingleton<DevelopVhost>();
    }
}
