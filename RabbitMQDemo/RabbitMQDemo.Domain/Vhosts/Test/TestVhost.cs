using Microsoft.Extensions.Options;
using RabbitMQDemo.Domain.Users;

namespace RabbitMQDemo.Domain.Vhosts.Test;

public class TestVhost : IVhost
{
    public string Name { get; init; } = "Test";

    public RabbitMQUser Admin { get; init; }
    public RabbitMQUser Publisher { get; init; }
    public RabbitMQUser Consumer { get; init; }

    public TestVhost(IOptions<TestVhostOption> option)
    {
        Admin = option.Value.Admin;
        Publisher = option.Value.Publisher;
        Consumer = option.Value.Consumer;
    }
}
