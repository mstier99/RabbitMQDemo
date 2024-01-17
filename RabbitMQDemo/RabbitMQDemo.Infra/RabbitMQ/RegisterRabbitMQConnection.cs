namespace RabbitMQDemo.Infra.RabbitMQ;

public static class RegisterRabbitMQConnection
{
    public static void AddRabbitMQConnection(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitMQConnectionOptions>(configuration.GetSection(nameof(RabbitMQConnectionOptions)));
        services.AddSingleton<IRabbitMQConnection, RabbitMQConnection>();
    }
}
