namespace RabbitMQDemo.Api.Controllers.Resource.Dtos;

public record CreateBindDto(
    string QueueName,
    string ExchangeName,
    string RoutingKey
    );
