using RabbitMQDemo.ResourceTests.Factories;

namespace RabbitMQDemo.ResourceTests.ClassFixtures;

public class RabbitMQClassFixture: IClassFixture<RabbitMQWebAppFactory>
{
    protected readonly HttpClient _client;
    protected readonly RabbitMQWebAppFactory _factory;

    public RabbitMQClassFixture(RabbitMQWebAppFactory factory)
    {
        _client = factory.CreateClient();
        _factory = factory;
    }
}
