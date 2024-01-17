using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RabbitMQDemo.Domain.Vhosts.Test;

public static class RegisterTestVhost
{
    public static void AddTestVhost(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<TestVhostOption>(configuration.GetSection(nameof(TestVhostOption)));
        services.AddSingleton<TestVhost>();
    }
}
