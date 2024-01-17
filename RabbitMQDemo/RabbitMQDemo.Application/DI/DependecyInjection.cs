using Microsoft.Extensions.DependencyInjection;
using RabbitMQDemo.Application.PipelineBehaviors;

namespace RabbitMQDemo.Application.DI;

public static class DependecyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(DependecyInjection).Assembly);
        });

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehavior<,>));

        return services;
    }
}
