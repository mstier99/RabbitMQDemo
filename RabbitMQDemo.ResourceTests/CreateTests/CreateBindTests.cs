using Newtonsoft.Json;
using RabbitMQDemo.Api.Controllers.RabbitMQ.Dtos;
using RabbitMQDemo.Api.Controllers.Resource.Dtos;
using RabbitMQDemo.ResourceTests.ClassFixtures;
using RabbitMQDemo.ResourceTests.Factories;
using RabbitMQDemo.TestShared.Helpers;

namespace RabbitMQDemo.ResourceTests.CreateTests;

public class CreateBindTests : ClassFixtures.RabbitMQClassFixture
{
    public CreateBindTests(RabbitMQWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task CreateAQueueAndExcangeAndBind_WhenGetBindsFromManagementApi_BindIsExistsAndPropertiesAreMatch()
    {
        // Arrange
        string source = await CreateQueue();
        string destination = await CreateExchange();
        var routingKey = "some routing key";

        var dto = new List<CreateBindDto>() { new CreateBindDto(source, destination, routingKey)};
        var content = StringContentHelper.Create(dto);

        // Act
        var response = await _client.PostAsync(Endpoints.CreateBinds, content);

        // Assert
        response.EnsureSuccessStatusCode();
        var json = await _factory.CreateManagementApiClient()
                                 .GetStringAsync("api/bindings");
        var binds = JsonConvert.DeserializeObject<List<Bind>>(json)!;

        binds.Count().Should().Be(2);
    }

    private async Task<string> CreateExchange()
    {
        var name = "ex.test.name";
        var type = "fanout";
        var durable = RandomHelper.GetTrueOrFalse;
        var autoDelete = true;

        var dto = new List<CreateExchangeDto>() {
            new CreateExchangeDto(
            name,
            type,
            durable,
            autoDelete
            )
        };

        var content = StringContentHelper.Create(dto);

        var response = await _client.PostAsync(Endpoints.CreateExchanges, content);
        response.EnsureSuccessStatusCode();

        return name;
    }

    private async Task<string> CreateQueue()
    {
        string name = "q.bindtest";
        bool durable = false;
        bool exclusive = RandomHelper.GetTrueOrFalse;
        bool autoDelete = true;
        string expireArgValue = "100000";
        string deadletterArgValue = "some.queue.name";
        List<Argument> arguments =
            [
                Argument.CreateExpireArgument(expireArgValue),
                Argument.CreateDeadletterExchange(deadletterArgValue)
            ];

        var dto = new List<CreateQueueDto> { new CreateQueueDto
        {
            Name = name,
            Durable = durable,
            Exclusive = exclusive,
            AutoDelete = autoDelete,
            Arguments = arguments
        } };
        var content = StringContentHelper.Create(dto);

        var response = await _client.PostAsync(Endpoints.CreateQueues, content);
        response.EnsureSuccessStatusCode();

        return name;
    }

    class Arguments
    {
    }

    class Bind
    {
        public string? source { get; set; }
        public string? vhost { get; set; }
        public string? destination { get; set; }
        public string? destination_type { get; set; }
        public string? routing_key { get; set; }
        public Arguments? arguments { get; set; }
        public string? properties_key { get; set; }
    }


}
