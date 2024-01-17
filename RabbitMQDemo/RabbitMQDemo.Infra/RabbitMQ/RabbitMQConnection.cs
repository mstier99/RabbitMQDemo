using RabbitMQ.Client;

namespace RabbitMQDemo.Infra.RabbitMQ;

public class RabbitMQConnection : IRabbitMQConnection
{
    Lazy<IConnection> _connection;

    public RabbitMQConnection(IOptions<RabbitMQConnectionOptions> options)
    {
        _connection = CreateLazyConnection(options.Value);
    }

    private Lazy<IConnection> CreateLazyConnection(RabbitMQConnectionOptions options)
    {
        return new Lazy<IConnection>(() =>
        {
            var connectionFactory = new ConnectionFactory();

            connectionFactory.ClientProvidedName = $"app: RabbitMQDemo, {options.ClientProvidedName}";

            connectionFactory.HostName = options.HostName;
            connectionFactory.VirtualHost = options.VirtualHost;
            connectionFactory.Port = options.Port;
            connectionFactory.UserName = options.UserName;
            connectionFactory.Password = options.Password;

            connectionFactory.DispatchConsumersAsync = true;

            return connectionFactory.CreateConnection();
        });
    }

    public IModel CreateChannel() => _connection!.Value.CreateModel();
}
