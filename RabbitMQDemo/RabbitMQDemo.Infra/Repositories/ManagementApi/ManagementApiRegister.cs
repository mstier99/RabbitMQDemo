using RabbitMQDemo.Application.Repositories;

namespace RabbitMQDemo.Infra.Repositories.ManagementApi;

public static class ManagementApiRegister
{
    public static void AddManagementApiRepository(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ManagementApiRepository>(configuration.GetSection(nameof(ManagementApiRepository)));
        services.AddSingleton<IManagementApiRepository, ManagementApiRepository>();
    }
}
