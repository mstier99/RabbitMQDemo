using Newtonsoft.Json;
using RabbitMQDemo.Api.Controllers.Administrator.Dtos;
using RabbitMQDemo.ResourceTests.ClassFixtures;
using RabbitMQDemo.ResourceTests.Factories;
using RabbitMQDemo.TestShared.Helpers;
using System.Net;

namespace RabbitMQDemo.AdministratorTests.UserTests;

public class CreateUserTests : RabbitMQClassFixture
{
    public CreateUserTests(RabbitMQWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task CreateAUser_WhenGetUsersFromManagementApi_UserAreExistsAndPropertiesAreMatch()
    {
        // Arrange
        var userName = "test";
        var password = "test";
        List<string> tags = ["administrator"];

        var dto = new CreateUserDto(userName, password, tags);

        var content = StringContentHelper.Create(dto);

        // Act
        var response = await _client.PostAsync(Endpoints.CreateUser, content);

        // Assert
        response.EnsureSuccessStatusCode();

        var json = await _factory.CreateManagementApiClient()
                                 .GetStringAsync("api/users");
        
        var users = JsonConvert.DeserializeObject<List<User>>(json);

        var user = users?.SingleOrDefault(x => x.name == userName);

        user.Should().NotBeNull();
    }

    [Fact]
    public async Task CreateAUserWithInvalidTag_WhenCallApi_ResponseWithValidationfail()
    {
        // Arrange
        var userName = "test";
        var password = "test";
        List<string> tags = ["invalid tag"];

        var dto = new CreateUserDto(userName, password, tags);

        var content = StringContentHelper.Create(dto);

        // Act
        var response = await _client.PostAsync(Endpoints.CreateUser, content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    public class User
    {
        public required string name { get; set; }
        public required string password_hash { get; set; }
        public required List<string> tags { get; set; }
    }
}
