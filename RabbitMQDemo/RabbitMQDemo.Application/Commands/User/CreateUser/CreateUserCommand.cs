namespace RabbitMQDemo.Application.Commands.User.CreateUser;

public record CreateUserCommand(
    string UserName,
    string Password,
    List<string> Tags) : ICommand;
