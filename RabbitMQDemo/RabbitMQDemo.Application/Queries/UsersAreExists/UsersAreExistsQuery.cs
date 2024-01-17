namespace RabbitMQDemo.Application.Queries.UsersAreExists;

public record UsersAreExistsQuery(List<string> UserNames): IQuery<bool>;
