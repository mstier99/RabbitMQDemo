using Microsoft.Extensions.DependencyInjection;

namespace RabbitMQDemo.TestShared;

public static class ServiceExtensions
{
    public static void  RemoveOrException<T>(this IServiceCollection services)
    {
        ServiceDescriptor? descriptor = services.SingleOrDefault(service => service.ServiceType == typeof(T))
            ?? throw new InvalidOperationException($"The type {typeof(T).FullName} is not registered in the service container.");

        services.Remove(descriptor);
    }
}

