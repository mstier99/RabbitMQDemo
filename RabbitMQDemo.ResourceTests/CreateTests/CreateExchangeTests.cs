using Newtonsoft.Json;
using RabbitMQDemo.Api.Controllers.Resource.Dtos;
using RabbitMQDemo.ResourceTests.ClassFixtures;
using RabbitMQDemo.ResourceTests.Factories;
using RabbitMQDemo.TestShared.Helpers;

namespace RabbitMQDemo.ResourceTests.CreateTests;

public class CreateExchangeTests : RabbitMQClassFixture
{
    public CreateExchangeTests(RabbitMQWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task CreateAExchnage_WhenGetExchangesFromManagementApi_ExchangeIsExistsAndPropertiesAreMatch()
    {
        // Arrange
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

        // Act
        var response = await _client.PostAsync(Endpoints.CreateExchanges, content);

        // Assert
        response.EnsureSuccessStatusCode();

        var json = await _factory.CreateManagementApiClient()
                                 .GetStringAsync("api/exchanges");
        var exchanges = JsonConvert.DeserializeObject<List<Exchange>>(json);

        var createdExchange = exchanges?.FirstOrDefault(x => x.name == name);

        createdExchange.Should().NotBeNull();
        createdExchange!.name.Should().Be(name);
        createdExchange.type.Should().Be(type);
        createdExchange.durable.Should().Be(durable);
        createdExchange.auto_delete.Should().Be(autoDelete);
    }


    class Arguments
    {
    }

    class Exchange
    {
        public Arguments? arguments { get; set; }
        public bool auto_delete { get; set; }
        public bool durable { get; set; }
        public string? name { get; set; }
        public string? type { get; set; }
        public string? vhost { get; set; }
    }
}
