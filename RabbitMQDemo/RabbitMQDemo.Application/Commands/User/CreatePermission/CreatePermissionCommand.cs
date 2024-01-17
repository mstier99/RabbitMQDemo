namespace RabbitMQDemo.Application.Commands.User.CreatePermission;

public record CreatePermissionCommand(
    string UserName,
    string Vhost,
    string Configure,
    string Write,
    string Read) : ICommand;