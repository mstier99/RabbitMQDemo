namespace RabbitMQDemo.Api.Controllers.Administrator.Dtos;

public record CreateUserDto(
    string UserName,
    string Password,
    List<string> Tags
    );
