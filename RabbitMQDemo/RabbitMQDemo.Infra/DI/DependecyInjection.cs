using RabbitMQDemo.Application.DI;
using RabbitMQDemo.Application.Repositories;
using RabbitMQDemo.Infra.RabbitMQ;
using RabbitMQDemo.Infra.Repositories;
using RabbitMQDemo.Infra.Repositories.ManagementApi;

namespace RabbitMQDemo.Infra.DI;

public static class DependecyInjection
{
    public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplication(configuration);

        services.AddRabbitMQConnection(configuration);
        services.AddManagementApiRepository(configuration);

        services.AddSingleton<IResourceRepository, ResourceRepository>();

        return services;
    }
}
