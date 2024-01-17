namespace RabbitMQDemo.Application.Queries.GetUsers;

public record GetUsersResponse(List<(string Name, string[] Tags)> Users);
