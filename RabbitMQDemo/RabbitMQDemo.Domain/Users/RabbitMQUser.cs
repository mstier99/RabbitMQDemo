namespace RabbitMQDemo.Domain.Users;

public record RabbitMQUser
{
    public required string UserName { get;  set; }
    public required string Password { get; set; }
}
