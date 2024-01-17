using RabbitMQ.Client;

namespace RabbitMQDemo.Infra;

public interface IRabbitMQConnection
{
    IModel CreateChannel();
}