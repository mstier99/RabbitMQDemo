using Microsoft.Extensions.Options;
using RabbitMQDemo.Domain.Users;

namespace RabbitMQDemo.Domain.Vhosts.Develop;

public class DevelopVhost : IVhost
{
    public string Name { get; init; } = "Develop";

    public RabbitMQUser Admin { get; init; }
    public RabbitMQUser Publisher { get; init; }
    public RabbitMQUser Consumer { get; init; }

    public DevelopVhost(IOptions<DevelopVhostOption> option)
    {
        Admin = option.Value.Admin;
        Publisher = option.Value.Publisher;
        Consumer = option.Value.Consumer;
    }
}
