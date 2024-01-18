using Newtonsoft.Json;
using RabbitMQDemo.Api.Controllers.Administrator.Dtos;
using RabbitMQDemo.ResourceTests.ClassFixtures;
using RabbitMQDemo.ResourceTests.Factories;
using RabbitMQDemo.TestShared.Helpers;

namespace RabbitMQDemo.AdministratorTests.VhostTests;

public class CreateVhostTests : RabbitMQClassFixture
{
    public CreateVhostTests(RabbitMQWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task CreateAVhost_WhenGetVhostFromManagementApi_VhostAreExistsAndPropertiesAreMatch()
    {
        // Arrange
        var name = "VhostNameTest";

        var dto = new CreateVhostDto(name);

        var content = StringContentHelper.Create(dto);

        // Act
        var response = await _client.PostAsync(Endpoints.CreateVhost, content);

        // Assert
        response.EnsureSuccessStatusCode();

        var json = await _factory.CreateManagementApiClient()
                                 .GetStringAsync("api/vhosts");
        var vhosts = JsonConvert.DeserializeObject<List<Vhost>>(json);

        var vhost = vhosts?.SingleOrDefault(x => x.name == name);

        vhost.Should().NotBeNull();
    }

    public class Metadata
    {
        public required string description { get; set; }
        public required List<object> tags { get; set; }
    }

    public class Vhost
    {
        public required string description { get; set; }
        public required Metadata metadata { get; set; }
        public required string name { get; set; }
        public required List<object> tags { get; set; }
        public required bool tracing { get; set; }
    }
}
