namespace RabbitMQDemo.Application.Commands.User.DeleteUser;

public record DeleteUsersCommand(List<string> Users): ICommand;
