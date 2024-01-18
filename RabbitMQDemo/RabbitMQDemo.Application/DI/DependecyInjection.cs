using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQDemo.Application.PipelineBehaviors;
using RabbitMQDemo.Application.PipelineBehaviors.Validation;

namespace RabbitMQDemo.Application.DI;

public static class DependecyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatrTosolution();

        return services;
    }

    private static void AddMediatrTosolution(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(DependecyInjection).Assembly);
            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehavior<,>));

        services.AddValidatorsFromAssembly(typeof(DependecyInjection).Assembly);
    }
}
