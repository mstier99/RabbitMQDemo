using FluentAssertions;
using Newtonsoft.Json;
using RabbitMQDemo.Api.Controllers.RabbitMQ.Dtos;
using RabbitMQDemo.ResourceTests.ClassFixtures;
using RabbitMQDemo.ResourceTests.Factories;
using RabbitMQDemo.TestShared.Helpers;

namespace RabbitMQDemo.ResourceTests.CreateTests;

public class CreateQueueTests : RabbitMQClassFixture
{
    public CreateQueueTests(RabbitMQWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task CreateAQueue_WhenGetQueuesFromManagementApi_QueueIsExistsAndPropertiesAreMatch()
    {
        // Arrange
        string name = "q.integrationtest123";
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

        // Act
        var response = await _client.PostAsync(Endpoints.CreateQueues, content);

        // Assert
        response.EnsureSuccessStatusCode();
        var json = await _factory.CreateManagementApiClient()
                                 .GetStringAsync("api/queues");
        var queues = JsonConvert.DeserializeObject<List<Queue>>(json);

        var createdQueue = queues?.FirstOrDefault(x => x.name == name);

        createdQueue.Should().NotBeNull();
        createdQueue!.name.Should().Be(name);
        createdQueue.durable.Should().Be(durable);
        createdQueue.exclusive.Should().Be(exclusive);
        createdQueue.auto_delete.Should().Be(autoDelete);

        createdQueue.arguments.Should().NotBeNull();
        createdQueue.arguments!.xexpires.Should().Be(Convert.ToInt32(expireArgValue));
        createdQueue.arguments.xdeadletterexchange.Should().Be(deadletterArgValue);
    }

    private class Arguments
    {
        [JsonProperty("x-dead-letter-exchange")]
        public string? xdeadletterexchange { get; set; }

        [JsonProperty("x-expires")]
        public int xexpires { get; set; }
    }

    private class Queue
    {
        public Arguments? arguments { get; set; }
        public bool auto_delete { get; set; }
        public bool durable { get; set; }
        public bool exclusive { get; set; }
        public string? name { get; set; }
    }
}
