namespace RabbitMQDemo.Api.Controllers.Resource.Dtos;

public record CreateExchangeDto(
    string Name,
    string Type,
    bool Durable,
    bool AutoDelete);

